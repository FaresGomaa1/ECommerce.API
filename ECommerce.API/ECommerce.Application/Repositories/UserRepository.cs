using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;
using ECommerce.API.DTOs.Users;
using ECommerce.API.ECommerce.Domain.Model;
using ECommerce.API.ECommerce.Application.Interfaces;

namespace ECommerce.API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserRepository(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IdentityResult> CreateUserAsync(UserRegisterDTO newUser)
        {
            try
            {
                var userModel = new ApplicationUser
                {
                    Email = newUser.Email,
                    FullName = newUser.FullName,
                    UserName = newUser.Email,
                    Address = $"{newUser.City}-{newUser.Street}-{newUser.State}"
                };
                IdentityResult result = await _userManager.CreateAsync(userModel, newUser.Password);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}