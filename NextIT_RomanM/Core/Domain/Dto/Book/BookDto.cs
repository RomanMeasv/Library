using NextIT_RomanM.Core.Domain.Dto.Borrowed;

namespace NextIT_RomanM.Core.Domain.Dto.Book
{
    public class BookDto
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
        public required string Author { get; set; }
        public required BorrowedDto Borrowed { get; set; }
    }
}
