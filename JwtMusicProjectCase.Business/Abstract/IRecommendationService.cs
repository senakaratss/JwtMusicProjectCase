using JwtMusicProjectCase.Business.Dtos.RecommendationDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtMusicProjectCase.Business.Abstract
{
    public interface IRecommendationService
    {
        Task<List<RecommendationDto>> GetRecommendations(string userId);
    }
}
