using AutoMapper;
using JwtMusicProjectCase.Business.Abstract;
using JwtMusicProjectCase.Business.Dtos.SongDtos;
using JwtMusicProjectCase.DataAccess.Repositories.Abstract;
using JwtMusicProjectCase.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtMusicProjectCase.Business.Concrete
{
    public class SongManager : ISongService
    {
        private readonly ISongDal _songDal;
        private readonly IMapper _mapper;

        public SongManager(IMapper mapper, ISongDal songDal)
        {
            _mapper = mapper;
            _songDal = songDal;
        }

        public async Task<bool> CanListenSong(int songId, int userPackageLevel)
        {
            var song = await _songDal.GetSongWithDetailsAsync(songId);
            if (song == null)
            {
                return false;
            }
            return userPackageLevel >= song.Package.PackageLevel;
        }

        public async Task<GetByIdSongDto> GetSongForListeningAsync(int songId, int userPackageLevel)
        {
            var song = await _songDal.GetSongWithDetailsAsync(songId);
            if(song == null)
            {
                return null;
            }
            if (song.Package.PackageLevel > userPackageLevel)
            {
                return null;
            }
            await _songDal.IncrementListenCountAsync(songId);
            return _mapper.Map<GetByIdSongDto>(song);
        }

        public async Task TCreateAsync(CreateSongDto t)
        {
            var value = _mapper.Map<Song>(t);
            await _songDal.CreateAsync(value);
        }

        public async Task TDeleteAsync(int id)
        {
            await _songDal.DeleteAsync(id);
        }

        public async Task<List<ResultSongDto>> TGetAllAsync()
        {
            var values=await _songDal.GetSongsWithDetailsAsync();
            return _mapper.Map<List<ResultSongDto>>(values);
        }

        public async Task<GetByIdSongDto> TGetByIdAsync(int id)
        {
            var value = await _songDal.GetSongWithDetailsAsync(id);
            return _mapper.Map<GetByIdSongDto>(value);
        }

        public async Task<List<ResultSongDto>> TGetSongsByArtistIdAsync(int artistId)
        {
            var values = await _songDal.GetSongsByArtistIdAsync(artistId);
            return _mapper.Map<List<ResultSongDto>>(values);
        }

        public async Task<List<ResultSongDto>> TGetSongsByGenreIdAsync(int genreId)
        {
            var values = await _songDal.GetSongsByGenreIdAsync(genreId);
            return _mapper.Map<List<ResultSongDto>>(values);
        }

        public async Task<List<ResultSongDto>> TMostListened5SongsAsync()
        {
            var values = await _songDal.MostListened5SongsAsync();
            return _mapper.Map<List<ResultSongDto>>(values);
        }

        public async Task<List<ResultSongDto>> TRecentlyAdded4SongsAsync()
        {
            var values = await _songDal.RecentlyAdded4SongsAsync();
            return _mapper.Map<List<ResultSongDto>>(values);
        }

        public async Task TUpdateAsync(UpdateSongDto t)
        {
            var value = await _songDal.GetByIdAsync(t.SongId);

            if (value == null)
            {
                return;
            }

            value.SongName = t.SongName;
            value.ArtistId = t.ArtistId;
            value.GenreId = t.GenreId;
            value.PackageId = t.PackageId;
            value.Duration = t.Duration;
            value.ReleaseDate = t.ReleaseDate;
            value.CoverImageUrl = t.CoverImageUrl;
            value.AudioUrl = t.AudioUrl;
            value.Lyrics = t.Lyrics;

            await _songDal.UpdateAsync(value);
        }
    }
}
