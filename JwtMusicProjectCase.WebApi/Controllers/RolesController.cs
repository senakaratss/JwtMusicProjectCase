using JwtMusicProjectCase.Business.Abstract;
using JwtMusicProjectCase.Business.Dtos.RoleDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace JwtMusicProjectCase.WebApi.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }
        [HttpGet]
        public async Task<IActionResult> RoleList()
        {
            var values = await _roleService.TGetAllRolesAsync();
            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleDto createRoleDto)
        {
            var result = await _roleService.TCreateRoleAsync(createRoleDto);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
            return Ok("Role created successfully.");
        }
        [HttpPost("assign")]
        public async Task<IActionResult> AssignRole(AssignRoleDto assignRoleDto)
        {
            var result = await _roleService.TAssignRoleAsync(assignRoleDto);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
            return Ok("Role assigned to user successfully");
        }
    }
}
