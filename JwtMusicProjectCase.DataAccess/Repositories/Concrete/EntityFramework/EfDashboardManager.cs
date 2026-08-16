using JwtMusicProjectCase.DataAccess.Context;
using JwtMusicProjectCase.DataAccess.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtMusicProjectCase.DataAccess.Repositories.Concrete.EntityFramework
{
    public class EfDashboardManager : IDashboardDal
    {
        private readonly MusicContext _context;

        public EfDashboardManager(MusicContext context)
        {
            _context = context;
        }

        public async Task<int> GetTotalArtistsAsync()
        {
            return await _context.Artists.CountAsync();
        }

        public async Task<int> GetTotalGenresAsync()
        {
            return await _context.Genres.CountAsync();

        }

        public async Task<int> GetTotalListensAsync()
        {
            return await _context.Songs.SumAsync(x => x.ListenCount);
        }

        public async Task<int> GetTotalPackagesAsync()
        {
            return await _context.Packages.CountAsync();
        }

        public async Task<int> GetTotalSongsAsync()
        {
            return await _context.Songs.CountAsync();
        }

        public async Task<int> GetTotalUsersAsync()
        {
            return await _context.Users.CountAsync();
        }
    }
}
