using FluentValidation;
using JwtMusicProjectCase.Business.Abstract;
using JwtMusicProjectCase.Business.Dtos.AppUserDtos;
using JwtMusicProjectCase.Business.Dtos.AuthDtos;
using JwtMusicProjectCase.Entity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtMusicProjectCase.Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IJwtService _jwtService;
        private readonly IValidator<RegisterDto> _validator;

        public AuthManager(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IJwtService jwtService, IValidator<RegisterDto> validator)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtService = jwtService;
            _validator = validator;
        }

        public async Task<bool> ChangePasswordAsync(string userId, ChangePasswordDto dto)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new Exception("Kullanıcı bulunamadı.");
            }
            if (dto.NewPassword != dto.ConfirmPassword)
            {
                throw new Exception("Yeni şifreler eşleşmiyor.");
            }
            var result = await _userManager.ChangePasswordAsync(user, dto.CurrentPassword, dto.NewPassword);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(x => x.Description));
                throw new Exception(errors);
            }
            return true;
        }

        public async Task<UserProfileDto> GetUserProfile(string userId)
        {
            var user = await _userManager.Users.Include(x => x.Package).FirstOrDefaultAsync(x => x.Id == userId);
            if (user == null)
            {
                throw new Exception("Kullanıcı bulunamadı.");
            }
            return new UserProfileDto
            {
                Name = user.Name,
                Surname = user.Surname,
                Username = user.UserName,
                ImageUrl = user?.ImageUrl,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                PackageId = user.PackageId,
                PackageName = user.Package.PackageName
            };
        }

        public async Task<string> LoginAsync(LoginDto loginDto)
        {
            var user = await _userManager.FindByNameAsync(loginDto.Username);
            if (user == null)
            {
                throw new Exception("Kullanıcı adı veya şifre hatalı.");
            }
            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded)
            {
                throw new Exception("Kullanıcı adı veya şifre hatalı.");
            }
            var token = await _jwtService.GenerateToken(user);
            return token;
        }

        public async Task<bool> RegisterAsync(RegisterDto registerDto)
        {
            //fluent validationa taşı + existingEmail,existingUsername kontrolü
            var validationResult = await _validator.ValidateAsync(registerDto);
            if (!validationResult.IsValid)
            {
                var errors = string.Join(" ", validationResult.Errors.Select(x => x.ErrorMessage));
                throw new Exception(errors);
            }

            var existingUsername = await _userManager.FindByNameAsync(registerDto.Username);
            if (existingUsername != null)
            {
                throw new Exception("Bu kullanıcı adı zaten kullanılıyor.");
            }
            var existingEmail = await _userManager.FindByEmailAsync(registerDto.Email);
            if (existingEmail != null)
            {
                throw new Exception("Bu email adresi zaten kullanılıyor.");
            }

            var appUser = new AppUser
            {
                Name = registerDto.Name,
                Surname = registerDto.Surname,
                UserName = registerDto.Username,
                Email = registerDto.Email,
                PhoneNumber = registerDto.PhoneNumber,
                PackageId = 1
            };
            var result = await _userManager.CreateAsync(appUser, registerDto.Password);

            if (!result.Succeeded)
            {
                var errors = string.Join(" ", result.Errors.Select(x => x.Description));
                throw new Exception(errors);
            }

            var roleResult = await _userManager.AddToRoleAsync(appUser, "User");
            if (!roleResult.Succeeded)
            {
                throw new Exception("Rol atanırken hata oluştu");
            }

            return true;
        }

        public async Task UpdateProfileAsync(string userId, UpdateProfileDto updateProfileDto)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new Exception("Kullanıcı bulunamadı");
            }

            var existingUsername = await _userManager.FindByNameAsync(updateProfileDto.Username);
            if (existingUsername != null && existingUsername.Id != userId)
            {
                throw new Exception("Bu kullanıcı adı zaten kullanılıyor.");
            }
            var existingEmail = await _userManager.FindByEmailAsync(updateProfileDto.Email);
            if (existingEmail != null && existingEmail.Id != userId)
            {
                throw new Exception("Bu email zaten kullanılıyor.");
            }
            user.Name = updateProfileDto.Name;
            user.Surname = updateProfileDto.Surname;
            user.UserName = updateProfileDto.Username;
            user.Email = updateProfileDto.Email;
            user.PhoneNumber = updateProfileDto.PhoneNumber;

            await _userManager.UpdateAsync(user);
        }
    }
}
