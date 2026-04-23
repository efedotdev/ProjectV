using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        IUserDal _userDal;
        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }
        public async Task<List<OperationClaim>> GetClaimsAsync(User user)
        {
            return await _userDal.GetClaimsAsync(user);
        }

        public async Task AddAsync(User user)
        {
            await _userDal.AddAsync(user);
        }

        public async  Task<User> GetByMailAsync(string email)
        {
            return await _userDal.GetAsync(u => u.Email == email);
        }

    }
}
