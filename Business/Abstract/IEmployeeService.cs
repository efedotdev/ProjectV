using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IEmployeeService
    {
        Task<IDataResult<List<Employee>>> GetAllAsync();
        Task<IResult> AddAsync(Employee employee);
        Task<IResult> Delete(Employee employee);
        Task<IResult> Update(Employee employee);
    }
}
