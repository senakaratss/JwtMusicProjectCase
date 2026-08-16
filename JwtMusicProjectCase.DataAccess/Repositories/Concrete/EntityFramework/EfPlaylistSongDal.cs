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
    public class EfPlaylistSongDal : GenericRepository<PlaylistSong>, IPlaylistSongDal
    {
        private readonly MusicContext _context;
        public EfPlaylistSongDal(MusicContext context) : base(context)
        {
            _context = context;
        }

        public async Task DeletePlaylistSongAsync(int playlistId, int songId)
        {
            var playlistSong = await _context.PlaylistSongs.FirstOrDefaultAsync(x => x.PlaylistId == playlistId
                                                                                    && x.SongId == songId);
            if (playlistSong != null)
            {
                _context.PlaylistSongs.Remove(playlistSong);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<PlaylistSong>> GetPlaylistSongsAsync(int playlistId)
        {
            var values = await _context.PlaylistSongs.Where(x => x.PlaylistId == playlistId)
                                .Include(x=>x.Song).ThenInclude(x=>x.Artist)
                                .ToListAsync();
            return values;
        }

        public async Task<bool> IsSongInPlaylistAsync(int playlistId, int songId)
        {
            return await _context.PlaylistSongs.AnyAsync(x => x.PlaylistId == playlistId && x.SongId == songId);
        }
    }
}
