using EntitiesCL.DataAccess;
using Microsoft.EntityFrameworkCore;
using ServicesCL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ServicesCL.Repo
{
    public abstract class VehicleServiceRepoBase<T> : IVehicleServiceRepoBase<T> where T : class
    {
        //private readonly ApplicationDbContext _context = null;
        private readonly DbSet<T> _dbSet = null;

        public VehicleServiceRepoBase(ApplicationDbContext _context)
        {
            //this._context = _context;
            _dbSet = _context.Set<T>();
        }

        public void Create(T entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(T entity /*object id*/)
        {
            //T existing = _dbSet.Find(id);
            _dbSet.Remove(entity/*existing*/);
        }

        public IQueryable<T> FindAll()
        {
            return _dbSet.AsNoTracking();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return _dbSet.Where(expression).AsNoTracking();
        }

        public void Update(T entity)
        {
            //_dbSet.Attach(entity);
            //_context.Entry(entity).State = EntityState.Modified;
            _dbSet.Update(entity);
        }
    }
}
