namespace NextIT_RomanM.Core.Domain.Exceptions
{
    public class LoginException : AppException
    {
        public LoginException(string error) : base(error) 
        {
            
        }
        public LoginException() : base("Wrong username or password.")
        {

        }
    }
}
