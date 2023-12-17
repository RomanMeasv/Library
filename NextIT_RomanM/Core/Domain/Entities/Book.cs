namespace NextIT_RomanM.Core.Domain.Entities
{
    public class Book
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
        public required string Author { get; set; }
        public required Borrowed Borrowed { get; set; }
    }
}
