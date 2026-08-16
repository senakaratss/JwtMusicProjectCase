using AutoMapper;
using JwtMusicProjectCase.Business.Abstract;
using JwtMusicProjectCase.Business.Dtos.AppUserDtos;
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
    public class UserManager : IUserService
    {
        private readonly IUserDal _userDal;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public UserManager(IUserDal userDal, IMapper mapper, UserManager<AppUser> userManager)
        {
            _userDal = userDal;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<List<ResultUserDto>> TGetAllAsync()
        {
            var values = await _userDal.GetUsersWithDetailsAsync();

            var result = new List<ResultUserDto>();
            foreach (var user in values)
            {
                var dto = _mapper.Map<ResultUserDto>(user);
                var roles = await _userManager.GetRolesAsync(user);
                dto.RoleName = roles.FirstOrDefault();
                result.Add(dto);
            }
            return result;
        }
    }
}
