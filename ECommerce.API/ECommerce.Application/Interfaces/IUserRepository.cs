using ECommerce.API.DTOs.Users;
using Microsoft.AspNetCore.Identity;

namespace ECommerce.API.ECommerce.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<IdentityResult> CreateUserAsync(UserRegisterDTO newUser);
    }
}
