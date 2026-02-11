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
        public IResult Add(Order order)
        {
            IResult result = BusinessRules.Run();
            if (result != null)
            {
                return result;
            }
            _orderDal.Add(order);
            return new SuccessResult(Messages.Added);
        }

        public IResult Delete(Order order)
        {
            IResult result = BusinessRules.Run();
            if (result != null)
            {
                return result;
            }
            _orderDal.Delete(order);
            return new SuccessResult(Messages.Deleted);
        }

        public IDataResult<List<Order>> GetAll()
        {
            return new SuccessDataResult<List<Order>>(_orderDal.GetAll(), Messages.Get);
        }

        public IResult Update(Order order)
        {
            IResult result = BusinessRules.Run();
            if (result != null)
            {
                return result;
            }
            _orderDal.Update(order);
            return new SuccessResult(Messages.Modified);
        }
    }
}
