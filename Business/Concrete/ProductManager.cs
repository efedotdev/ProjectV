using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Cahing;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Transaction;
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
        [LoggingAspect]
        [CacheRemoveAspect("IProductService.Get")]
        public async Task<IResult> AddAsync(Product product)
        {
            IResult result = BusinessRules.Run();
            if (result != null)
            {
                return result;
            }
            await _productDal.AddAsync(product);
            return new SuccessResult(Messages.Added);
        }

        [SecuredOperation("product.delete,admin")]
        [CacheRemoveAspect("IProductService.Get")]
        public async Task<IResult> Delete(Product product)
        {
            await _productDal.Delete(product);
            return new SuccessResult(Messages.Deleted);
        }
        [LoggingAspect]
        [PerformanceAspect(1/1000000000)]
        [CacheAspect(duration:10)]
        public async Task<IDataResult<List<Product>>> GetAllAsync()
        {
            var data = await _productDal.GetAllAsync();
            return new SuccessDataResult<List<Product>>(data, Messages.Get);
        }

        [SecuredOperation("product.update,admin")]
        [ValidationAspect(typeof(ProductValidator))]
        [TransactionScopeAspect]
        [LoggingAspect]
        [CacheRemoveAspect("IProductService.Get")]
        public async Task<IResult> Update(Product product)
        {

            IResult result = BusinessRules.Run(await CheckIfProductNameExists(product.ProductName, product.ProductId));
            if (result != null)
            {
                return result;
            }
            await _productDal.Update(product);
            return new SuccessResult(Messages.Modified);
        }

        // CheckIfProductNameExists metodunu ID'yi hariç tutacak şekilde güncelleyin:
        private async Task<IResult> CheckIfProductNameExists(string productName, int currentProductId)
        {
            var result = await _productDal.GetAllAsync(p => p.ProductName == productName && p.ProductId != currentProductId);
            if (result.Any())
            {
                return new ErrorResult(Messages.ProductNameAlreadyExists);
            }
            return new SuccessResult();
        }
    }
}
