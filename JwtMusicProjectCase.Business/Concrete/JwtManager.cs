using JwtMusicProjectCase.Business.Abstract;
using JwtMusicProjectCase.Entity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace JwtMusicProjectCase.Business.Concrete
{
    public class JwtManager : IJwtService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<AppUser> _userManager;
        private readonly IPackageService _packageService;

        public JwtManager(IConfiguration configuration, UserManager<AppUser> userManager, IPackageService packageService)
        {
            _configuration = configuration;
            _userManager = userManager;
            _packageService = packageService;
        }

        public async Task<string> GenerateToken(AppUser appUser)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");

            var roles = await _userManager.GetRolesAsync(appUser);
            var userPackage = await _packageService.TGetByIdAsync(appUser.PackageId);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,appUser.Id),
                new Claim(ClaimTypes.Email,appUser.Email),
                new Claim(ClaimTypes.Name,appUser.Name),
                new Claim(ClaimTypes.Surname,appUser.Surname),

                new Claim("PackageId",appUser.PackageId.ToString()),
                new Claim("PackageName",userPackage.PackageName),
                new Claim("PackageLevel",userPackage.PackageLevel.ToString()),
            };
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]!));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims:claims,
                expires: DateTime.UtcNow.AddMinutes(int.Parse(jwtSettings["ExpireMinutes"]!)),
                signingCredentials:credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
