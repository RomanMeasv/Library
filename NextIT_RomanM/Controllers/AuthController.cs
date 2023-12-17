using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NextIT_RomanM.Core.Application.Services;
using NextIT_RomanM.Core.Domain.Constants;
using NextIT_RomanM.Core.Domain.Dto.Auth;
using NextIT_RomanM.Core.Domain.Exceptions;
using NextIT_RomanM.Core.Domain.Identity.Managers;
using System.ComponentModel.DataAnnotations;

namespace NextIT_RomanM.Controllers
{
    public class AuthController : BaseController
    {
        private readonly UserManager _userManager;
        private readonly SignInManager _signInManager;
        private readonly IMapper _mapper;
        private readonly UserEventTracker _userEventTracker;

        public AuthController(UserManager userManager, SignInManager signInManager, IMapper mapper, UserEventTracker userEventTracker)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _userEventTracker = userEventTracker;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LoginSuccessDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Login([FromBody][Required] LoginDto dto) 
        {
            var user = _userManager.FindByUsername(dto.Username);
            if(user is null)
            {
                return BadRequest(new LoginException());
            }

            if (_signInManager.CheckPassword(dto.Password, out user))
            {
                _userEventTracker.TrackEvent(UserEvents.Auth.LOGIN_SUCCESS);
                return Ok(_mapper.Map<LoginSuccessDto>(user));
            }
            else return BadRequest(new LoginException());

        }
    }
}
