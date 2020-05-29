namespace Auth.Application.Commands
{
    public class RefreshJwtCommand
    {
        public string Jwt { get; set; }
        public string RefreshToken { get; set; }
    }
}