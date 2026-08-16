using JwtMusicProjectCase.DataAccess.Context;
using JwtMusicProjectCase.DataAccess.Repositories.Abstract;
using JwtMusicProjectCase.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtMusicProjectCase.DataAccess.Repositories.Concrete.EntityFramework
{
    public class EfGenreDal : GenericRepository<Genre>, IGenreDal
    {
        public EfGenreDal(MusicContext context) : base(context)
        {
        }
    }
}
