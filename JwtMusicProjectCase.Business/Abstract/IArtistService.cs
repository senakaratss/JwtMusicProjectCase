using JwtMusicProjectCase.Business.Dtos.ArtistDtos;
using JwtMusicProjectCase.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtMusicProjectCase.Business.Abstract
{
    public interface IArtistService:IGenericService<ResultArtistDto,GetByIdArtistDto,CreateArtistDto,UpdateArtistDto>
    {
        Task<GetByIdArtistDto> TGetArtistWithSongCountAsync(int id);
    }
}
