namespace NextIT_RomanM.Core.Domain.Exceptions
{
    public class AppException : Exception
    {
        public AppException(string error) : base(error) { }
    }
}
