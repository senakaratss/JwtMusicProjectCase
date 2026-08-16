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
    public class EfArtistDal : GenericRepository<Artist>, IArtistDal
    {
        private readonly MusicContext _context;
        public EfArtistDal(MusicContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Artist> GetArtistWithSongCountAsync(int id)
        {
            return await _context.Artists.Include(x=>x.Songs).FirstOrDefaultAsync(x => x.ArtistId == id);
        }
    }
}
