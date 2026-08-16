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
    public class EfSongDal : GenericRepository<Song>, ISongDal
    {
        private readonly MusicContext _context;
        public EfSongDal(MusicContext context) : base(context)
        {
             _context=context;
        }

        public async Task<List<Song>> GetSongsByArtistIdAsync(int artistId)
        {
            return await _context.Songs.Where(x => x.ArtistId == artistId).Include(x => x.Genre)
                .Include(x => x.Artist).Include(x => x.Package).ToListAsync();
        }

        public async Task<List<Song>> GetSongsByGenreIdAsync(int genreId)
        {
            return await _context.Songs.Where(x => x.GenreId == genreId).Include(x => x.Genre)
                .Include(x => x.Artist).Include(x => x.Package).ToListAsync();
        }

        public async Task<List<Song>> GetSongsByIds(List<int> songIds)
        {
            return await _context.Songs.Include(x => x.Artist).Where(x => songIds.Contains(x.SongId)).ToListAsync();
        }

        public async Task<List<Song>> GetSongsWithDetailsAsync()
        {
            var values = await _context.Songs.Include(x => x.Genre).Include(x => x.Artist).Include(x => x.Package).ToListAsync();
            return values;
        }

        public async Task<Song> GetSongWithDetailsAsync(int id)
        {
            return  await _context.Songs.Include(x => x.Genre).Include(x => x.Artist).Include(x => x.Package)
                .FirstOrDefaultAsync(x=>x.SongId==id);
        }

        public async Task IncrementListenCountAsync(int songId)
        {
            var song = await _context.Songs.FindAsync(songId);

            if (song == null) return;
           
            song.ListenCount++;
            await _context.SaveChangesAsync();
        }

        public async Task<List<Song>> MostListened5SongsAsync()
        {
            return await _context.Songs.OrderByDescending(x => x.ListenCount).Take(5)
                .Include(x=>x.Genre).Include(x=>x.Artist).Include(x=>x.Package)
                .ToListAsync();
        }

        public async Task<List<Song>> RecentlyAdded4SongsAsync()
        {
            return await _context.Songs.OrderByDescending(x => x.ReleaseDate).Take(4)
                .Include(x=>x.Genre).Include(x=>x.Artist).Include(x=>x.Package)
                .ToListAsync();
        }
    }
}
