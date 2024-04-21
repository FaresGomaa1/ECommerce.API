using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ECommerce.API.DTOs.Users;
using ECommerce.API.Repositories;
using ECommerce.API.ECommerce.Application.Interfaces;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterDTO newUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _userRepository.CreateUserAsync(newUser);

            if (result.Succeeded)
            {
                return Ok("User created successfully");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);
            }

        }
    }
}
