using JwtMusicProjectCase.Business.Abstract;
using JwtMusicProjectCase.Business.Dtos.PlaylistSongDtos;
using JwtMusicProjectCase.DataAccess.Repositories.Abstract;
using JwtMusicProjectCase.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtMusicProjectCase.Business.Concrete
{
    public class PlaylistSongManager : IPlaylistSongService
    {
        private readonly IPlaylistSongDal _playlistSongDal;
        private readonly IPlaylistDal _playlistDal;
       

        public PlaylistSongManager(IPlaylistSongDal playlistSongDal, IPlaylistDal playlistDal)
        {
            _playlistSongDal = playlistSongDal;
            _playlistDal = playlistDal;
        }

        public async Task AddSongToPlaylistAsync(int playlistId, int songId, string userId)
        {
            var playlist = await _playlistDal.GetPlaylistByIdAsync(playlistId, userId);
            if (playlist == null)
            {
                throw new Exception("Playlist not found or does not belong to the user.");
            }
            var exists = await _playlistSongDal.IsSongInPlaylistAsync(playlistId, songId);
            if (exists)
            {
                throw new Exception("Song already exists in the playlist.");
            }
            var value = new PlaylistSong
            {
                PlaylistId = playlistId,
                SongId = songId
            };
            await _playlistSongDal.CreateAsync(value);
        }

        public async Task<List<ResultPlaylistSongDto>> GetPlaylistSongsAsync(int playlistId, string userId)
        {
            var playlist = await _playlistDal.GetPlaylistByIdAsync(playlistId, userId);
            if (playlist == null)
            {
                throw new Exception("Playlist not found or does not belong to the user.");
            }
            var values = await _playlistSongDal.GetPlaylistSongsAsync(playlistId);

            return values.Select(x => new ResultPlaylistSongDto
            {
                SongId = x.SongId,
                SongName = x.Song.SongName,
                CoverImageUrl = x.Song.CoverImageUrl,
                AudioUrl = x.Song.AudioUrl,
                Duration = x.Song.Duration,
                ArtistName = x.Song.Artist.ArtistName
            }).ToList();
        }

        public async Task RemoveSongFromPlyalistAsync(int playlistId, int songId, string userId)
        {
            var playlist = await _playlistDal.GetPlaylistByIdAsync(playlistId, userId);
            if(playlist == null)
            {
                throw new Exception("Playlist not found or does not belong to the user.");
            }
            await _playlistSongDal.DeletePlaylistSongAsync(playlistId, songId);

        }
    }
}
