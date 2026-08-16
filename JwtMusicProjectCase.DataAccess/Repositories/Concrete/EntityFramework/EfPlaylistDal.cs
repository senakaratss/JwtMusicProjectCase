using JwtMusicProjectCase.DataAccess.Context;
using JwtMusicProjectCase.DataAccess.Repositories.Abstract;
using JwtMusicProjectCase.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtMusicProjectCase.DataAccess.Repositories.Concrete.EntityFramework
{
    public class EfPlaylistDal : GenericRepository<Playlist>, IPlaylistDal
    {
        private readonly MusicContext _context;
        public EfPlaylistDal(MusicContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Playlist> GetPlaylistByIdAsync(int id, string userId)
        {
            return await _context.Playlists.FirstOrDefaultAsync(x => x.PlaylistId == id && x.UserId == userId);
        }

        public async Task<List<Playlist>> GetPlaylistsByUserAsync(string userId)
        {
            return await _context.Playlists.Where(x => x.UserId == userId).ToListAsync();
        }
    }
}
