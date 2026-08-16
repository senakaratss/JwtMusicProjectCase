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
    public class EfListeningHistoryDal : GenericRepository<ListeningHistory>, IListeningHistoryDal
    {
        private readonly MusicContext _context;
        public EfListeningHistoryDal(MusicContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<ListeningHistory>> GetUserListeningHistoryAsync(string userId)
        {
            return await _context.ListeningHistories.Include(x => x.Song).ThenInclude(x=>x.Artist)
                .Where(x => x.UserId == userId).OrderByDescending(x=>x.ListenDate).ToListAsync();
        }
    }
}
