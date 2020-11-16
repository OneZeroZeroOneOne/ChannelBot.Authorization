using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using ChannelBot.DAL.Contexts;
using ChannelBot.DAL.Models;
using Microsoft.EntityFrameworkCore;
using ChannelBot.BLL.Options;
using Microsoft.IdentityModel.Tokens;
using ChannelBot.BLL.Abstractions;
using ChannelBot.Utilities.Exceptions;


namespace ChannelBot.BLL.Services
{
    public class AuthService: IAuthService
    {
        private MainContext _context;
        public AuthService(MainContext context)
        {
            _context = context;
        }

        async public Task<ClaimsIdentity> GetIdentity(string login, string password)
        {
            Admin admin = await _context.Admin.FirstOrDefaultAsync(x => x.Email == login);
            if (admin == null || admin.Email != login || admin.Password != password)
            {
                throw ExceptionFactory.SoftException(ExceptionEnum.InvalidCredentials, "invalid email or password");
            }
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, admin.Id.ToString()),
                new Claim(ClaimsIdentity.DefaultNameClaimType, admin.Role.ToString()),
            };
            return new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
        }

        public string GenerateToken(ClaimsIdentity claims)
        {
            var now = DateTime.UtcNow;

            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                notBefore: now,
                claims: claims.Claims,
                expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            return "Bearer " + new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}
