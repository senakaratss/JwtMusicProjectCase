using JwtMusicProjectCase.Business.Dtos.PlaylistSongDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtMusicProjectCase.Business.Abstract
{
    public interface IPlaylistSongService
    {
        Task AddSongToPlaylistAsync(int playlistId,int songId,string userId);
        Task RemoveSongFromPlyalistAsync(int playlistId, int songId, string userId);
        Task<List<ResultPlaylistSongDto>> GetPlaylistSongsAsync(int playlistId, string userId);
    }
}
