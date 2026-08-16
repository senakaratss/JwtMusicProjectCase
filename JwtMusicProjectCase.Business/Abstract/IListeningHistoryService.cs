using JwtMusicProjectCase.Business.Dtos.ListeningHistoryDtos;
using JwtMusicProjectCase.Business.ML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtMusicProjectCase.Business.Abstract
{
    public interface IListeningHistoryService
    {
        Task AddListeningHistoryAsync(string userId, int songId);
        Task<List<ListeningHistoryByUserDto>> TGetUserListeningHistoryAsync(string userId);
        Task<List<RecommendationTrainingData>> GetRecommendationTrainingDataAsync();
    }
}
