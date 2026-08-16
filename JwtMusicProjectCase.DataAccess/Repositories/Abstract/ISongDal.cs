using JwtMusicProjectCase.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtMusicProjectCase.DataAccess.Repositories.Abstract
{
    public interface ISongDal:IGenericDal<Song>
    {
        Task<List<Song>> GetSongsWithDetailsAsync();
        Task<Song> GetSongWithDetailsAsync(int id);
        Task<List<Song>> GetSongsByArtistIdAsync(int artistId);
        Task<List<Song>> GetSongsByGenreIdAsync(int genreId);
        Task<List<Song>> MostListened5SongsAsync();
        Task<List<Song>> RecentlyAdded4SongsAsync();
        Task IncrementListenCountAsync(int songId);
        Task<List<Song>> GetSongsByIds(List<int> songIds);
    }
}
