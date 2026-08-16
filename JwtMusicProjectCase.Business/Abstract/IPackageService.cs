using JwtMusicProjectCase.Business.Dtos.PackageDtos;
using JwtMusicProjectCase.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtMusicProjectCase.Business.Abstract
{
    public interface IPackageService:IGenericService<ResultPackageDto,GetByIdPackageDto,CreatePackageDto,UpdatePackageDto>
    {
        Task ChangePackageAsync(string userId, int packageId);
    }
}
