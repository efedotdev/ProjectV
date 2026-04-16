using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IDepartmentService
    {
        Task<IDataResult<List<Department>>> GetAllAsync();
        Task<IResult> AddAsync(Department department);
        Task<IResult> Delete(Department department);
        Task<IResult> Update(Department department);
    }
}
