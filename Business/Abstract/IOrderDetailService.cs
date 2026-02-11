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
        IDataResult<List<OrderDetail>> GetAll();
        IResult Add(OrderDetail orderDetail);
        IResult Delete(OrderDetail orderDetail);
        IResult Update(OrderDetail orderDetail);
    }
}
