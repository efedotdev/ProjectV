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
    public class OrderManager:IOrderService
    {
        IOrderDal _orderDal;
        public OrderManager(IOrderDal orderDal)
        {
            _orderDal = orderDal;
        }
        public async Task<IResult> AddAsync(Order order)
        {
            IResult result = BusinessRules.Run();
            if (result != null)
            {
                return result;
            }
            await _orderDal.AddAsync(order);
            return new SuccessResult(Messages.Added);
        }

        public async Task<IResult> Delete(Order order)
        {
            IResult result = BusinessRules.Run();
            if (result != null)
            {
                return result;
            }
            await _orderDal.Delete(order);
            return new SuccessResult(Messages.Deleted);
        }

        public async Task<IDataResult<List<Order>>> GetAllAsync()
        {
            var data = await _orderDal.GetAllAsync();
            return new SuccessDataResult<List<Order>>(data,Messages.Get);
        }

        public async Task<IResult> Update(Order order)
        {
            IResult result = BusinessRules.Run();
            if (result != null)
            {
                return result;
            }
            await _orderDal.Update(order);
            return new SuccessResult(Messages.Modified);
        }
    }
}
