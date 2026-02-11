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
        public IResult Add(Department department)
        {
            IResult result = BusinessRules.Run();
            if (result != null)
            {
                return result;
            }
            _departmentDal.Add(department);
            return new SuccessResult(Messages.Added);
        }

        public IResult Delete(Department department)
        {
            IResult result = BusinessRules.Run();
            if (result != null)
            {
                return result;
            }
            _departmentDal.Delete(department);
            return new SuccessResult(Messages.Deleted);
        }

        public IDataResult<List<Department>> GetAll()
        {
            return new SuccessDataResult<List<Department>>(_departmentDal.GetAll(), Messages.Get);
        }

        public IResult Update(Department department)
        {
            IResult result = BusinessRules.Run();
            if (result != null)
            {
                return result;
            }
            _departmentDal.Update(department);
            return new SuccessResult(Messages.Modified);
        }
    }
}
