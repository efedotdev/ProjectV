using Business.Abstract;
using Business.Constants;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CustomerManager : ICustomerService
    {
        ICustomerDal _customerDal;
        public CustomerManager(ICustomerDal customerDal)
        {
            _customerDal = customerDal;
        }
        public async Task<IResult> AddAsync(Customer customer)
        {
            IResult result = BusinessRules.Run();
            if (result != null)
            {
                return result;
            }
            await _customerDal.AddAsync(customer);
            return new SuccessResult(Messages.Added);
        }

        public async Task<IResult> Delete(Customer customer)
        {
            IResult result = BusinessRules.Run();
            if (result != null)
            {
                return result;
            }
            await _customerDal.Delete(customer);
            return new SuccessResult(Messages.Deleted);
        }

        public async Task<IDataResult<List<Customer>>> GetAllAsync()
        {
            var data = await _customerDal.GetAllAsync();
            return new SuccessDataResult<List<Customer>>(data, Messages.Get);
        }

        public async Task<IResult> Update(Customer customer)
        {
            IResult result = BusinessRules.Run();
            if (result != null)
            {
                return result;
            }
            await _customerDal.Update(customer);
            return new SuccessResult(Messages.Modified);
        }
    }
}
