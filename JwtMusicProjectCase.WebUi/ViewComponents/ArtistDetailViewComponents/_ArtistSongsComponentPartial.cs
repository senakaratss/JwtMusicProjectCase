using JwtMusicProjectCase.WebUi.Dtos.SongDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

namespace JwtMusicProjectCase.WebUi.ViewComponents.ArtistDetailViewComponents
{
    public class _ArtistSongsComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _ArtistSongsComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync(int artistId)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"https://localhost:7048/api/Songs/SongListByArtist/{artistId}");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultSongDto>>(jsonData);

                var token = Request.Cookies["JwtToken"];
                var userPackageLevel = 1;
                if (!string.IsNullOrEmpty(token))
                {
                    var handler = new JwtSecurityTokenHandler();
                    var jwtToken = handler.ReadJwtToken(token);
                    var packageLevelClaim = jwtToken.Claims.FirstOrDefault(x => x.Type == "PackageLevel");
                    if (packageLevelClaim != null)
                    {
                        int.TryParse(packageLevelClaim.Value, out userPackageLevel);
                    }

                    ViewBag.UserPackageLevel = userPackageLevel;
                }
                return View(values);
            }
            return View();

        }
    }
}