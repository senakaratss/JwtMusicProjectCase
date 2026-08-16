using JwtMusicProjectCase.Business.Abstract;
using JwtMusicProjectCase.Business.Dtos.ArtistDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace JwtMusicProjectCase.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistsController : ControllerBase
    {
        private readonly IArtistService _artistService;

        public ArtistsController(IArtistService artistService)
        {
            _artistService = artistService;
        }

        [HttpGet]
        public async Task<IActionResult> ArtistList()
        {
            var values = await _artistService.TGetAllAsync();
            return Ok(values);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetArtistById(int id)
        {
            var value = await _artistService.TGetArtistWithSongCountAsync(id);
            return Ok(value);
        }

        [Authorize(Roles ="Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArtist(int id)
        {
            await _artistService.TDeleteAsync(id);
            return Ok("Silme işlemi başarılı");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateArtist(CreateArtistDto createArtistDto)
        {
            await _artistService.TCreateAsync(createArtistDto);
            return Ok("Ekleme işlemi başarılı");
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> UpdateArtist(UpdateArtistDto updateArtistDto)
        {
            await _artistService.TUpdateAsync(updateArtistDto);
            return Ok("Güncelleme işlemi başarılı");
        }
    }
}
