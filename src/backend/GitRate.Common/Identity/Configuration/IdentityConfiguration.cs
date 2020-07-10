namespace GitRate.Common.Identity.Configuration
{
    /// <summary>
    /// Configuration for <see cref="Identity"/>
    /// </summary>
    public class IdentityConfiguration
    {
        /// <summary>
        /// Flag if password requires digit character
        /// </summary>
        public bool RequireDigit { get; set; } = false;

        /// <summary>
        /// Flag if password requires at least 6 characters
        /// </summary>
        public int RequiredLength { get; set; } = 6;

        /// <summary>
        /// Flag if password requires number of unique characters
        /// </summary>
        public int RequiredUniqueChars { get; set; } = 0;

        /// <summary>
        /// Flag if password requires lower case characters
        /// </summary>
        public bool RequireLowercase { get; set; } = false;

        /// <summary>
        /// Flag if password requires non alphanumeric character
        /// </summary>
        public bool RequireNonAlphanumeric { get; set; } = false;

        /// <summary>
        /// Flag if password requires upper case character
        /// </summary>
        public bool RequireUppercase { get; set; } = false;
    }
}