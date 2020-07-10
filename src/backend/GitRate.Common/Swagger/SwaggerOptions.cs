namespace GitRate.Common.Swagger
{
    /// <summary>
    /// Swagger options
    /// </summary>
    public class SwaggerOptions
    {
        /// <summary>
        /// Flag if swagger is enabled
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// Title of swagger ui
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Version of api
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// Route prefix for swagger endpoint
        /// </summary>
        public string RoutePrefix { get; set; }

        /// <summary>
        /// Flag if security option enabled for swagger ui
        /// </summary>
        public bool IncludeSecurity { get; set; }
    }
}
