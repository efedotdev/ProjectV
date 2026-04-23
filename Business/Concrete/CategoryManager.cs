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
    public class CategoryManager : ICategoryService
    {
        ICategoryDal _categoryDal;
        public CategoryManager(ICategoryDal CategoryDal)
        {
            _categoryDal = CategoryDal;
        }
        public async Task<IResult> AddAsync(Category Category)
        {
            IResult result = BusinessRules.Run();
            if (result != null)
            {
                return result;
            }
            await _categoryDal.AddAsync(Category);
            return new SuccessResult(Messages.Added);
        }

        public async Task<IResult> Delete(Category Category)
        {
            IResult result = BusinessRules.Run();
            if (result != null)
            {
                return result;
            }
            await _categoryDal.Delete(Category);
            return new SuccessResult(Messages.Deleted);
        }

        public async Task<IDataResult<List<Category>>> GetAllAsync()
        {
            var data = await _categoryDal.GetAllAsync();
            return new SuccessDataResult<List<Category>>(data,Messages.Get);
            
        }

        public async Task<IResult> Update(Category Category)
        {
            IResult result = BusinessRules.Run();
            if (result != null)
            {
                return result;
            }
            await _categoryDal.Update(Category);
            return new SuccessResult(Messages.Modified);
        }
    }
}
