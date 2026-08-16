using JwtMusicProjectCase.Business.Abstract;
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
    public class PlaylistSongsController : ControllerBase
    {
        private readonly IPlaylistSongService _playlistSongService;

        public PlaylistSongsController(IPlaylistSongService playlistSongService)
        {
            _playlistSongService = playlistSongService;
        }

        [HttpGet("{playlistId}/songs")]
        public async Task<IActionResult> GetPlaylistSongs(int playlistId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var values = await _playlistSongService.GetPlaylistSongsAsync(playlistId, userId);
            return Ok(values);
        }

        [HttpPost("{playlistId}/songs/{songId}")]
        public async Task<IActionResult> AddSongToPlaylist(int playlistId, int songId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            await _playlistSongService.AddSongToPlaylistAsync(playlistId, songId, userId);
            return Ok("Song added to playlist successfully.");
        }

        [HttpDelete("{playlistId}/songs/{songId}")]
        public async Task<IActionResult> RemoveSongFromPlaylist(int playlistId, int songId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            await _playlistSongService.RemoveSongFromPlyalistAsync(playlistId, songId, userId);
            return Ok("Song removed from playlist successfully.");
        }
    }
}
