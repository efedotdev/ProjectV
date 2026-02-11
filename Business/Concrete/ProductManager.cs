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
    public class ProductManager : IProductService
    {
        IProductDal _productDal;
        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }
        public IResult Add(Product product)
        {
            IResult result = BusinessRules.Run();
            if (result!=null)
            {
                return result;
            }
               _productDal.Add(product);
            return new SuccessResult(Messages.Added);
        }

        public IResult Delete(Product product)
        {
            IResult result = BusinessRules.Run();
            if (result != null)
            {
                return result;
            }
            _productDal.Delete(product);
            return new SuccessResult(Messages.Deleted);
        }

        public IDataResult<List<Product>> GetAll()
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(), Messages.Get);
        }

        public IResult Update(Product product)
        {
            IResult result = BusinessRules.Run();
            if (result != null)
            {
                return result;
            }
            _productDal.Update(product);
            return new SuccessResult(Messages.Modified);
        }
    }
}
