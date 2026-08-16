using JwtMusicProjectCase.Business.Abstract;
using JwtMusicProjectCase.Business.Dtos.SongDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace JwtMusicProjectCase.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongsController : ControllerBase
    {
        private readonly ISongService _songService;
        private readonly IListeningHistoryService _listeningHistoryService;
        private readonly IWebHostEnvironment _environment;

        public SongsController(ISongService songService, IWebHostEnvironment environment, IListeningHistoryService listeningHistoryService)
        {
            _songService = songService;
            _environment = environment;
            _listeningHistoryService = listeningHistoryService;
        }
        [HttpGet]
        public async Task<IActionResult> SongList()
        {
            var values = await _songService.TGetAllAsync();
            return Ok(values);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSongById(int id)
        {
            var value = await _songService.TGetByIdAsync(id);
            return Ok(value);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateSong([FromForm] CreateSongDto createSongDto)
        {
            if (createSongDto.AudioFile != null)
            {
                var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "Audio");
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(createSongDto.AudioFile.FileName);
                var filePath = Path.Combine(folderPath, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await createSongDto.AudioFile.CopyToAsync(stream);
                }
                createSongDto.AudioUrl = "/Audio/" + fileName;
            }
            await _songService.TCreateAsync(createSongDto);
            return Ok("Ekleme işlemi başarılı");
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> UpdateSong([FromForm] UpdateSongDto updateSongDto)
        {
            var song = await _songService.TGetByIdAsync(updateSongDto.SongId);
            if (song == null)
            {
                return NotFound("Şarkı bulunamadı.");
            }
            if (updateSongDto.AudioFile != null)
            {
                //eski dosyayı sil
                if (!string.IsNullOrEmpty(song.AudioUrl))
                {
                    var oldFileName = Path.GetFileName(song.AudioUrl);
                    var oldFilePath = Path.Combine(_environment.ContentRootPath, "Audio", oldFileName);
                    if (System.IO.File.Exists(oldFilePath))
                    {
                        System.IO.File.Delete(oldFilePath);
                    }
                }
                //yeni dosyayı kaydet
                var folderPath = Path.Combine(_environment.ContentRootPath, "Audio");
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(updateSongDto.AudioFile.FileName);
                var filePath = Path.Combine(folderPath, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await updateSongDto.AudioFile.CopyToAsync(stream);
                }
                updateSongDto.AudioUrl = "/Audio/" + fileName;
            }
            else
            {
                updateSongDto.AudioUrl = song.AudioUrl;
            }
            await _songService.TUpdateAsync(updateSongDto);
            return Ok("Güncelleme işlemi başarılı");
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSong(int id)
        {
            await _songService.TDeleteAsync(id);
            return Ok("Silme işlemi başarılı");
        }

        [Authorize]
        [HttpGet("listen/{songId}")]
        public async Task<IActionResult> ListenSong(int songId)
        {
            var userPackageLevel = User.FindFirst("PackageLevel")?.Value;
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var song = await _songService.GetSongForListeningAsync(songId, int.Parse(userPackageLevel));
            if (song == null)
            {
                return Forbid();
            }
            await _listeningHistoryService.AddListeningHistoryAsync(userId, songId);

            var fileName = Path.GetFileName(song.AudioUrl);
            var filePath = Path.Combine(_environment.ContentRootPath, "Audio", fileName);
            if (!System.IO.File.Exists(filePath))
            {
                return NotFound("Müzik dosyası bulunamadı");
            }
            var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            return File(stream, "audio/mpeg", enableRangeProcessing: true);
        }
        [HttpGet("SongListByArtist/{artistId}")]
        public async Task<IActionResult> SongListByArtist(int artistId)
        {
            var values = await _songService.TGetSongsByArtistIdAsync(artistId);
            return Ok(values);
        }
        [HttpGet("SongListByGenre/{genreId}")]
        public async Task<IActionResult> SongListByGenret(int genreId)
        {
            var values = await _songService.TGetSongsByGenreIdAsync(genreId);
            return Ok(values);
        }
        [HttpGet("most-listened")]
        public async Task<IActionResult> MostListened5Songs()
        {
            var values = await _songService.TMostListened5SongsAsync();
            return Ok(values);
        }
        [HttpGet("recently-added")]
        public async Task<IActionResult> RecentlyAdded4Songs()
        {
            var values = await _songService.TRecentlyAdded4SongsAsync();
            return Ok(values);
        }
    }
}