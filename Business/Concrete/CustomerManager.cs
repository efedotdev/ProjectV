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
    public class CustomerManager:ICustomerService
    {
        ICustomerDal _customerDal;
        public CustomerManager(ICustomerDal customerDal)
        {
            _customerDal = customerDal;
        }
        public IResult Add(Customer customer)
        {
            IResult result = BusinessRules.Run();
            if (result != null)
            {
                return result;
            }
            _customerDal.Add(customer);
            return new SuccessResult(Messages.Added);
        }

        public IResult Delete(Customer customer)
        {
            IResult result = BusinessRules.Run();
            if (result != null)
            {
                return result;
            }
            _customerDal.Delete(customer);
            return new SuccessResult(Messages.Deleted);
        }

        public IDataResult<List<Customer>> GetAll()
        {
            return new SuccessDataResult<List<Customer>>(_customerDal.GetAll(), Messages.Get);
        }

        public IResult Update(Customer customer)
        {
            IResult result = BusinessRules.Run();
            if (result != null)
            {
                return result;
            }
            _customerDal.Update(customer);
            return new SuccessResult(Messages.Modified);
        }
    }
}
