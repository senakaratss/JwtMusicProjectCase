using JwtMusicProjectCase.Business.Dtos.PlaylistDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtMusicProjectCase.Business.Abstract
{
    public interface IPlaylistService
    {
        Task TCreateAsync(CreatePlaylistDto t, string userId);
        Task TUpdateAsync(UpdatePlaylistDto t, string userId);
        Task TDeleteAsync(int id,string userId);
        Task<List<ResultPlaylistDto>> TGetAllAsync(string userId);
        Task<GetByIdPlaylistDto> TGetByIdAsync(int id, string userId);
    }
}
