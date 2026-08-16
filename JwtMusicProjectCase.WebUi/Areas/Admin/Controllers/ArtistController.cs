using JwtMusicProjectCase.WebUi.Dtos.ArtistDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace JwtMusicProjectCase.WebUi.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ArtistController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ArtistController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> ArtistList()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7048/api/Artists");
            if (response.IsSuccessStatusCode)
            {
                var jsonData=await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultArtistDto>>(jsonData);
                return View(values);
            }

            return View();
        }
        public IActionResult CreateArtist()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateArtist(CreateArtistDto createArtistDto)
        {
            var client = _httpClientFactory.CreateClient();
            var token = Request.Cookies["JwtToken"];
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.PostAsJsonAsync("https://localhost:7048/api/Artists", createArtistDto);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("ArtistList");
            }
            return View();
        }

        public async Task<IActionResult> UpdateArtist(int id)
        {
            var client=_httpClientFactory.CreateClient();
            var response = await client.GetAsync($"https://localhost:7048/api/Artists/{id}");
            if (response.IsSuccessStatusCode)
            {
                var jsonData=await response.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<GetByIdArtistDto>(jsonData);
                return View(value);
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UpdateArtist(UpdateArtistDto updateArtistDto)
        {
            var token = Request.Cookies["JwtToken"];
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.PutAsJsonAsync("https://localhost:7048/api/Artists", updateArtistDto);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("ArtistList");
            }
            return View();
        }
        
        public async Task<IActionResult> DeleteArtist(int id)
        {
            var token = Request.Cookies["JwtToken"];
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.DeleteAsync($"https://localhost:7048/api/Artists/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("ArtistList");
            }
            return View();
        }
    }
}
