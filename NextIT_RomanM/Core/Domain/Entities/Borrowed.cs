namespace NextIT_RomanM.Core.Domain.Entities
{
    public class Borrowed
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        // Tu som dlho rozmýšlal či použiť DateTimeOffset alebo string
        public string From { get; set; } = default!;
    }
}
