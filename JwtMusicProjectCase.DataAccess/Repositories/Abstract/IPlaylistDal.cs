using JwtMusicProjectCase.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtMusicProjectCase.DataAccess.Repositories.Abstract
{
    public interface IPlaylistDal:IGenericDal<Playlist>
    {
        Task<List<Playlist>> GetPlaylistsByUserAsync(string userId);
        Task<Playlist> GetPlaylistByIdAsync(int id,string userId);
    }
}
