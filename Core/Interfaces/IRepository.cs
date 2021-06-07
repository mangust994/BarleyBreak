using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Core.Interfaces
{
    public interface IRepository : IDisposable
    {
        IEnumerable<TEntity> GetAll<TEntity>() where TEntity : class, IEntity;
        IEnumerable<TEntity> GetFiltered<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class, IEntity;
        TEntity FirstorDefault<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class;

        IEnumerable<TEntity> GetFilteredByQuery<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class;

        IEnumerable<TEntity> GetWithInclude<TEntity>(Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, object>>[] includeProperties) where TEntity : class, IEntity;

        void AddAndSave<TEntity>(TEntity entity) where TEntity : class;

        void UpdateAndSave<TEntity>(TEntity entity) where TEntity : class;

        void RemoveAndSave<TEntity>(TEntity entity) where TEntity : class;

    }
}
