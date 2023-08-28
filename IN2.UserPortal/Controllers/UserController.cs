using IN2.UserPortal.Core.Interfaces;
using IN2.UserPortal.Core.Models.DtoModels;
using IN2.UserPortal.Persistance.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IN2.UserPortal.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserAccountService _service;

        private readonly IUserRepository _userRepository;

        public UserController(IUserAccountService service, IUserRepository userRepository)
        {
            _service = service;
            _userRepository = userRepository;
        }

        [HttpGet("GetUsers")]
        public async Task<IActionResult> GetUsers(string userHospital)
        {
            try
            {
                var userRoleId = HttpContext?.User.Claims.Where(x => x.Type == "UserRoleId").Single();
                var users = await _userRepository.GetAllUsersHospital(userHospital, int.Parse(userRoleId.Value));
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpGet("{id}", Name = "UserById")]
        public async Task<IActionResult> GetUser(int id)
        {
            try
            {
                var user = await _service.GetUserById(id);
                if (user == null)
                    return BadRequest();
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpPut("updateUser")]
        public async Task<IActionResult> UpdateUser(UserDto request)
        {
            try
            {
                var userUpdateResponse = await _service.UpdateUser(request);

                return Ok(userUpdateResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPut("activateDeactivateUser")]
        public async Task<IActionResult> ActivateDeactivateUser(UserDto request)
        {
            try
            {
                var user = await _service.ActivateDeactivateUser(request);
                return Ok(user);
            }catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            
        }

        [HttpDelete("deleteUser")]
        public async Task<IActionResult> DeleteUser(UserDto request)
        {
            try
            {
                var user = await _service.DeleteUser(request);
                return Ok(user);
            }catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            
        }

        [HttpGet("GetHospitals")]
        public async Task<IActionResult> GetHospitals()
        {
            try
            {
                var hospitalProducts = await _userRepository.GetHospitals();
                return Ok(hospitalProducts);

            }catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            
        }

    }
}
