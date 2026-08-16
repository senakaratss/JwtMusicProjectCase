using JwtMusicProjectCase.Business.Abstract;
using JwtMusicProjectCase.Business.Dtos.GenreDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace JwtMusicProjectCase.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGenreService _genreService;

        public GenresController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        [HttpGet]
        public async Task<IActionResult> GenreList()
        {
            var values = await _genreService.TGetAllAsync();
            return Ok(values);
        }
        [HttpGet("GetGenreById/{id}")]
        public async Task<IActionResult> GetGenreById(int id)
        {
            var value = await _genreService.TGetByIdAsync(id);
            return Ok(value);
        }

        [Authorize(Roles ="Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateGenre(CreateGenreDto createGenreDto)
        {
            await _genreService.TCreateAsync(createGenreDto);
            return Ok("Ekleme işlemi başarılı");
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> UpdateGenre(UpdateGenreDto updateGenreDto)
        {
            await _genreService.TUpdateAsync(updateGenreDto);
            return Ok("Güncelleme işlemi başarılı");
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGenre(int id)
        {
            await _genreService.TDeleteAsync(id);
            return Ok("Silme işlemi başarılı");
        }
    }
}
