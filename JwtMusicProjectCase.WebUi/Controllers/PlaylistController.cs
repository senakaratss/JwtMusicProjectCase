using JwtMusicProjectCase.WebUi.Dtos.PlaylistDtos;
using JwtMusicProjectCase.WebUi.Dtos.PlaylistSongDtos;
using JwtMusicProjectCase.WebUi.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace JwtMusicProjectCase.WebUi.Controllers
{
    public class PlaylistController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public PlaylistController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IActionResult> GetMyPlaylists()
        {
            var client = _httpClientFactory.CreateClient();
            var token = Request.Cookies["JwtToken"];
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await client.GetAsync("https://localhost:7048/api/Playlists");
            if (response.IsSuccessStatusCode)
            {
                var jsonData= await response.Content.ReadAsStringAsync();
                Console.WriteLine(jsonData);
                var values = JsonConvert.DeserializeObject<List<ResultPlaylistDto>>(jsonData);
                return Json(values);
            }
            return BadRequest();
        }
        [HttpPost]
        public async Task<IActionResult> CreatePlaylist(CreatePlaylistDto createPlaylistDto)
        {
            var client = _httpClientFactory.CreateClient();
            var token = Request.Cookies["JwtToken"];
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.PostAsJsonAsync("https://localhost:7048/api/Playlists", createPlaylistDto);

            return RedirectToAction("MyProfile", "Profile");
        }
        public async Task<IActionResult> PlaylistDetail(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var token = Request.Cookies["JwtToken"];
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var playlistResponse = await client.GetAsync($"https://localhost:7048/api/Playlists/{id}");
            var playlistSongResponse = await client.GetAsync($"https://localhost:7048/api/PlaylistSongs/{id}/songs");
            if (!playlistSongResponse.IsSuccessStatusCode || !playlistResponse.IsSuccessStatusCode)
            {
                return NotFound();
            }
            var playlistSongJson = await playlistSongResponse.Content.ReadAsStringAsync();
            var playlistJson = await playlistResponse.Content.ReadAsStringAsync();

            var playlist = JsonConvert.DeserializeObject<ResultPlaylistDto>(playlistJson);
            var playlistSongs = JsonConvert.DeserializeObject<List<ResultPlaylistSongDto>>(playlistSongJson);

            var model = new PlaylistDetailViewModel
            {
                PlaylistId = id,
                PlaylistName = playlist.PlaylistName,
                PlaylistSongs = playlistSongs
            };
            return View(model);
        }
        public async Task<IActionResult> AddSongToPlaylist(int songId, int playlistId)
        {
            var token = Request.Cookies["JwtToken"];
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.PostAsync($"https://localhost:7048/api/PlaylistSongs/{playlistId}/songs/{songId}", null);
            if (response.IsSuccessStatusCode)
            {
                return Ok();
            }
            return BadRequest();
        }
        public async Task<IActionResult> RemoveSongFromPlaylist(int songId, int playlistId)
        {
            var token = Request.Cookies["JwtToken"];
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.DeleteAsync($"https://localhost:7048/api/PlaylistSongs/{playlistId}/songs/{songId}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("PlaylistDetail", new { id = playlistId });
            }
            return View();
        }
    }
}
