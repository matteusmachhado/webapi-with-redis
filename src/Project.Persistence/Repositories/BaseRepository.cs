﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Project.Domain.Interfaces;
using Project.Domain.Interfaces.Repositories;
using Project.Persistence.Contexts;
using Project.Persistence.Infrastructure;
using System.Linq.Expressions;

namespace Project.Persistence.Repositories
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, IAggregateRoot
    {
        protected readonly ProjectDbContext _db;
        protected readonly DbSet<TEntity> _dbSet;
        private readonly IDistributedCache _cache;
        private readonly JsonSerializerSettings _jsonSerializerSettings;
        private readonly DistributedCacheEntryOptions options;

        protected BaseRepository(ProjectDbContext db, IDistributedCache cache)
        {
            _db = db;
            _cache = cache;
            _dbSet = db.Set<TEntity>();
            _jsonSerializerSettings = new JsonSerializerSettings
            {
                ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
                ContractResolver = new JsonConstructorPrivateResolver()
            };
            options = new DistributedCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromMinutes(5));
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(int page, 
            int pageSize,
            Expression<Func<TEntity, object>> orderBy, 
            bool asNoTracking = true, 
            CancellationToken cancellationToken = default)
        {
            var key = $"{typeof(TEntity).Name}-page-{page}-size-{pageSize}";

            string? cached = await _cache.GetStringAsync(key);

            IEnumerable<TEntity>? entities;
            if (string.IsNullOrEmpty(cached))
            {
                var query = _dbSet
                    .OrderBy(orderBy)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize);

                if (asNoTracking) query = query.AsNoTracking();

                entities = await query.ToListAsync();

                if (entities is null)
                {
                    return Enumerable.Empty<TEntity>();
                }

                await _cache.SetStringAsync(key, JsonConvert.SerializeObject(entities), options, cancellationToken);

                return entities;
            }

            entities = JsonConvert.DeserializeObject<IEnumerable<TEntity>>(cached, _jsonSerializerSettings);

            
            if (entities is not null && entities.Any())
            {
                var tracking = !asNoTracking;
                if (tracking) _db.Set<TEntity>().AttachRange(entities);

                return entities;
            }

            return Enumerable.Empty<TEntity>();
        }

        public async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            string key = $"{typeof(TEntity).Name}-{id}";

            string? cached = await _cache.GetStringAsync(key, cancellationToken);

            TEntity? entity;
            if (string.IsNullOrEmpty(cached))
            {
                entity = await _dbSet.FindAsync(id, cancellationToken);

                if (entity is null)
                {
                    return entity;
                }

                await _cache.SetStringAsync(key, JsonConvert.SerializeObject(entity), options, cancellationToken);

                return entity;
            }

            entity = JsonConvert.DeserializeObject<TEntity>(cached, _jsonSerializerSettings);

            if (entity is not null)
            {
                _db.Set<TEntity>().Attach(entity);
            }

            return entity;
        }

        public async Task Add(TEntity entity)
        {
            await _dbSet.AddAsync(entity);

            var primaryKey = _db.HasPrimaryKey(entity);

            if (!string.IsNullOrEmpty(primaryKey))
            {
                string key = $"{typeof(TEntity).Name}-{primaryKey}";
                await _cache.RemoveAsync(key);
            }
        }

        public async Task Update(TEntity entity)
        {
            _dbSet.Update(entity);

            var primaryKey = _db.HasPrimaryKey(entity);

            if (!string.IsNullOrEmpty(primaryKey))
            {
                string key = $"{typeof(TEntity).Name}-{primaryKey}";
                await _cache.RemoveAsync(key);
            }
        }

        public async void Remove(TEntity entity)
        {
            _dbSet.Remove(entity);

            var primaryKey = _db.HasPrimaryKey(entity);

            if (!string.IsNullOrEmpty(primaryKey))
            {
                string key = $"{typeof(TEntity).Name}-{primaryKey}";
                await _cache.RemoveAsync(key);
            }
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
