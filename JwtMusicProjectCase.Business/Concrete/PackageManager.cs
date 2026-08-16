using AutoMapper;
using JwtMusicProjectCase.Business.Abstract;
using JwtMusicProjectCase.Business.Dtos.PackageDtos;
using JwtMusicProjectCase.DataAccess.Repositories.Abstract;
using JwtMusicProjectCase.Entity.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtMusicProjectCase.Business.Concrete
{
    public class PackageManager : IPackageService
    {
        private readonly IPackageDal _packageDal;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public PackageManager(IPackageDal packageDal, IMapper mapper, UserManager<AppUser> userManager)
        {
            _packageDal = packageDal;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task TCreateAsync(CreatePackageDto t)
        {
            var value = _mapper.Map<Package>(t);
            await _packageDal.CreateAsync(value);
        }

        public async Task TDeleteAsync(int id)
        {
            await _packageDal.DeleteAsync(id);
        }

        public async Task<List<ResultPackageDto>> TGetAllAsync()
        {
            var values = await _packageDal.GetAllAsync();
            return _mapper.Map<List<ResultPackageDto>>(values);
        }

        public async Task<GetByIdPackageDto> TGetByIdAsync(int id)
        {
            var value = await _packageDal.GetByIdAsync(id);
            return _mapper.Map<GetByIdPackageDto>(value);
        }

        public async Task TUpdateAsync(UpdatePackageDto t)
        {
            var value = _mapper.Map<Package>(t);
            await _packageDal.UpdateAsync(value);
        }

        public async Task ChangePackageAsync(string userId, int packageId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            user.PackageId = packageId;
            await _userManager.UpdateAsync(user);
        }
    }
}
