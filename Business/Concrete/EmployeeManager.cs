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
        public IResult Add(Employee employee)
        {
            IResult result = BusinessRules.Run();
            if (result != null)
            {
                return result;
            }
            _employeeDal.Add(employee);
            return new SuccessResult(Messages.Added);
        }

        public IResult Delete(Employee employee)
        {
            IResult result = BusinessRules.Run();
            if (result != null)
            {
                return result;
            }
            _employeeDal.Delete(employee);
            return new SuccessResult(Messages.Deleted);
        }

        public IDataResult<List<Employee>> GetAll()
        {
            return new SuccessDataResult<List<Employee>>(_employeeDal.GetAll(), Messages.Get);
        }

        public IResult Update(Employee employee)
        {
            IResult result = BusinessRules.Run();
            if (result != null)
            {
                return result;
            }
            _employeeDal.Update(employee);
            return new SuccessResult(Messages.Modified);
        }
    }
}
