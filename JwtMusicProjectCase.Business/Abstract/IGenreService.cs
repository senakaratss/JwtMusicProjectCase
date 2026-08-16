using JwtMusicProjectCase.Business.Dtos.GenreDtos;
using JwtMusicProjectCase.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtMusicProjectCase.Business.Abstract
{
    public interface IGenreService:IGenericService<ResultGenreDto,GetByIdGenreDto,CreateGenreDto,UpdateGenreDto>
    {
    }
}
