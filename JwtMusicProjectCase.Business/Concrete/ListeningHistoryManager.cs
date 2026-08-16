using JwtMusicProjectCase.Business.Abstract;
using JwtMusicProjectCase.Business.Dtos.ListeningHistoryDtos;
using JwtMusicProjectCase.Business.ML;
using JwtMusicProjectCase.DataAccess.Repositories.Abstract;
using JwtMusicProjectCase.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtMusicProjectCase.Business.Concrete
{
    public class ListeningHistoryManager : IListeningHistoryService
    {
        private readonly IListeningHistoryDal _listeningHistoryDal;

        public ListeningHistoryManager(IListeningHistoryDal listeningHistoryDal)
        {
            _listeningHistoryDal = listeningHistoryDal;
        }

        public async Task AddListeningHistoryAsync(string userId, int songId)
        {
            var value = new ListeningHistory
            {
                UserId = userId,
                SongId = songId,
                ListenDate = DateTime.Now
            };
            await _listeningHistoryDal.CreateAsync(value);
        }

        public async Task<List<RecommendationTrainingData>> GetRecommendationTrainingDataAsync()
        {
            var histories = await _listeningHistoryDal.GetAllAsync();
            return histories.GroupBy(x => new { x.SongId, x.UserId }).Select(x => new RecommendationTrainingData
            {
                UserId = x.Key.UserId,
                SongId = (uint)x.Key.SongId,
                Label = x.Count()
            }).ToList();
        }

        public async Task<List<ListeningHistoryByUserDto>> TGetUserListeningHistoryAsync(string userId)
        {
            var values = await _listeningHistoryDal.GetUserListeningHistoryAsync(userId);
            return values.Select(x => new ListeningHistoryByUserDto
            {
                ListeningHistoryId = x.ListeningHistoryId,
                SongId = x.SongId,
                SongName = x.Song.SongName,
                ArtistName = x.Song.Artist.ArtistName,
                CoverImageUrl = x.Song.CoverImageUrl,
                ListenDate = x.ListenDate
            }).ToList();
        }
    }
}
