using FluentValidation;
using NextIT_RomanM.Core.Domain.Dto.Borrowed;

namespace NextIT_RomanM.Core.Domain.Dto.Book
{
    public class UpdateBookDto
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
        public required string Author { get; set; }
        public required BorrowedDto Borrowed { get; set; }   
    }
    public class UpdateBookDtoValidator : AbstractValidator<UpdateBookDto>
    {
        public UpdateBookDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(15);
            RuleFor(x => x.Author).NotEmpty();
        }
    }
}
