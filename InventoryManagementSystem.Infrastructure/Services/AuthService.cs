﻿using InventoryManagementSystem.Domain.DTOs.Account;
using InventoryManagementSystem.Domain.DTOs.Response;
using InventoryManagementSystem.Domain.Helpers;
using InventoryManagementSystem.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using InventoryManagementSystem.Domain.Constants;

namespace InventoryManagementSystem.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IRoleService _roleService;
        private readonly JWTSettings _jwt;
        

        public AuthService(
            UserManager<ApplicationUser> userManager, 
            IOptions<JWTSettings> jwt,
            IRoleService roleService
            )
        {
            _userManager = userManager;
            _roleService = roleService;
            _jwt = jwt.Value;


        }

        

        private async Task<JwtSecurityToken> CreateJWT(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
            {
                roleClaims.Add(new Claim("roles", role));
                foreach (var claim in _roleService.GetRoleClaimsPermissions(role).Result)
                    if(roleClaims.FirstOrDefault(i=>i.Value == claim) == null)
                        roleClaims.Add(new Claim(OtherConstants.Permissions.ToString(), claim));
            }



            var Claims = new List<Claim>
                {
                       new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                       new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                       new Claim(JwtRegisteredClaimNames.Email, user.Email),
                       new Claim(ClaimTypes.NameIdentifier, user.Id),
                       new Claim("userId", user.Id),
                }
            .Union(userClaims)
            .Union(roleClaims);
            


            SigningCredentials credentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.SECRETKEY)),
                SecurityAlgorithms.HmacSha256
                );


            return new JwtSecurityToken(
                    claims: Claims,
                    signingCredentials: credentials,
                    issuer: _jwt.Issuer,
                    audience: _jwt.Audience,
                    expires: DateTime.Now.AddDays(_jwt.DurationInDays)
                );
        }

        public async Task<ApplicationUser> AddUser(RegisterDTO userDTO)
        {
            if (await _userManager.FindByNameAsync(userDTO.Username) != null)
                return null;

            if (await _userManager.FindByEmailAsync(userDTO.Email) != null)
                return null;
            ApplicationUser user = new ApplicationUser
            {
                Email = userDTO.Email,
                PhoneNumber = userDTO.PhoneNumber,
                UserName = userDTO.Username,
            };
            var result = await _userManager.CreateAsync(user, userDTO.Password);
            if (result.Succeeded)        
                return user;
            return null;
        }

        public async Task<BaseResponse> Register(RegisterDTO userDTO)
        {
            if (await _userManager.FindByNameAsync(userDTO.Username) != null)
                return new BaseResponse { Message = userDTO.Username + " is already exists !" };

            if (await _userManager.FindByEmailAsync(userDTO.Email) != null)
                return new BaseResponse { Message = userDTO.Email + " is already exists !" };


            ApplicationUser user = await AddUser(userDTO);
            if (user == null)
                return new BaseResponse { Message = $"something went wrong while creating {userDTO.Username}" };

            var roleResult = await _userManager.AddToRoleAsync(user, "Basic");
            if (!roleResult.Succeeded)
                return new BaseResponse { Message = $"something went wrong while completing your account data" };

            JwtSecurityToken token = await CreateJWT(user);

            return new RegisterationResponse
            {
                isAuthenticated = true,
                Message = "Account Created Successfully",
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                UserEmail = userDTO.Email,
                UserName = userDTO.Username
            };

        }

        public async Task<BaseResponse> Login(LoginDTO loginDTO)
        {
            ApplicationUser user;
            if (loginDTO.Username.IsEmail())
                user = await _userManager.FindByEmailAsync(loginDTO.Username);
            else
                user = await _userManager.FindByNameAsync(loginDTO.Username);

            if (user == null || !(await _userManager.CheckPasswordAsync(user, loginDTO.Password)))
                return new BaseResponse { isAuthenticated = false, Message = "User or Password is Invalid" };

            JwtSecurityToken token = await CreateJWT(user);
            return new AuthResponse
            {
                isAuthenticated = true,
                Message = user.UserName + " LoggedIn Successfully",
                Token = new JwtSecurityTokenHandler().WriteToken(token)
            };
        }

        
    }
}
