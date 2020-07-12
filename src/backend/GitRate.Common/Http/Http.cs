using GitRate.Common.Extensions;
using GitRate.Common.Http.Exceptions;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace GitRate.Common.Http
{
    /// <summary> Http client for making http requests between applications  </summary>
    public abstract class Http : IHttp
    {
        private Uri? _requestUri;
        private HttpContent? _httpContent;
        private FileRequest[]? _requestFiles;
        private List<KeyValuePair<string, string>>? _queryParams;

        private HttpClient _httpClient;
        private ILogger<Http> _logger;

        /// <summary> Http client for making http requests between applications  </summary>
        public Http(HttpClient httpClient, ILogger<Http> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        #region Public methods

        /// <summary> <inheritdoc /> </summary>
        public IHttp For(string uri)
        {
            _requestUri = new Uri(uri, UriKind.RelativeOrAbsolute);
            return this;
        }

        /// <summary> <inheritdoc /> </summary>
        public IHttp For(Uri uri)
        {
            _requestUri = uri;
            return this;
        }

        /// <summary> <inheritdoc /> </summary>
        public IHttp WithBody<TBody>(TBody body) where TBody : class
        {
            _httpContent = BuildRequestBody(body);
            return this;
        }

        /// <summary> <inheritdoc /> </summary>
        public IHttp WithFiles(params FileRequest[] files)
        {
            var content = new MultipartFormDataContent();

            foreach (var file in files)
            {
                var streamContent = new StreamContent(file.FileStream);

                streamContent.Headers.ContentType = new MediaTypeHeaderValue(HttpConstants.MimeTypes.Octet);
                streamContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                {
                    FileName = file.FileName,
                    Name = file.ContentBodyPartName,
                };

                content.Add(streamContent, "data");
            }

            _httpContent = content;

            return this;
        }

        /// <summary> <inheritdoc /> </summary>
        public IHttp WithQueryParams<TQueryModel>(TQueryModel queryModel) where TQueryModel : class
        {
            var queryParams = new List<KeyValuePair<string, string>>();

            PopulateQueryParams(queryModel, queryParams);

            return WithQueryParams(queryParams);
        }

        /// <summary> <inheritdoc /> </summary>
        public IHttp WithQueryParams(List<KeyValuePair<string, string>> queryParams)
        {
            if (_queryParams == null)
                _queryParams = new List<KeyValuePair<string, string>>(queryParams.Count);

            _queryParams.AddRange(queryParams);

            return this;
        }

        /// <summary> <inheritdoc/> </summary>
        public async Task<TResponse> PostAsync<TResponse>() where TResponse : class
        {
            return await SendAsync<TResponse>(HttpMethod.Post);
        }


        /// <summary> <inheritdoc/> </summary>
        public async Task<TResponse> GetAsync<TResponse>() where TResponse : class
        {
            return await SendAsync<TResponse>(HttpMethod.Get);
        }

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public async Task<TResponse> SendAsync<TResponse>(HttpMethod method) where TResponse : class
        {
            var response = await SendAsync(method);

            return await BuildResponseBody<TResponse>(response);
        }

        /// <summary> <inheritdoc /> </summary>
        public async Task<HttpResponseMessage> SendAsync(HttpMethod method)
        {
            var response = default(HttpResponseMessage);

            var requestUri = BuildRequestUri();

            var httpRequestMessage = BuildHttpRequestMessage(method, requestUri);

            try
            {
                _logger.LogInformation("Request to {uri}", requestUri);

                response = await _httpClient.SendAsync(httpRequestMessage);

                _logger.LogInformation("Response recieved from {uri}", requestUri);

                return response.EnsureSuccessStatusCode();
            }
            catch (Exception ex) when (ex is HttpRequestException || ex is TimeoutException)
            {
                var uriStr = requestUri.ToString();

                _logger.LogError(ex, "An error occurred during http request. RequestUri: {uri}", uriStr);

                if (response == null)
                    throw;

                var responseContent = await response.Content.ReadAsStringAsync();

                throw new HttpException("Error occurred during http request", ex, response.StatusCode, uriStr, responseContent);

            }
            finally
            {
                _requestUri = null;
                _httpContent = null;
                _requestFiles = null;
                _queryParams = null;
            }
        }

        #endregion

        #region Protected methods

        /// <summary>
        /// Builds request body
        /// </summary>
        protected abstract HttpContent? BuildRequestBody<TRequestBody>(TRequestBody body);

        /// <summary>
        /// Builds response body
        /// </summary>
        protected abstract Task<TResponseBody> BuildResponseBody<TResponseBody>(HttpResponseMessage response) where TResponseBody : class;

        #endregion

        #region Private methods

        /// <summary>
        /// Builds request uri based on passed uri string and query params
        /// </summary>
        private Uri BuildRequestUri()
        {
            if (_requestUri == null)
                throw new ArgumentNullException(nameof(_requestUri), "Request Uri cannot be null");

            if (_queryParams == null || !_queryParams.Any())
                return _requestUri;

            var queryBuilder = new QueryBuilder(_queryParams);

            var resultUrl = $"{_requestUri}{queryBuilder.ToQueryString().ToUriComponent()}";

            var uriKind = _requestUri.IsAbsoluteUri
                ? UriKind.Absolute
                : UriKind.Relative;

            return new Uri(resultUrl, uriKind);
        }

        /// <summary>
        /// Builds http request message based on passed body or files
        /// </summary>
        private HttpRequestMessage BuildHttpRequestMessage(HttpMethod method, Uri uri)
        {
            return new HttpRequestMessage
            {
                Method = method,
                RequestUri = uri,
                Content = _httpContent
            };
        }

        /// <summary>
        /// Reads all models properties and writes to <paramref name="queryParams"/> recursively
        /// </summary>
        /// <typeparam name="TQueryModel">Model of type to read properties from</typeparam>
        /// <param name="queryModel">Actual model to read from</param>
        /// <param name="queryParams">Query list to write params to</param>
        private void PopulateQueryParams<TQueryModel>(TQueryModel queryModel, List<KeyValuePair<string, string>> queryParams)
        {
            if (queryModel == null)
                return;

            var propertyInfos = queryModel.GetType().GetProperties();

            foreach (var propertyinfo in propertyInfos)
            {
                var propertyValue = propertyinfo.GetValue(queryModel);

                if (propertyValue == null)
                    continue;

                // if current property is collection
                // then loop through and add its values to query params
                if (propertyinfo.IsIEnumerable())
                {
                    var collection = (IEnumerable)propertyValue;

                    // we rely on fact that collection contains primitives
                    foreach (var value in collection)
                    {
                        if (value != null)
                            queryParams.Add(new KeyValuePair<string, string>(propertyinfo.Name, Convert.ToString(value, CultureInfo.InvariantCulture)));
                    }
                    continue;
                }

                // if current property is class recursively do the same
                if (propertyinfo.IsClass())
                {
                    // get current executing method
                    var methodInfo = GetType().GetGenericMethod(nameof(PopulateQueryParams));

                    // get typed method
                    var typedMethod = methodInfo.MakeGenericMethod(propertyinfo.PropertyType);

                    typedMethod.Invoke(this, new[] { propertyValue, queryParams });
                    continue;
                }

                // current property is primitive
                queryParams.Add(new KeyValuePair<string, string>(propertyinfo.Name, Convert.ToString(propertyValue, CultureInfo.InvariantCulture)));
            }
        }

        #endregion
    }
}
