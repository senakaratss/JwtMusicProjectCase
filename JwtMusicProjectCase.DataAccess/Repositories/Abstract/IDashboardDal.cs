using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtMusicProjectCase.DataAccess.Repositories.Abstract
{
    public interface IDashboardDal
    {
        Task<int> GetTotalUsersAsync();
        Task<int> GetTotalSongsAsync();
        Task<int> GetTotalArtistsAsync();
        Task<int> GetTotalGenresAsync();
        Task<int> GetTotalPackagesAsync();
        Task<int> GetTotalListensAsync();
    }
}
