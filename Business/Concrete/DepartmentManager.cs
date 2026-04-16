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
    public class DepartmentManager:IDepartmentService
    {
        IDepartmentDal _departmentDal;
        public DepartmentManager(IDepartmentDal departmentDal)
        {
            _departmentDal = departmentDal;
        }
        public async Task<IResult> AddAsync(Department department)
        {
            IResult result = BusinessRules.Run();
            if (result != null)
            {
                return result;
            }
            await _departmentDal.AddAsync(department);
            return new SuccessResult(Messages.Added);
        }

        public async Task<IResult> Delete(Department department)
        {
            IResult result = BusinessRules.Run();
            if (result != null)
            {
                return result;
            }
            await _departmentDal.Delete(department);
            return new SuccessResult(Messages.Deleted);
        }

        public async Task<IDataResult<List<Department>>> GetAllAsync()
        {
            var data = await _departmentDal.GetAllAsync();
            return new SuccessDataResult<List<Department>>(data, Messages.Get);
        }

        public async Task<IResult> Update(Department department)
        {
            IResult result = BusinessRules.Run();
            if (result != null)
            {
                return result;
            }
            await _departmentDal.Update(department);
            return new SuccessResult(Messages.Modified);
        }
    }
}
