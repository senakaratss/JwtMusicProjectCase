using JwtMusicProjectCase.Business.Abstract;
using JwtMusicProjectCase.Business.Dtos.RecommendationDtos;
using JwtMusicProjectCase.Business.ML;
using Microsoft.ML;
using Microsoft.ML.Trainers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtMusicProjectCase.Business.Concrete
{
    public class RecommendationManager : IRecommendationService
    {
        private readonly IListeningHistoryService _listeningHistoryService;
        private readonly ISongService _songService;
        private readonly MLContext _mLContext;

        public RecommendationManager(IListeningHistoryService listeningHistoryService, MLContext mLContext, ISongService songService)
        {
            _listeningHistoryService = listeningHistoryService;
            _mLContext = mLContext;
            _songService = songService;
        }

        public async Task<List<RecommendationDto>> GetRecommendations(string userId)
        {
            var trainingData = await _listeningHistoryService.GetRecommendationTrainingDataAsync();
            var songs = await _songService.TGetAllAsync();

            IDataView data = _mLContext.Data.LoadFromEnumerable(trainingData);

            var pipeline = _mLContext.Transforms.Conversion.MapValueToKey(
                                outputColumnName: "UserIdEncoded",
                                inputColumnName: nameof(RecommendationTrainingData.UserId)).Append(
                            _mLContext.Transforms.Conversion.MapValueToKey(
                                outputColumnName: "SongIdEncoded",
                                inputColumnName: nameof(RecommendationTrainingData.SongId)));

            var options = new MatrixFactorizationTrainer.Options
            {
                MatrixColumnIndexColumnName = "UserIdEncoded",
                MatrixRowIndexColumnName = "SongIdEncoded",
                LabelColumnName = nameof(RecommendationTrainingData.Label),
                NumberOfIterations = 20,
                ApproximationRank = 100
            };

            var trainingPipeline = pipeline.Append(_mLContext.Recommendation().Trainers.MatrixFactorization(options));
            var model = trainingPipeline.Fit(data);

            var predictionEngine = _mLContext.Model.CreatePredictionEngine<RecommendationTrainingData, RecommendationPrediction>(model);

            var userSongIds = trainingData.Where(x => x.UserId == userId).Select(x=>x.SongId).ToHashSet();
            var allSongIds = songs.Select(x => (uint)x.SongId).Distinct();

            var recommendations = new List<(int SongId, float Score)>();
            foreach (var songId in allSongIds)
            {
                if (userSongIds.Contains(songId)) continue;

                var prediction = predictionEngine.Predict(new RecommendationTrainingData
                {
                    UserId = userId,
                    SongId = songId
                });
                recommendations.Add(((int)songId, prediction.Score));
            }
            var topRecommendations=recommendations.OrderByDescending(x => x.Score).Take(5).ToList();
            
            var result = new List<RecommendationDto>();

            foreach (var recommendation in topRecommendations)
            {
                var song = songs.FirstOrDefault(x => x.SongId == recommendation.SongId);
                if (song == null) continue;

                result.Add(new RecommendationDto
                {
                    SongId = song.SongId,
                    SongName = song.SongName,
                    ArtistName = song.ArtistName,
                    CoverImageUrl = song.CoverImageUrl,
                    Score = recommendation.Score
                });
            }
            return result;
        }
    }
}
