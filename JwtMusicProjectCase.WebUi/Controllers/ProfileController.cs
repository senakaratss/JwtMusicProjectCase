using JwtMusicProjectCase.WebUi.Dtos.ProfileDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace JwtMusicProjectCase.WebUi.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ProfileController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> MyProfile()
        {
            var token = Request.Cookies["JwtToken"];

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync("https://localhost:7048/api/Auth/profile");
            if (!response.IsSuccessStatusCode)
            {
                return RedirectToAction("Login", "Auth");
            }
            var jsonData = await response.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<UserProfileDto>(jsonData);
            return View(values);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProfile(UpdateProfileDto dto)
        {
            var token = Request.Cookies["JwtToken"];
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var jsonData = JsonConvert.SerializeObject(dto);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await client.PutAsync("https://localhost:7048/api/auth", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("MyProfile");
            }
            var errorMessage = await response.Content.ReadAsStringAsync();
            TempData["Error"] = errorMessage;
            return RedirectToAction("MyProfile");
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto changePasswordDto)
        {
            var token = Request.Cookies["JwtToken"];
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var jsonData = JsonConvert.SerializeObject(changePasswordDto);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await client.PutAsync("https://localhost:7048/api/Auth/change-password", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Login","Auth");
            }
            var errorMessage = await response.Content.ReadAsStringAsync();
            TempData["PasswordError"] = errorMessage;
            return RedirectToAction("MyProfile");
        }
    }
}
