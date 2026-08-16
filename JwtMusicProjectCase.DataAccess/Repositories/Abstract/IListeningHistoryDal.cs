using JwtMusicProjectCase.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtMusicProjectCase.DataAccess.Repositories.Abstract
{
    public interface IListeningHistoryDal:IGenericDal<ListeningHistory>
    {
        Task<List<ListeningHistory>> GetUserListeningHistoryAsync(string userId);
    }
}
