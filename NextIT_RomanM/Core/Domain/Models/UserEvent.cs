namespace NextIT_RomanM.Core.Domain.Models
{
    public class UserEvent
    {
        public required string Username { get; set; }
        public List<KeyValuePair<string, object>> Params { get; set; } = new();
        public required DateTimeOffset RequiredAt { get; set; }
    }
}
