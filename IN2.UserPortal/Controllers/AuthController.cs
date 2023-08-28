using IN2.UserPortal.Core.Interfaces;
using IN2.UserPortal.Core.Models.DtoModels;
using IN2.UserPortal.Persistance.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IN2.UserPortal.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserLogInService _userLogInService;
        private readonly IUserRegisterService _userRegisterService;
        private readonly IActivationAccountService _activationAccountService;
        private readonly IResetPasswordService _resetPasswordService;

        public AuthController(IUserLogInService userLogInService, IUserRegisterService userRegisterService, IActivationAccountService activationAccountService, IResetPasswordService resetPasswordService, IEmailSender emailSender)
        {
            _userLogInService = userLogInService;
            _userRegisterService = userRegisterService;
            _activationAccountService = activationAccountService;
            _resetPasswordService = resetPasswordService;
        }

        [HttpGet]
        public ActionResult<string> GetMe()
        {
            var userName = User?.Identity?.Name;
            return Ok(userName);
        }

        [Authorize]
        [HttpPost("register")]
        public async Task<ActionResult<UserModel>> Register(UserRegisterDto request)
        {

            try
            {
                var administratorId = HttpContext?.User.Claims.Where(x => x.Type == "UserId").Single();
                var response = await _userRegisterService.UserRegister(request, int.Parse(administratorId.Value));
                return Ok(response);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            
        }
        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(UserLoginDto request)
        {
            try
            {
                var response = await _userLogInService.UserLogin(request);
                return Ok(response);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("activateAccount")]
        public async Task<ActionResult<UserModel>> ActivationAccount(ActivationAccountDto request)
        {
            try
            {
                var response = await _activationAccountService.ActivationAccount(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            
        }

        [HttpPost("resetPassword")]
        public async Task<ActionResult<UserModel>> ResetPassword(ResetPasswordDto request)
        {
            try
            {
                var response = await _resetPasswordService.ResetPassword(request);
                return Ok(response);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            
        }


        [HttpGet("IsActivationTokenValid")]
        public async Task<ActionResult<UserModel>> IsActivationTokenValid(string? activationToken)
        {
            try
            {
                var response = await _activationAccountService.IsActivationTokenValid(activationToken);
                return Ok(response);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
