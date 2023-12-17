namespace NextIT_RomanM.Core.Domain.Dto.Auth
{
    public class LoginSuccessDto
    {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public IList<string>? Roles { get; set; }
        public string Token { get; set; } = null!;
    }
}
