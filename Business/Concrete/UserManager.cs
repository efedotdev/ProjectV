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
        //public IResult Add(User user)
        //{
        //    _userDal.Add(user);
        //    return new SuccessResult(Messages.Added);
        //}

        //public IDataResult<User> GetByMail(string email)
        //{
        //    return new SuccessDataResult<User>(_userDal.Get(u => u.Email == email));
        //}

        //public IDataResult<List<OperationClaim>> GetClaims(User user)
        //{
        //    return new SuccessDataResult <List<OperationClaim>> (_userDal.GetClaims(user));           
        //}
        // ????????????????????????????????????? neden RESULT KULLANAMIYOR PASSWORD HASH GELMEDİ AUTHMANAGERDA
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
