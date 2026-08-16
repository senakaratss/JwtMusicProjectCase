using AutoMapper;
using JwtMusicProjectCase.Business.Abstract;
using JwtMusicProjectCase.Business.Dtos.ArtistDtos;
using JwtMusicProjectCase.DataAccess.Repositories.Abstract;
using JwtMusicProjectCase.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtMusicProjectCase.Business.Concrete
{
    public class ArtistManager : IArtistService
    {
        private readonly IArtistDal _artistDal;
        private readonly IMapper _mapper;

        public ArtistManager(IArtistDal artistDal, IMapper mapper)
        {
            _artistDal = artistDal;
            _mapper = mapper;
        }

        public async Task TCreateAsync(CreateArtistDto t)
        {
            var value = _mapper.Map<Artist>(t);
            value.IsVerified = false;
            value.CreatedDate = DateTime.Now;
            await _artistDal.CreateAsync(value);
        }

        public async Task TDeleteAsync(int id)
        {
            await _artistDal.DeleteAsync(id);
        }

        public async Task<List<ResultArtistDto>> TGetAllAsync()
        {
            var values = await _artistDal.GetAllAsync();
            return _mapper.Map<List<ResultArtistDto>>(values);
        }

        public async Task<GetByIdArtistDto> TGetArtistWithSongCountAsync(int id)
        {
            var value = await _artistDal.GetArtistWithSongCountAsync(id);
            return _mapper.Map<GetByIdArtistDto>(value);
        }

        public async Task<GetByIdArtistDto> TGetByIdAsync(int id)
        {
            var value = await _artistDal.GetByIdAsync(id);
            return _mapper.Map<GetByIdArtistDto>(value);
        }

        public async Task TUpdateAsync(UpdateArtistDto t)
        {
            var artist = await _artistDal.GetByIdAsync(t.ArtistId);

            artist.ArtistName = t.ArtistName;
            artist.ArtistImageUrl = t.ArtistImageUrl;
            artist.CoverImageUrl = t.CoverImageUrl;
            artist.Bio = t.Bio;
            artist.Country = t.Country;
            artist.IsVerified = t.IsVerified;

            await _artistDal.UpdateAsync(artist);
        }
    }
}
