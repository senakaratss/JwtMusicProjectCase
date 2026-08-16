using JwtMusicProjectCase.WebUi.Dtos.PackageDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace JwtMusicProjectCase.WebUi.Controllers
{
    public class PackageController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public PackageController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IActionResult> PackageList(int currentPackageId)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7048/api/packages");
            if (response.IsSuccessStatusCode)
            {
                var jsonData=await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultPackageDto>>(jsonData);

                ViewBag.CurrentPackageId = currentPackageId;

                return View(values);
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ChangePackage(int packageId)
        {
            var token = Request.Cookies["JwtToken"];
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.PutAsync($"https://localhost:7048/api/packages/change-package/{packageId}", null);
            
            return RedirectToAction("MyProfile","Profile");
        }
    }
}
