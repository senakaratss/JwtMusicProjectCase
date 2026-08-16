using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtMusicProjectCase.Business.Abstract
{
    public interface IGenericService<TResulDto, TGetByIdDto, TCreateDto, TUpdateDto>
    {
        Task<List<TResulDto>> TGetAllAsync();
        Task<TGetByIdDto> TGetByIdAsync(int id);
        Task TCreateAsync(TCreateDto t);
        Task TUpdateAsync(TUpdateDto t);
        Task TDeleteAsync(int id);
    }
}
