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
        public IResult Add(Category Category)
        {
            IResult result = BusinessRules.Run();
            if (result != null)
            {
                return result;
            }
            _categoryDal.Add(Category);
            return new SuccessResult(Messages.Added);
        }

        public IResult Delete(Category Category)
        {
            IResult result = BusinessRules.Run();
            if (result != null)
            {
                return result;
            }
            _categoryDal.Delete(Category);
            return new SuccessResult(Messages.Deleted);
        }

        public IDataResult<List<Category>> GetAll()
        {
            return new SuccessDataResult<List<Category>>(_categoryDal.GetAll(), Messages.Get);
        }

        public IResult Update(Category Category)
        {
            IResult result = BusinessRules.Run();
            if (result != null)
            {
                return result;
            }
            _categoryDal.Update(Category);
            return new SuccessResult(Messages.Modified);
        }
    }
}
