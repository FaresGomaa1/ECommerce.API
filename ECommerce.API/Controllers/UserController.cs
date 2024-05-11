using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ECommerce.API.DTOs.Users;
using ECommerce.API.Repositories;
using ECommerce.API.ECommerce.Application.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserRepository userRepository, ILogger<UserController> logger)
        {
            _userRepository = userRepository;
            _logger = logger;   
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterDTO newUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _userRepository.CreateUserAsync(newUser, "customer");

                if (result.Succeeded)
                {
                    return Ok(new { message = "User created successfully" });
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
            catch (Exception ex)
            {
                // Log the exception for further analysis
                _logger.LogError(ex, "An error occurred while registering a user.");

                // Return a generic error message
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while registering a user.");
            }
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLogInDTO user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                // Authenticate user and generate token
                var result = await _userRepository.GenerateToken(user);
                return result;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }
    }
}
