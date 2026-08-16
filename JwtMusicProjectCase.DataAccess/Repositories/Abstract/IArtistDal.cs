using JwtMusicProjectCase.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtMusicProjectCase.DataAccess.Repositories.Abstract
{
    public interface IArtistDal:IGenericDal<Artist>
    {
        Task<Artist> GetArtistWithSongCountAsync(int id);
    }
}
