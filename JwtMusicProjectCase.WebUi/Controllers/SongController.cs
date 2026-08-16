using JwtMusicProjectCase.WebUi.Dtos.GenreDtos;
using JwtMusicProjectCase.WebUi.Dtos.RecommendationDtos;
using JwtMusicProjectCase.WebUi.Dtos.SongDtos;
using JwtMusicProjectCase.WebUi.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace JwtMusicProjectCase.WebUi.Controllers
{
    public class SongController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public SongController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IActionResult> Listen(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var token = Request.Cookies["JwtToken"];
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync($"https://localhost:7048/api/Songs/listen/{id}");
            if (response.StatusCode == HttpStatusCode.Forbidden)
            {
                return StatusCode(403, "Bu şarkıyı dinlemek için paketini yükseltmen gerekiyor.");
            }
            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode);
            }
            var stream = await response.Content.ReadAsStreamAsync();
            return File(stream, "audio/mpeg", enableRangeProcessing: true);
        }
        public async Task<IActionResult> SongList()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7048/api/songs");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultSongDto>>(jsonData);

                var token = Request.Cookies["JwtToken"];
                int userPackageLevel = 1;
                if (!string.IsNullOrEmpty(token))
                {
                    var handler = new JwtSecurityTokenHandler();
                    var jwtToken = handler.ReadJwtToken(token);

                    var packageLevelClaim = jwtToken.Claims.FirstOrDefault(x => x.Type == "PackageLevel");

                    if (packageLevelClaim != null)
                    {
                        int.TryParse(packageLevelClaim.Value, out userPackageLevel);
                    }
                    var isEliteMember = userPackageLevel == 4;
                    ViewBag.UserPackageLevel = userPackageLevel;
                    ViewBag.IsEliteMember = isEliteMember;
                    if (isEliteMember)
                    {
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                        var recommendedResponse = await client.GetAsync("https://localhost:7048/api/Recommendations");
                        if (recommendedResponse.IsSuccessStatusCode)
                        {
                            var jsonRecommended = await recommendedResponse.Content.ReadAsStringAsync();
                            var recommended = JsonConvert.DeserializeObject<List<RecommendationDto>>(jsonRecommended);
                            ViewBag.RecommendedSongs = recommended;
                        }
                    }

                }
                return View(values);
            }
            return View();
        }
        public async Task<IActionResult> SongListByGenre(int id)
        {
            var client = _httpClientFactory.CreateClient();

            var genreResponse = await client.GetAsync($"https://localhost:7048/api/Genres/GetGenreById/{id}");

            var songResponse = await client.GetAsync($"https://localhost:7048/api/songs/SongListByGenre/{id}");

            var genreJson = await genreResponse.Content.ReadAsStringAsync();
            var songJson = await songResponse.Content.ReadAsStringAsync();

            var genre = JsonConvert.DeserializeObject<GetByIdGenreDto>(genreJson);
            var songs = JsonConvert.DeserializeObject<List<ResultSongDto>>(songJson);

            var model = new GenreDetailViewModel
            {
                GenreId = genre.GenreId,
                GenreName = genre.GenreName,
                Songs = songs ?? new List<ResultSongDto>()
            };

            return View(model);
        }
    }
}
