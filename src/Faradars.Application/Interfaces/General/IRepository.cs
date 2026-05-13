using System.Data.Common;
using System.Linq.Expressions;
using Faradars.Domain.Interfaces;

namespace Faradars.Application.Interfaces.General;

public interface IRepository<TEntity> where TEntity : class, IEntity
{
    IQueryable<TEntity> Table { get; }
    IQueryable<TEntity> TableNoTracking { get; }

    Task<List<T>> RawSqlQuery<T>(string query, Func<DbDataReader, T> map);

    TEntity? Get(Expression<Func<TEntity, bool>> filter);
    Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken = default);
    TEntity? GetById(params object[] ids);
    Task<TEntity?> GetByIdAsync(CancellationToken cancellationToken, params object[] ids);
    
    void Add(TEntity entity, bool saveNow = true);
    Task AddAsync(TEntity entity, CancellationToken cancellationToken, bool saveNow = true);
    void AddRange(IEnumerable<TEntity> entities, bool saveNow = true);
    Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken, bool saveNow = true);

    void Update(TEntity entity, bool saveNow = true);
    Task UpdateAsync(TEntity entity, CancellationToken cancellationToken, bool saveNow = true);
    void UpdateRange(IEnumerable<TEntity> entities, bool saveNow = true);
    Task UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken, bool saveNow = true);
    
    void Delete(TEntity entity, bool saveNow = true);
    Task DeleteAsync(TEntity entity, CancellationToken cancellationToken, bool saveNow = true);
    void DeleteRange(IEnumerable<TEntity> entities, bool saveNow = true);
    Task DeleteRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken, bool saveNow = true);

    void Attach(TEntity entity);
    void Detach(TEntity entity);

    void LoadCollection<TProperty>(TEntity entity, Expression<Func<TEntity, IEnumerable<TProperty>>> collectionProperty)
        where TProperty : class;

    Task LoadCollectionAsync<TProperty>(TEntity entity,
        Expression<Func<TEntity, IEnumerable<TProperty>>> collectionProperty, CancellationToken cancellationToken)
        where TProperty : class;

    void LoadReference<TProperty>(TEntity entity, Expression<Func<TEntity, TProperty?>> referenceProperty)
        where TProperty : class;

    Task LoadReferenceAsync<TProperty>(TEntity entity,
        Expression<Func<TEntity, TProperty?>> referenceProperty, CancellationToken cancellationToken)
        where TProperty : class;
}