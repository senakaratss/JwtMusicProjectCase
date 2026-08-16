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
    public class RecommendationsController : ControllerBase
    {
        private readonly IRecommendationService _recommendationService;

        public RecommendationsController(IRecommendationService recommendationService)
        {
            _recommendationService = recommendationService;
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetRecommendations()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var packageClaim = User.FindFirst("PackageLevel")?.Value;
            if (!int.TryParse(packageClaim, out int packageLevel))
            {
                return Unauthorized();
            }
            if (packageLevel != 4)
            {
                return Forbid();
            }
            var recommendations = await _recommendationService.GetRecommendations(userId);
            return Ok(recommendations);
        }
    }
}
