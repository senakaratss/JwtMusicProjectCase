using JwtMusicProjectCase.Business.Abstract;
using JwtMusicProjectCase.Business.Dtos.RoleDtos;
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
    public class RoleManager : IRoleService
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;

        public RoleManager(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<IdentityResult> TAssignRoleAsync(AssignRoleDto dto)
        {
            var user = await _userManager.FindByIdAsync(dto.UserId);
            if (user == null)
            {
                return IdentityResult.Failed(
                    new IdentityError
                    {
                        Description = "User not found"
                    });
            }

            var roleExists = await _roleManager.RoleExistsAsync(dto.RoleName);
            if (!roleExists)
            {
                return IdentityResult.Failed(
                    new IdentityError
                    {
                        Description = "Role not found."
                    });
            }
            var currentRoles = await _userManager.GetRolesAsync(user);
            if (currentRoles.Any())
            {
                var removeResult = await _userManager.RemoveFromRolesAsync(user, currentRoles);
                if (!removeResult.Succeeded)
                    return removeResult;
            }
            return await _userManager.AddToRoleAsync(user, dto.RoleName);
        }

        public async Task<IdentityResult> TCreateRoleAsync(CreateRoleDto dto)
        {
            var roleExists = await _roleManager.RoleExistsAsync(dto.RoleName);
            if (roleExists)
            {
                return IdentityResult.Failed(
                    new IdentityError
                    {
                        Description = "Role already exist"
                    });
            }
            return await _roleManager.CreateAsync(new IdentityRole(dto.RoleName));

        }

        public async Task<List<string>> TGetAllRolesAsync()
        {
            return await _roleManager.Roles.Select(x => x.Name).ToListAsync();
        }

        public async Task<bool> TRoleExistsAsync(string roleName)
        {
            return await _roleManager.RoleExistsAsync(roleName);
        }
    }
}
