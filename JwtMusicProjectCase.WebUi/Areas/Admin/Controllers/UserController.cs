using JwtMusicProjectCase.WebUi.Dtos.UserDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace JwtMusicProjectCase.WebUi.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public UserController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> UserList()
        {
            var client = _httpClientFactory.CreateClient();
            var token = Request.Cookies["JwtToken"];
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync("https://localhost:7048/api/Users");
            var roleResponse=await client.GetAsync("https://localhost:7048/api/Roles");
            if (response.IsSuccessStatusCode && roleResponse.IsSuccessStatusCode)
            {
                var jsonData= await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultUserDto>>(jsonData);

                var roleJson = await roleResponse.Content.ReadAsStringAsync();
                var roles = JsonConvert.DeserializeObject<List<string>>(roleJson);
                ViewBag.Roles = roles;

                return View(values);
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AssignRole(AssignRoleDto assignRoleDto)
        {
            var client = _httpClientFactory.CreateClient();
            var token = Request.Cookies["JwtToken"];
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.PostAsJsonAsync("https://localhost:7048/api/Roles/assign", assignRoleDto);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("UserList");
            }
            var errorMessage=await response.Content.ReadAsStringAsync();

            return BadRequest(errorMessage);
        }
    }
}
