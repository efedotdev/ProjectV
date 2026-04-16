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
    public class EmployeeManager:IEmployeeService
    {
        IEmployeeDal _employeeDal;
        public EmployeeManager(IEmployeeDal employeeDal)
        {
            _employeeDal = employeeDal;
        }
        public async Task<IResult> AddAsync(Employee employee)
        {
            IResult result = BusinessRules.Run();
            if (result != null)
            {
                return result;
            }
            await _employeeDal.AddAsync(employee);
            return new SuccessResult(Messages.Added);
        }

        public async Task<IResult> Delete(Employee employee)
        {
            IResult result = BusinessRules.Run();
            if (result != null)
            {
                return result;
            }
            await _employeeDal.Delete(employee);
            return new SuccessResult(Messages.Deleted);
        }

        public async Task<IDataResult<List<Employee>>> GetAllAsync()
        {
            var data = await _employeeDal.GetAllAsync();
            return new SuccessDataResult<List<Employee>>(data,Messages.Get);
        }

        public async Task<IResult> Update(Employee employee)
        {
            IResult result = BusinessRules.Run();
            if (result != null)
            {
                return result;
            }
            await _employeeDal.Update(employee);
            return new SuccessResult(Messages.Modified);
        }
    }
}
