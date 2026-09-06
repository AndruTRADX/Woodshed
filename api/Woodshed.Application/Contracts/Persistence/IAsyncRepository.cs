using System.Linq.Expressions;
using AutoMapper;
using Woodshed.Application.Contracts.Specifications;
using Woodshed.Domain.Common;

namespace Woodshed.Application.Contracts.Persistence;

public interface IAsyncRepository<T> where T : BaseDomainModel
{
    Task<IReadOnlyList<T>> GetAllAsync();

    Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate);
    Task<IReadOnlyList<T>> GetAsync(
        Expression<Func<T, bool>>? predicate = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        bool enableTracking = false
    );
    Task<IReadOnlyList<T>> GetAsync(
        List<string> includeStrings,
        Expression<Func<T, bool>>? predicate = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        bool enableTracking = false
    );
    Task<IReadOnlyList<T>> GetAsync(
        List<Expression<Func<T, object>>> includes,
        Expression<Func<T, bool>>? predicate = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        bool enableTracking = false
    );

    Task<T?> GetFirstAsync(Expression<Func<T, bool>> predicate);
    Task<T?> GetFirstAsync(
        Expression<Func<T, bool>>? predicate = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        bool enableTracking = false
    );
    Task<T?> GetFirstAsync(
        List<string> includeStrings,
        Expression<Func<T, bool>>? predicate = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        bool enableTracking = false
    );
    Task<T?> GetFirstAsync(
        List<Expression<Func<T, object>>> includes,
        Expression<Func<T, bool>>? predicate = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        bool enableTracking = false
    );
    Task<TResult?> GetFirstAsync<TResult>(
        Expression<Func<T, bool>> predicate,
        IConfigurationProvider configuration,
        CancellationToken cancellationToken = default);

    void AddEntity(T entity);
    void UpdateEntity(T entity);
    void DeleteEntity(T entity);

    Task<T?> GetByIdWithSpec(ISpecification<T> specification);
    Task<IReadOnlyList<T>> GetAllWithSpec(ISpecification<T> specification);
    Task<IReadOnlyList<TResult>> GetAllWithSpec<TResult>(ISpecification<T> specification, IConfigurationProvider configuration, CancellationToken cancellationToken = default);
    Task<int> CountAsync(ISpecification<T> specification);
}