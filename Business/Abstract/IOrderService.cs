using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IOrderService
    {
        Task<IDataResult<List<Order>>> GetAllAsync();
        Task<IResult> AddAsync(Order order);
        Task<IResult> Delete(Order order);
        Task<IResult> Update(Order order);
    }
}
