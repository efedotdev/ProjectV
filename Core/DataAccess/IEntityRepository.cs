using Core.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess
{
    public interface IEntityRepository<T>  where T : class,IEntity,new()
    {
        Task<List<T>> GetAllAsync (Expression<Func<T,bool>> filter=null);
        Task<T> GetAsync(Expression<Func<T,bool>> filter=null);
        Task AddAsync(T entity);
        Task Delete(T entity);
        Task Update(T entity);

    }
}
