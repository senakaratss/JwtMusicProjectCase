using JwtMusicProjectCase.WebUi.Dtos.SongDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace JwtMusicProjectCase.WebUi.ViewComponents.SongViewComponents
{
    public class _RecentlyAddedSongsComponentPartial:ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _RecentlyAddedSongsComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7048/api/songs/recently-added");
            if (response.IsSuccessStatusCode)
            {
                var jsonData=await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultSongDto>>(jsonData);
                return View(values);
            }
            return View();
        }
    }
}
