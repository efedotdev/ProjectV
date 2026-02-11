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
        public IResult Add(OrderDetail orderDetail)
        {
            IResult result = BusinessRules.Run();
            if (result != null)
            {
                return result;
            }
            _orderDetailDal.Add(orderDetail);
            return new SuccessResult(Messages.Added);
        }

        public IResult Delete(OrderDetail orderDetail)
        {
            IResult result = BusinessRules.Run();
            if (result != null)
            {
                return result;
            }
            _orderDetailDal.Delete(orderDetail);
            return new SuccessResult(Messages.Deleted);
        }

        public IDataResult<List<OrderDetail>> GetAll()
        {
            return new SuccessDataResult<List<OrderDetail>>(_orderDetailDal.GetAll(), Messages.Get);
        }

        public IResult Update(OrderDetail orderDetail)
        {
            IResult result = BusinessRules.Run();
            if (result != null)
            {
                return result;
            }
            _orderDetailDal.Update(orderDetail);
            return new SuccessResult(Messages.Modified);
        }
    }
}
