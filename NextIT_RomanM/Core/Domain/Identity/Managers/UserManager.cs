using Microsoft.Extensions.Options;
using NextIT_RomanM.Core.Configuration;
using NextIT_RomanM.Core.Domain.Identity.Entities;

namespace NextIT_RomanM.Core.Domain.Identity.Managers
{
    public class UserManager
    {
        private readonly IOptions<UserSettings> _userSettings;

        public UserManager(IOptions<UserSettings> userSettings)
        {
            _userSettings = userSettings;
        }

        public User? FindByUsername(string username)
        {
            if (username == _userSettings.Value.Username)
            {
                return new User
                {
                    Username = _userSettings.Value.Username,
                    Password = _userSettings.Value.Password
                };
            }
            else return null;
        }
    }
}
