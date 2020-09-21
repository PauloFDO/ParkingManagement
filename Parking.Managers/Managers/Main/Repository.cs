using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Parking.EF;
using Parking.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Parking.Managers
{
    public class Repository<TEntity, TId> : IRepository<TEntity, TId> where TEntity : class
    {
        protected ApplicationDbContext _context;
        protected DbSet<TEntity> dbSet;

        public Repository()
        {
            this._context = CreateNewContextForGeneralUse.ReturnANewContext();
            this.dbSet = _context.Set<TEntity>();
        }

        public virtual async Task<TEntity> GetByIdAsync(TId id, bool loadAllRelatedData = true)
        {
            var entity =  await this._context.Set<TEntity>().FindAsync(id);

            if (loadAllRelatedData)
            {
                foreach (var navigation in _context.Entry(entity).Navigations)
                {
                    navigation.Load();
                }
            }

            return entity;
        }

        public async Task<IQueryable<TEntity>> GetAllAsync()
        {
            IQueryable<TEntity> records = _context.Set<TEntity>();
            return records;
        }

        public async Task<IQueryable<TEntity>> GetAllItemsOfGivenEntityWithConditionAsync(Expression<Func<TEntity, bool>> whereClause, bool loadAllRelatedData = true)
        {
            IQueryable<TEntity> set = _context.Set<TEntity>();

            if (loadAllRelatedData)
            {
                foreach (var item in set)
                {
                    foreach (var navigation in _context.Entry(item).Navigations)
                    {
                        navigation.Load();
                    }
                }
            }

            IQueryable<TEntity> query = set.Where(whereClause);

            return query;
        }

        public async Task<IQueryable<TEntity>> GetAllItemsWithConditionAndIncludesAsync(Expression<Func<TEntity, bool>> whereClause, params Expression<Func<TEntity, object>>[] includeExpressions)
        {
            IQueryable<TEntity> set = _context.Set<TEntity>();

            foreach (var includeExpression in includeExpressions)
            {
                set = set.Include(includeExpression);
            }

            IQueryable<TEntity> query = set.Where(whereClause);

            return query;
        }

        public async Task<IQueryable<TEntity>> GetAllItemsOfGivenEntityWithIncludesAsync(params Expression<Func<TEntity, object>>[] includeExpressions)
        {
            IQueryable<TEntity> set = _context.Set<TEntity>();

            foreach (var includeExpression in includeExpressions)
            {
                set = set.Include(includeExpression);
            }

            return set;
        }

        public async Task<TEntity> GetSingleItemOnConditionAsync(Expression<Func<TEntity, bool>> singleClause, bool loadAllRelatedData)
        {
            IQueryable<TEntity> set = _context.Set<TEntity>();
            TEntity query = await set.SingleOrDefaultAsync(singleClause);

            if (query != null && loadAllRelatedData)
            {
                foreach (var navigation in _context.Entry(query).Navigations)
                {
                    navigation.Load();
                }
            }

            return query;
        }

        public virtual async Task CreateAsync(TEntity entity)
        {
            this._context.Set<TEntity>().Add(entity);
        }

        public virtual async Task SaveAsync()
        {
            this._context.SaveChanges();
        }

        public virtual async Task UpdateAsync(TEntity entity)
        {
            this._context.Set<TEntity>().Update(entity);
        }
    }
}
