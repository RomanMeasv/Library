namespace NextIT_RomanM.Core.Domain.Identity.Entities
{
    public class User
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
        public IList<string>? Roles { get; set; }
        public string? Token { get; set; }
    }
}
