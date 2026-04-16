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
    public class OrderDetailManager : IOrderDetailService
    {
        IOrderDetailDal _orderDetailDal;
        public OrderDetailManager(IOrderDetailDal orderDetailDal)
        {
            _orderDetailDal = orderDetailDal;
        }
        public async Task<IResult> AddAsync(OrderDetail orderDetail)
        {
            IResult result = BusinessRules.Run();
            if (result != null)
            {
                return result;
            }
            await _orderDetailDal.AddAsync(orderDetail);
            return new SuccessResult(Messages.Added);
        }

        public async Task<IResult> Delete(OrderDetail orderDetail)
        {
            IResult result = BusinessRules.Run();
            if (result != null)
            {
                return result;
            }
            await _orderDetailDal.Delete(orderDetail);
            return new SuccessResult(Messages.Deleted);
        }

        public async Task<IDataResult<List<OrderDetail>>> GetAllAsync()
        {
            var data = await _orderDetailDal.GetAllAsync();
            return new SuccessDataResult<List<OrderDetail>>(data,Messages.Get);
        }

        public async Task<IResult> Update(OrderDetail orderDetail)
        {
            IResult result = BusinessRules.Run();
            if (result != null)
            {
                return result;
            }
            await _orderDetailDal.Update(orderDetail);
            return new SuccessResult(Messages.Modified);
        }
    }
}
