using FluentValidation;
using NextIT_RomanM.Core.Domain.Dto.Borrowed;

namespace NextIT_RomanM.Core.Domain.Dto.Book
{
    public class NewBookDto
    {
        public required string Name { get; set; }
        public required string Author { get; set; }
        public required BorrowedDto Borrowed { get; set; }
    }
    public class NewBookDtoValidator : AbstractValidator<NewBookDto> 
    { 
        public NewBookDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(15);
            RuleFor(x => x.Author).NotEmpty();
        }
    }
}
