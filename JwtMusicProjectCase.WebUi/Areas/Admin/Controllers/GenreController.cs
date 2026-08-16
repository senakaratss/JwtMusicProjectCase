using JwtMusicProjectCase.WebUi.Dtos.GenreDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace JwtMusicProjectCase.WebUi.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GenreController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public GenreController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> GenreList()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7048/api/Genres");


            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultGenreDto>>(jsonData);

                return View(values);
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SaveGenre(SaveGenreDto saveGenreDto)
        {
            var token = Request.Cookies["JwtToken"];
            var client= _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response;
            if (saveGenreDto.GenreId == null)
            {
                response = await client.PostAsJsonAsync("https://localhost:7048/api/Genres", saveGenreDto);
            }
            else
            {
                response = await client.PutAsJsonAsync("https://localhost:7048/api/Genres", saveGenreDto);
            }
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("GenreList");
            }
            return View();
        }
        public async Task<IActionResult> DeleteGenre(int id)
        {
            var token = Request.Cookies["JwtToken"];
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await client.DeleteAsync($"https://localhost:7048/api/Genres/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("GenreList");
            }
            return View();
        }
    }
}
