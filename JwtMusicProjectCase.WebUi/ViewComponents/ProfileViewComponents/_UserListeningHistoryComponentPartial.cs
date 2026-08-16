using JwtMusicProjectCase.WebUi.Dtos.ListeningHistoryDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace JwtMusicProjectCase.WebUi.ViewComponents.ProfileViewComponents
{
    public class _UserListeningHistoryComponentPartial:ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _UserListeningHistoryComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var token = Request.Cookies["JwtToken"];
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync("https://localhost:7048/api/ListeningHistories/GetUserListeningHistory");
            if (response.IsSuccessStatusCode)
            {
                var jsonData= await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<UserListeningHistoryDto>>(jsonData);

                return View(values);
            }
            return View();
        }
    }
}
