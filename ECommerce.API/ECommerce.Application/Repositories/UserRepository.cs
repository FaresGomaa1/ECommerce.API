﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ECommerce.API.DTOs.Users;
using ECommerce.API.ECommerce.Domain.Model;
using ECommerce.API.ECommerce.Application.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace ECommerce.API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public UserRepository(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        public async Task<IdentityResult> CreateUserAsync(UserRegisterDTO newUser, string roleName)
        {
            try
            {
                var userModel = new ApplicationUser
                {
                    Email = newUser.Email,
                    FullName = newUser.FullName,
                    UserName = newUser.Email,
                    PhoneNumber = newUser.Phone,
                    Address = $"{newUser.City}-{newUser.Street}-{newUser.State}"
                };

                // Create the user
                IdentityResult result = await _userManager.CreateAsync(userModel, newUser.Password);

                // If user creation is successful and a role name is provided, assign the role to the user
                if (result.Succeeded && !string.IsNullOrEmpty(roleName))
                {
                    // Check if the role exists, if not create it
                    if (!await _roleManager.RoleExistsAsync(roleName))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(roleName));
                    }

                    // Assign the role to the user
                    await _userManager.AddToRoleAsync(userModel, roleName);
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<IActionResult> GenerateToken(UserLogInDTO user)
        {
            // Find user by userName
            ApplicationUser userModel = await _userManager.FindByNameAsync(user.Email);

            // Check if user exists and password is correct
            if (userModel != null && await _userManager.CheckPasswordAsync(userModel, user.Password))
            {
                // Create claims
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, userModel.Id),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                // Get roles
                var roles = await _userManager.GetRolesAsync(userModel);
                if (roles != null)
                {
                    foreach (var role in roles)
                    {
                        // Add each role as a claim
                        claims.Add(new Claim(ClaimTypes.Role, role));
                    }
                }

                // Create symmetric security key
                var authSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("HelloFromECommerceWebsite5HelloFromECommerceWebsite5"));

                // Create signing credentials
                var credentials = new SigningCredentials(authSecurityKey, SecurityAlgorithms.HmacSha256);

                // Create JWT token
                var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:ValidIss"],
                    audience: _configuration["Jwt:ValidAud"],
                    claims: claims,
                    expires: DateTime.UtcNow.AddHours(3),
                    signingCredentials: credentials
                );

                // Return token as a string and its expiration time
                return new OkObjectResult(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            else
            {
                // Return error message if email or password is incorrect
                return new UnauthorizedObjectResult(new { error = "Email or password is incorrect" });
            }
        }
    }
}