using JwtMusicProjectCase.Business.Dtos.RoleDtos;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtMusicProjectCase.Business.Abstract
{
    public interface IRoleService
    {
        Task<IdentityResult> TCreateRoleAsync(CreateRoleDto dto);
        Task<IdentityResult> TAssignRoleAsync(AssignRoleDto dto);
        Task<bool> TRoleExistsAsync(string roleName);
        Task<List<string>> TGetAllRolesAsync();
    }
}
