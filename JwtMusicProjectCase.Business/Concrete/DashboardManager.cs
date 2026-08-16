using JwtMusicProjectCase.Business.Abstract;
using JwtMusicProjectCase.Business.Dtos.DashboardDtos;
using JwtMusicProjectCase.DataAccess.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtMusicProjectCase.Business.Concrete
{
    public class DashboardManager : IDashboardService
    {
        private readonly IDashboardDal _dashboardDal;

        public DashboardManager(IDashboardDal dashboardDal)
        {
            _dashboardDal = dashboardDal;
        }

        public async Task<DashboardStatisticsDto> GetStatisticsAsync()
        {
            return new DashboardStatisticsDto
            {
                TotalArtists = await _dashboardDal.GetTotalArtistsAsync(),
                TotalGenres=await _dashboardDal.GetTotalGenresAsync(),
                TotalListens=await _dashboardDal.GetTotalListensAsync(),
                TotalPackages=await _dashboardDal.GetTotalPackagesAsync(),
                TotalSongs=await _dashboardDal.GetTotalSongsAsync(),
                TotalUsers=await _dashboardDal.GetTotalUsersAsync(),
            };
        }
    }
}
