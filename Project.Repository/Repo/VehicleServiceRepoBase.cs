using Project.DAL.DataAccess;
using Microsoft.EntityFrameworkCore;
using Project.Repository.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Project.Repository.Common.Interfaces.UOW;

namespace Project.Repository.Repo
{
    public abstract class VehicleServiceRepoBase<T> : IVehicleServiceRepoBase<T> where T : class
    {
        internal ApplicationDbContext _context;
        internal DbSet<T> _dbSet;

        public VehicleServiceRepoBase(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<T> Create(T entity)
        {
           await _dbSet.AddAsync(entity);
           return entity;
        }

        public async Task<T> Update(T entityNew, int id)
        {
            var entityOld = await _dbSet.FindAsync(id);

            if (entityOld == null) {
                return null;
            }
            _context.Entry(entityOld).CurrentValues.SetValues(entityNew);
            return entityNew;
        }

        public async Task<T> DeleteById(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null) 
            {
                return entity;
            };
            _dbSet.Attach(entity);
            _dbSet.Remove(entity);
            return entity;
        }

        public async Task<bool> FindIfExists(Expression<Func<T, bool>> expression)
        {
            return await _dbSet.AnyAsync(expression);
        }

        public IQueryable<T> FindAll()
        {
            return _dbSet.AsNoTracking();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return _dbSet.Where(expression).AsNoTracking();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

    }
}
