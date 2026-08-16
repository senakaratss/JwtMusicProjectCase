using JwtMusicProjectCase.Business.Abstract;
using JwtMusicProjectCase.Business.Dtos.PackageDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace JwtMusicProjectCase.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackagesController : ControllerBase
    {
        private readonly IPackageService _packageService;

        public PackagesController(IPackageService packageService)
        {
            _packageService = packageService;
        }
        [HttpGet]
        public async Task<IActionResult> GetPackageList()
        {
            var values = await _packageService.TGetAllAsync();
            return Ok(values);
        }
        [HttpGet("GetPackageById/{id}")]
        public async Task<IActionResult> GetPackageById(int id)
        {
            var value = await _packageService.TGetByIdAsync(id);
            return Ok(value);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePackage(int id)
        {
            await _packageService.TDeleteAsync(id);
            return Ok("Silme işlemi başarılı");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreatePackage(CreatePackageDto createPackageDto)
        {
            await _packageService.TCreateAsync(createPackageDto);
            return Ok("Ekleme işlemi başarılı");
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> UpdatePackage(UpdatePackageDto updatePackageDto)
        {
            await _packageService.TUpdateAsync(updatePackageDto);
            return Ok("Güncelleme işlemi başarılı");
        }
        [Authorize]
        [HttpPut("change-package/{packageId}")]
        public async Task<IActionResult> ChangePackage(int packageId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            await _packageService.ChangePackageAsync(userId, packageId);
            return Ok("Paketiniz başarıyla güncellendi.");
        }
    }
}
