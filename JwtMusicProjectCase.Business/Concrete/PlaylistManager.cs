using AutoMapper;
using JwtMusicProjectCase.Business.Abstract;
using JwtMusicProjectCase.Business.Dtos.PlaylistDtos;
using JwtMusicProjectCase.DataAccess.Repositories.Abstract;
using JwtMusicProjectCase.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace JwtMusicProjectCase.Business.Concrete
{
    public class PlaylistManager : IPlaylistService
    {
        private readonly IPlaylistDal _playlistDal;
        private readonly IMapper _mapper;

        public PlaylistManager(IPlaylistDal playlistDal, IMapper mapper)
        {
            _playlistDal = playlistDal;
            _mapper = mapper;
        }

        public async Task TCreateAsync(CreatePlaylistDto t, string userId)
        {
            var value = _mapper.Map<Playlist>(t);
            value.UserId = userId;
            await _playlistDal.CreateAsync(value);
        }

        public async Task TDeleteAsync(int id, string userId)
        {
            var playlist = await _playlistDal.GetByIdAsync(id);
            if(playlist.UserId != userId)
            {
                throw new UnauthorizedAccessException("You are not authorized to delete this playlist.");
            }
            await _playlistDal.DeleteAsync(id);
        }

        public async Task<List<ResultPlaylistDto>> TGetAllAsync(string userId)
        {
            var values = await _playlistDal.GetPlaylistsByUserAsync(userId);
            return _mapper.Map<List<ResultPlaylistDto>>(values);
        }

        public async Task<GetByIdPlaylistDto> TGetByIdAsync(int id, string userId)
        {
            var value = await _playlistDal.GetPlaylistByIdAsync(id, userId);

            if (value == null) return null;

            return _mapper.Map<GetByIdPlaylistDto>(value);
        }

        public async Task TUpdateAsync(UpdatePlaylistDto t, string userId)
        {
            var playlist = await _playlistDal.GetPlaylistByIdAsync(t.PlaylistId, userId);

            if(playlist == null)
            {
                throw new UnauthorizedAccessException("You are not authorized to update this playlist.");
            }

            playlist.PlaylistName = t.PlaylistName;
            await _playlistDal.UpdateAsync(playlist);
        }
    }
}

