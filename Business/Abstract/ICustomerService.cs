using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICustomerService
    {
        Task<IDataResult<List<Customer>>> GetAllAsync();
        Task<IResult> AddAsync(Customer customer);
        Task<IResult> Delete(Customer customer);
        Task<IResult> Update(Customer customer);
    }
}
