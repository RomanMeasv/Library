namespace NextIT_RomanM.Core.Configuration
{
    public class UserSettings
    {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public List<string> Roles { get; set; } = null!;
    }
}
