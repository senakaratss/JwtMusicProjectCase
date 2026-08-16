using JwtMusicProjectCase.Business.Dtos.AppUserDtos;
using JwtMusicProjectCase.Business.Dtos.AuthDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtMusicProjectCase.Business.Abstract
{
    public interface IAuthService
    {
        Task<bool> RegisterAsync(RegisterDto registerDto);
        Task<string> LoginAsync(LoginDto loginDto);
        Task<UserProfileDto> GetUserProfile(string userId);
        Task UpdateProfileAsync(string userId, UpdateProfileDto updateProfileDto);
        Task<bool> ChangePasswordAsync(string userId, ChangePasswordDto dto);
    }
}
