namespace NextIT_RomanM.Core.Domain.Exceptions
{
    public class BookNotFoundException : AppException
    {
        public BookNotFoundException(string error) : base(error)
        {

        }
        public BookNotFoundException() : base("Book not found.")
        {

        }
    }
}
