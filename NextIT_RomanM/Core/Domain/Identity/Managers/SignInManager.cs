using Microsoft.Extensions.Options;
using NextIT_RomanM.Core.Configuration;
using NextIT_RomanM.Core.Domain.Identity.Entities;

namespace NextIT_RomanM.Core.Domain.Identity.Managers
{
    public class SignInManager
    {
        private readonly IOptions<UserSettings> _userSettings;
        private const string token = "123";

        public SignInManager(IOptions<UserSettings> userSettings)
        {
            _userSettings = userSettings;
        }

        public bool CheckPassword(string password, out User? user)
        {
            if (password == _userSettings.Value.Password)
            {
                user = new User
                {
                    Username = _userSettings.Value.Username,
                    Password = _userSettings.Value.Password,
                    Roles = GetUserRoles(),
                    Token = token,
                };
                return true;
            }
            else
            {
                user = null;
                return false;
            }
        }

        private List<string> GetUserRoles()
        {
            return _userSettings.Value.Roles;
        }
    }
}
