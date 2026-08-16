using AutoMapper;
using JwtMusicProjectCase.Business.Abstract;
using JwtMusicProjectCase.Business.Dtos.GenreDtos;
using JwtMusicProjectCase.DataAccess.Repositories.Abstract;
using JwtMusicProjectCase.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtMusicProjectCase.Business.Concrete
{
    public class GenreManager : IGenreService
    {
        private readonly IGenreDal _genreDal;
        private readonly IMapper _mapper;

        public GenreManager(IGenreDal genreDal, IMapper mapper)
        {
            _genreDal = genreDal;
            _mapper = mapper;
        }

        public async Task TCreateAsync(CreateGenreDto t)
        {
            var value = _mapper.Map<Genre>(t);
            await _genreDal.CreateAsync(value);
        }

        public async Task TDeleteAsync(int id)
        {
            await _genreDal.DeleteAsync(id);
        }

        public async Task<List<ResultGenreDto>> TGetAllAsync()
        {
            var values = await _genreDal.GetAllAsync();
            return _mapper.Map<List<ResultGenreDto>>(values);
        }

        public async Task<GetByIdGenreDto> TGetByIdAsync(int id)
        {
            var value = await _genreDal.GetByIdAsync(id);
            return _mapper.Map<GetByIdGenreDto>(value);
        }

        public async Task TUpdateAsync(UpdateGenreDto t)
        {
            var value = _mapper.Map<Genre>(t);
            await _genreDal.UpdateAsync(value);
        }
    }
}
