using Project.Repository.Common.Interfaces.UOW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Project.Repository.Common.Interfaces
{
    public interface IVehicleServiceRepoBase<T>
    {
        IQueryable<T> FindAll();
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
        Task<bool> FindIfExists(Expression<Func<T, bool>> expression);
        Task<T> Create(T entity);
        Task<T> Update(T entity, int id); 
        Task<T> DeleteById(int id);
    }
}
