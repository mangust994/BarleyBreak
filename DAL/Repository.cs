using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DAL
{
    public class Repository : IRepository
    {
        private DbContext context;
        public Repository(BarleyBreakContext dbContext)
        {
            this.context = dbContext;
        }

        void IRepository.AddAndSave<TEntity>(TEntity entity)
        {
            this.context.Set<TEntity>().Add(entity);
            this.context.SaveChanges();
        }
        IEnumerable<TEntity> IRepository.GetAll<TEntity>()
        {
            return this.context.Set<TEntity>();
        }
        IEnumerable<TEntity> IRepository.GetFiltered<TEntity>(Expression<Func<TEntity, bool>> predicate)
        {
            return this.context.Set<TEntity>().Where(predicate);
        }
        IEnumerable<TEntity> IRepository.GetFilteredByQuery<TEntity>(Expression<Func<TEntity, bool>> predicate)
        {
            return this.context.Set<TEntity>().Where<TEntity>(predicate);
        }


        IEnumerable<TEntity> IRepository.GetWithInclude<TEntity>(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var query = Include(includeProperties);
            var list =  query.Where(predicate).ToList();
            return list;
        }


        TEntity IRepository.FirstorDefault<TEntity>(Expression<Func<TEntity, bool>> predicate) =>
           this.context.Set<TEntity>().FirstOrDefault(predicate);

        void IRepository.RemoveAndSave<TEntity>(TEntity entity)
        {
            this.context.Set<TEntity>().Remove(entity);
            this.context.SaveChanges();
        }

        void IRepository.UpdateAndSave<TEntity>(TEntity entity)
        {
            this.context.Entry(entity).State = EntityState.Modified;
            this.context.SaveChanges();
        }
        IQueryable<TEntity> Include<TEntity>(params Expression<Func<TEntity, object>>[] includeProperties) where TEntity : class, IEntity
        {
            IQueryable<TEntity> query = this.context.Set<TEntity>().AsNoTracking();
            return includeProperties
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }
        public void Dispose()
        {
            this.context.Dispose();
        }
    }
}
