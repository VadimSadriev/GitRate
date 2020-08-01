using GitRate.Application.Dto.Repository;
using GitRate.Application.Enums;
using GitRate.Application.Extensions;
using GitRate.Application.Github;
using GitRate.Common.Extensions;
using GitRate.Common.Http.Clients.Github;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace GitRate.Application.Services
{
    /// <summary>
    /// Service for building queries for github search
    /// </summary>
    public class GithubQueryBuilder : IGithubQueryBuilder
    {
        private readonly IOptions<GithubHttpConfiguration> _options;
        private readonly Dictionary<RepositorySearchCriteria, string> _criteriaQueryDict;

        /// <summary>
        /// Service for building queries for github search
        /// </summary>
        public GithubQueryBuilder(IOptions<GithubHttpConfiguration> options)
        {
            _options = options;
            _criteriaQueryDict = EnumExtensions.GetValues<RepositorySearchCriteria>()
                .ToDictionary(enumValue => enumValue, enumValue => enumValue.GetAttribute<EnumMemberAttribute>().Value);
        }

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public string BuildRepositorySearch(GithubRepositorySearchDto searchDto)
        {
            var query = GithubSpecifications.WithKeywords(searchDto.Keywords);

            if (searchDto.Criterias != null && searchDto.Criterias.Any())
            {
                var resultCriteria = searchDto.Criterias
                     .Select(criteriaDto => $"{_criteriaQueryDict[criteriaDto.Criteria]}:{string.Join(",", criteriaDto.Values)}")
                     .Aggregate((crt, nextCtr) => $"{crt}+{nextCtr}");

                query = query.WithCriteria(resultCriteria);
            }

            if (!string.IsNullOrWhiteSpace(searchDto.Order))
                query = query.WithOrder(searchDto.Order);

            if (!string.IsNullOrWhiteSpace(searchDto.Sort))
                query = query.WithSort(searchDto.Sort);

            return $"{_options.Value.Url}/search/repositories?{query}";
        }
    }
}
