using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtMusicProjectCase.Business.Dtos.DashboardDtos
{
    public class DashboardStatisticsDto
    {
        public int TotalUsers { get; set; }
        public int TotalSongs { get; set; }
        public int TotalPackages { get; set; }
        public int TotalGenres { get; set; }
        public int TotalListens { get; set; }
        public int TotalArtists { get; set; }
    }
}
