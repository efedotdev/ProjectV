using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICategoryService
    {
        Task<IDataResult<List<Category>>> GetAllAsync();
        Task<IResult> AddAsync(Category category);
        Task<IResult> Delete(Category category);
        Task<IResult> Update(Category category);
    }
}
