using Project.Domain.Entities;
using System;
using System.Linq.Expressions;

namespace Project.Domain.Interfaces.Repositories
{
    public interface IBaseRepository <TEntity> : IDisposable where TEntity : class, IAggregateRoot
    {
        Task<IEnumerable<TEntity>> GetAllAsync(int page, int pageSize, Expression<Func<TEntity, object>> orderBy, bool asNoTracking = true, CancellationToken cancellationToken = default);
        Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task Add(TEntity entity);
        Task Update(TEntity entity);
        void Remove(TEntity entity);
    }
}
