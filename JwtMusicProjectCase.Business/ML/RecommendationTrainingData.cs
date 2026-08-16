using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtMusicProjectCase.Business.ML
{
    public class RecommendationTrainingData
    {
        public string UserId { get; set; }
        public uint SongId { get; set; }
        public float Label { get; set; } //number of listens
    }
}
