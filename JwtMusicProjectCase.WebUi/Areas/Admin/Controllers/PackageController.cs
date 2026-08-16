using JwtMusicProjectCase.WebUi.Dtos.PackageDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace JwtMusicProjectCase.WebUi.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PackageController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public PackageController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> PackageList()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7048/api/Packages");
            if (response.IsSuccessStatusCode)
            {
                var jsonData=await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultPackageDto>>(jsonData);
                return View(values);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SavePackage(SavePackageDto savePackageDto)
        {
            var token = Request.Cookies["JwtToken"];
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            
            HttpResponseMessage response;

            if (savePackageDto.PackageId==null)
            {
                response = await client.PostAsJsonAsync("https://localhost:7048/api/Packages", savePackageDto);
            }
            else
            {
                response = await client.PutAsJsonAsync("https://localhost:7048/api/Packages", savePackageDto);

            }
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("PackageList");
            }
            var errorMessage = await response.Content.ReadAsStringAsync();
            return Content(errorMessage);
        }

        public async Task<IActionResult> DeletePackage(int id)
        {
            var token = Request.Cookies["JwtToken"];
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.DeleteAsync($"https://localhost:7048/api/Packages/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("PackageList");
            }
            return View();
        }
    }
}
