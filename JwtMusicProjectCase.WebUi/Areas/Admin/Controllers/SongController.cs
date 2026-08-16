using JwtMusicProjectCase.WebUi.Dtos.ArtistDtos;
using JwtMusicProjectCase.WebUi.Dtos.GenreDtos;
using JwtMusicProjectCase.WebUi.Dtos.PackageDtos;
using JwtMusicProjectCase.WebUi.Dtos.SongDtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace JwtMusicProjectCase.WebUi.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SongController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public SongController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> SongList(int page = 1, int pageSize = 6)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7048/api/Songs");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultSongDto>>(jsonData);

                var pagedValues = values.Skip((page - 1) * pageSize).Take(pageSize).ToList();
               
                var totalCount = values.Count();
                var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

                ViewBag.TotalPages = totalPages;
                ViewBag.CurrentPage = page;
                ViewBag.PageSize = pageSize;
                ViewBag.TotalCount = totalCount;

                return View(pagedValues);
            }
            return View();
        }

        public async Task<IActionResult> CreateSong()
        {
            var client = _httpClientFactory.CreateClient();

            var genreResponse = await client.GetAsync("https://localhost:7048/api/Genres");
            if (genreResponse.IsSuccessStatusCode)
            {
                var genreJson = await genreResponse.Content.ReadAsStringAsync();
                var genres = JsonConvert.DeserializeObject<List<ResultGenreDto>>(genreJson);
                ViewBag.Genres = new SelectList(genres, "GenreId", "GenreName");
            }
            var packageResponse = await client.GetAsync("https://localhost:7048/api/Packages");
            if (packageResponse.IsSuccessStatusCode)
            {
                var packageJson = await packageResponse.Content.ReadAsStringAsync();
                var packages = JsonConvert.DeserializeObject<List<ResultPackageDto>>(packageJson);
                ViewBag.Packages = new SelectList(packages, "PackageId", "PackageName");
            }
            var artistResponse = await client.GetAsync("https://localhost:7048/api/Artists");
            if (artistResponse.IsSuccessStatusCode)
            {
                var artistJson = await artistResponse.Content.ReadAsStringAsync();
                var artists = JsonConvert.DeserializeObject<List<ArtistListDto>>(artistJson);
                ViewBag.Artists = new SelectList(artists, "ArtistId", "ArtistName");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateSong(CreateSongDto createSongDto)
        {
            var token = Request.Cookies["JwtToken"];
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            using var formData = new MultipartFormDataContent();
            formData.Add(new StringContent(createSongDto.SongName), "SongName");
            formData.Add(new StringContent(createSongDto.CoverImageUrl), "CoverImageUrl");
            formData.Add(new StringContent(createSongDto.Duration.ToString()), "Duration");
            formData.Add(new StringContent(createSongDto.ListenCount.ToString()), "ListenCount");
            formData.Add(new StringContent(createSongDto.ReleaseDate.ToString("yyyy-MM-dd")), "ReleaseDate");
            formData.Add(new StringContent(createSongDto.Lyrics), "Lyrics");
            formData.Add(new StringContent(createSongDto.ArtistId.ToString()), "ArtistId");
            formData.Add(new StringContent(createSongDto.GenreId.ToString()), "GenreId");
            formData.Add(new StringContent(createSongDto.PackageId.ToString()), "PackageId");

            if (createSongDto.AudioFile != null)
            {
                var fileContent = new StreamContent(createSongDto.AudioFile.OpenReadStream());
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(createSongDto.AudioFile.ContentType);
                formData.Add(fileContent, "AudioFile", createSongDto.AudioFile.FileName);
            }
            var response = await client.PostAsync("https://localhost:7048/api/Songs", formData);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                return Content(
                    $"API Hatası: {response.StatusCode}<br>{responseContent}",
                    "text/html");
            }

            return RedirectToAction("SongList");
        }

        public async Task<IActionResult> UpdateSong(int id)
        {
            var client = _httpClientFactory.CreateClient();

            var genreResponse = await client.GetAsync("https://localhost:7048/api/Genres");
            if (genreResponse.IsSuccessStatusCode)
            {
                var genreJson = await genreResponse.Content.ReadAsStringAsync();
                var genres = JsonConvert.DeserializeObject<List<ResultGenreDto>>(genreJson);
                ViewBag.Genres = new SelectList(genres, "GenreId", "GenreName");
            }
            var packageResponse = await client.GetAsync("https://localhost:7048/api/Packages");
            if (packageResponse.IsSuccessStatusCode)
            {
                var packageJson = await packageResponse.Content.ReadAsStringAsync();
                var packages = JsonConvert.DeserializeObject<List<ResultPackageDto>>(packageJson);
                ViewBag.Packages = new SelectList(packages, "PackageId", "PackageName");
            }
            var artistResponse = await client.GetAsync("https://localhost:7048/api/Artists");
            if (artistResponse.IsSuccessStatusCode)
            {
                var artistJson = await artistResponse.Content.ReadAsStringAsync();
                var artists = JsonConvert.DeserializeObject<List<ArtistListDto>>(artistJson);
                ViewBag.Artists = new SelectList(artists, "ArtistId", "ArtistName");
            }
            var response = await client.GetAsync($"https://localhost:7048/api/Songs/{id}");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<GetByIdSongDto>(jsonData);
                return View(value);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateSong(UpdateSongDto updateSongDto)
        {
            var token = Request.Cookies["JwtToken"];
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            using var formData = new MultipartFormDataContent();
            formData.Add(new StringContent(updateSongDto.SongId.ToString()), "SongId");
            formData.Add(new StringContent(updateSongDto.SongName), "SongName");
            formData.Add(new StringContent(updateSongDto.CoverImageUrl), "CoverImageUrl");
            formData.Add(new StringContent(updateSongDto.Duration.ToString()), "Duration");
            formData.Add(new StringContent(updateSongDto.ListenCount.ToString()), "ListenCount");
            formData.Add(new StringContent(updateSongDto.ReleaseDate.ToString("yyyy-MM-dd")), "ReleaseDate");
            formData.Add(new StringContent(updateSongDto.Lyrics), "Lyrics");
            formData.Add(new StringContent(updateSongDto.ArtistId.ToString()), "ArtistId");
            formData.Add(new StringContent(updateSongDto.GenreId.ToString()), "GenreId");
            formData.Add(new StringContent(updateSongDto.PackageId.ToString()), "PackageId");

            if (updateSongDto.AudioFile != null)
            {
                var fileContent = new StreamContent(updateSongDto.AudioFile.OpenReadStream());
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(updateSongDto.AudioFile.ContentType);
                formData.Add(fileContent, "AudioFile", updateSongDto.AudioFile.FileName);
            }

            var response = await client.PutAsync("https://localhost:7048/api/Songs", formData);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("SongList");
            }
            var error = await response.Content.ReadAsStringAsync();

            return Content(
                $"API Hatası: {response.StatusCode}<br>{error}",
                "text/html");
        }

        public async Task<IActionResult> DeleteSong(int id)
        {
            var token = Request.Cookies["JwtToken"];
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.DeleteAsync($"https://localhost:7048/api/Songs/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("SongList");
            }
            return View();
        }
    }
}
