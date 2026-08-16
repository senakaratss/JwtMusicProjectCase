using JwtMusicProjectCase.Business.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace JwtMusicProjectCase.WebApi.Controllers
{
    [Authorize(Roles ="Admin")]
    public class DashboardsController : Controller
    {
        private readonly IDashboardService _dashboardService;

        public DashboardsController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }
        [HttpGet("statistics")]
        public async Task<IActionResult> GetStatistics()
        {
            var result = await _dashboardService.GetStatisticsAsync();
            return Ok(result);
        }
    }
}
