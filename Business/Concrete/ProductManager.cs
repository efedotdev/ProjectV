using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Cahing;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        IProductDal _productDal;
        ICategoryService _categoryService;
        IOrderDetailService _orderDetailService;
        public ProductManager(IProductDal productDal, ICategoryService categoryService, IOrderDetailService orderDetailService)
        {
            _productDal = productDal;
            _categoryService = categoryService;
            _orderDetailService = orderDetailService;

        }
        public IResult Add(Product product)
        {
            IResult result = BusinessRules.Run();
            if (result != null)
            {
                return result;
            }
            _productDal.Add(product);
            return new SuccessResult(Messages.Added);
        }

        [SecuredOperation("product.delete,admin")]
        [CacheRemoveAspect("IProductService.Get")]
        public IResult Delete(Product product)
        {
            IResult result = BusinessRules.Run(CheckIfProductUsedInOrders(product.ProductId));
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

        [SecuredOperation("product.update,admin")]
        [ValidationAspect(typeof(ProductValidator))]
        [CacheRemoveAspect("IProductService.Get")]
        public IResult Update(Product product)
        {

            IResult result = BusinessRules.Run(CheckIfProductNameExists(product.ProductName));
            if (result != null)
            {
                return result;
            }
            _productDal.Update(product);
            return new SuccessResult(Messages.Modified);
        }

        private IResult CheckIfProductNameExists(string productName)
        {
            var result = _productDal.GetAll(p => p.ProductName == productName).Any();
            if (result)
            {
                return new ErrorResult(Messages.ProductNameAlreadyExists);
            }
            return new SuccessResult();
        }
        private IResult CheckIfProductUsedInOrders(int productId)
        {
            var result = _orderDetailService.GetAll();
            if (result.Data.Any(p => p.ProductId == productId))
            {
                return new ErrorResult(Messages.PreviouslyUsedProduct);
            }
            return new SuccessResult();
        }
    }
}
