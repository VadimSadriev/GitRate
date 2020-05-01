namespace GitRate.Common.Identity.Configuration
{
    public class IdentityConfiguration
    {
        public bool RequireDigit { get; set; } = false;

        public int RequiredLength { get; set; } = 6;

        public int RequiredUniqueChars { get; set; } = 0;

        public bool RequireLowercase { get; set; } = false;

        public bool RequireNonAlphanumeric { get; set; } = false;

        public bool RequireUppercase { get; set; } = false;
    }
}