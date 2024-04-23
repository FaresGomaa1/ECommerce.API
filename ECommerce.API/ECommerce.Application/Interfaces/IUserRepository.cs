using ECommerce.API.DTOs.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.ECommerce.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<IdentityResult> CreateUserAsync(UserRegisterDTO newUser);
        Task<IActionResult> GenerateToken(UserLogInDTO user);
    }
}
