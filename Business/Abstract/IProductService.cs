using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IProductService
    {
        Task<IDataResult <List<Product>>> GetAllAsync();
        Task<IResult> AddAsync(Product product);
        Task<IResult> Delete(Product product);
        Task<IResult> Update(Product product);
    }
}
