using JwtMusicProjectCase.Business.Dtos.RecommendationDtos;
using JwtMusicProjectCase.Business.Dtos.SongDtos;
using JwtMusicProjectCase.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtMusicProjectCase.Business.Abstract
{
    public interface ISongService:IGenericService<ResultSongDto,GetByIdSongDto,CreateSongDto,UpdateSongDto>
    {
        Task<bool> CanListenSong(int songId, int userPackageLevel);
        Task<GetByIdSongDto> GetSongForListeningAsync(int songId, int userPackageLevel);
        Task<List<ResultSongDto>> TGetSongsByArtistIdAsync(int artistId);
        Task<List<ResultSongDto>> TGetSongsByGenreIdAsync(int genreId);
        Task<List<ResultSongDto>> TMostListened5SongsAsync();
        Task<List<ResultSongDto>> TRecentlyAdded4SongsAsync();
    }
}
