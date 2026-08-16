using JwtMusicProjectCase.WebUi.Dtos.AuthDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace JwtMusicProjectCase.WebUi.Controllers
{
    public class AuthController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AuthController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            var client = _httpClientFactory.CreateClient();

            var jsonData = JsonConvert.SerializeObject(registerDto);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("https://localhost:7048/api/Auth/register",content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Login");
            }
            var errorMessage = await response.Content.ReadAsStringAsync();
            ModelState.AddModelError("", errorMessage);

            return View(registerDto);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.PostAsJsonAsync("https://localhost:7048/api/Auth/login", loginDto);
            if (response.IsSuccessStatusCode)
            {
                //tokenı alcaz
                var json = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<ResultLoginDto>(json);
                Response.Cookies.Append("JwtToken", values.Token,new CookieOptions
                {
                    HttpOnly = true, //js cookieyi okuyamaz
                    Secure = true,        
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTimeOffset.Now.AddDays(7)
                });
                return RedirectToAction("MyProfile", "Profile");
            }
            var errorMessage = await response.Content.ReadAsStringAsync();
            ModelState.AddModelError("", errorMessage);
            return View(loginDto);
        }
        public IActionResult Logout()
        {
            Response.Cookies.Delete("JwtToken");
            return RedirectToAction("Login");
        }
    }
}
