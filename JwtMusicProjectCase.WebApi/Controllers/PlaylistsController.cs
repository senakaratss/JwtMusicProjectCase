using JwtMusicProjectCase.Business.Abstract;
using JwtMusicProjectCase.Business.Dtos.PlaylistDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace JwtMusicProjectCase.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PlaylistsController : ControllerBase
    {
        private readonly IPlaylistService _playlistService;

        public PlaylistsController(IPlaylistService playlistService)
        {
            _playlistService = playlistService;
        }
        [HttpGet]
        public async Task<IActionResult> GetUserPlaylists()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var values = await _playlistService.TGetAllAsync(userId);
            return Ok(values);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserPlaylistById(int id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var value = await _playlistService.TGetByIdAsync(id, userId);
            if(value== null)
            {
                return NotFound();
            }
            return Ok(value);
        }
        [HttpPost]
        public async Task<IActionResult> CreateUserPlaylist(CreatePlaylistDto createPlaylistDto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized();
            }
            await _playlistService.TCreateAsync(createPlaylistDto, userId);
            return Ok("Playlist oluşturuldu.");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserPlaylist(int id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            await _playlistService.TDeleteAsync(id, userId);
            return Ok("Playlist silindi.");
        }
        [HttpPut]
        public async Task<IActionResult> UpdateUserPlaylist(UpdatePlaylistDto updatePlaylistDto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            await _playlistService.TUpdateAsync(updatePlaylistDto, userId);
            return Ok("Playlist güncellendi.");
        }
    }
}
