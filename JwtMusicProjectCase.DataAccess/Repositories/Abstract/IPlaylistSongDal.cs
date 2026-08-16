using JwtMusicProjectCase.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtMusicProjectCase.DataAccess.Repositories.Abstract
{
    public interface IPlaylistSongDal:IGenericDal<PlaylistSong>
    {
        Task<bool> IsSongInPlaylistAsync(int playlistId, int songId);
        Task DeletePlaylistSongAsync(int playlistId, int songId);
        Task<List<PlaylistSong>> GetPlaylistSongsAsync(int playlistId);
    }
}
