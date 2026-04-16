using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IOrderDetailService
    {
        Task<IDataResult<List<OrderDetail>>> GetAllAsync();
        Task<IResult> AddAsync(OrderDetail orderDetail);
        Task<IResult> Delete(OrderDetail orderDetail);
        Task<IResult> Update(OrderDetail orderDetail);
    }
}
