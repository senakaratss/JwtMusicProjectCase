using JwtMusicProjectCase.Business.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace JwtMusicProjectCase.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListeningHistoriesController : ControllerBase
    {
        private readonly IListeningHistoryService _listeningHistoryService;

        public ListeningHistoriesController(IListeningHistoryService listeningHistoryService)
        {
            _listeningHistoryService = listeningHistoryService;
        }

        [Authorize]
        [HttpGet("GetUserListeningHistory")]
        public async Task<IActionResult> GetUserListeningHistory()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if(userId == null)
            {
                return Unauthorized();
            }
            var values = await _listeningHistoryService.TGetUserListeningHistoryAsync(userId);
            return Ok(values);
        }
    }
}
