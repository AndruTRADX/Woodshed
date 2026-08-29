using System.Linq.Expressions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Woodshed.Application.Contracts.Persistence;
using Woodshed.Application.Contracts.Specifications;
using Woodshed.Domain.Common;
using Woodshed.Infrastructure.Persistence;
using Woodshed.Infrastructure.Specifications;

namespace Woodshed.Infrastructure.Repositories;

public class RepositoryBase<T>(AppDbContext dbContext) : IAsyncRepository<T> where T : BaseDomainModel
{
    protected readonly AppDbContext _dbContext = dbContext;

    public async Task<IReadOnlyList<T>> GetAllAsync()
    {
        return await _dbContext.Set<T>().ToListAsync();
    }

    public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate)
    {
        return await _dbContext.Set<T>().Where(predicate).ToListAsync();
    }

    public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, List<string>? includeStrings = null, bool enableTracking = false)
    {
        IQueryable<T> query = _dbContext.Set<T>();

        if (!enableTracking)
            query = query.AsNoTracking();

        if (includeStrings is not null)
            query = includeStrings.Aggregate(query, (current, include) => current.Include(include));

        if (predicate is not null)
            query = query.Where(predicate);

        if (orderBy is not null)
            return await orderBy(query).ToListAsync();

        return await query.ToListAsync();
    }

    public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, List<Expression<Func<T, object>>>? includes = null, bool enableTracking = false)
    {
        IQueryable<T> query = _dbContext.Set<T>();

        if (!enableTracking)
            query = query.AsNoTracking();

        if (includes is not null)
            query = includes.Aggregate(query, (current, include) => current.Include(include));

        if (predicate is not null)
            query = query.Where(predicate);

        if (orderBy is not null)
            return await orderBy(query).ToListAsync();

        return await query.ToListAsync();
    }

    public async Task<T?> GetFirstAsync(Expression<Func<T, bool>> predicate)
    {
        var response = await _dbContext.Set<T>().Where(predicate).ToListAsync();
        return response.FirstOrDefault();
    }

    public async Task<T?> GetFirstAsync(Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, List<string>? includeStrings = null, bool enableTracking = false)
    {
        IQueryable<T> query = _dbContext.Set<T>();

        if (!enableTracking)
            query = query.AsNoTracking();

        if (includeStrings is not null)
            query = includeStrings.Aggregate(query, (current, include) => current.Include(include));

        if (predicate is not null)
            query = query.Where(predicate);

        if (orderBy is not null)
        {
            var response = await orderBy(query).ToListAsync();
            return response.FirstOrDefault();
        }

        var responseQuery = await query.ToListAsync();

        return responseQuery.FirstOrDefault();
    }

    public async Task<T?> GetFirstAsync(Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, List<Expression<Func<T, object>>>? includes = null, bool enableTracking = false)
    {
        IQueryable<T> query = _dbContext.Set<T>();

        if (!enableTracking)
            query = query.AsNoTracking();

        if (includes is not null)
            query = includes.Aggregate(query, (current, include) => current.Include(include));

        if (predicate is not null)
            query = query.Where(predicate);

        if (orderBy is not null)
        {
            var response = await orderBy(query).ToListAsync();
            return response.FirstOrDefault();
        }

        var responseQuery = await query.ToListAsync();

        return responseQuery.FirstOrDefault();
    }

    public void AddEntity(T entity)
    {
        _dbContext.Set<T>().Add(entity);
    }

    public void UpdateEntity(T entity)
    {
        _dbContext.Set<T>().Update(entity);
    }

    public void DeleteEntity(T entity)
    {
        _dbContext.Set<T>().Remove(entity);
    }

    public async Task<T?> GetByIdWithSpec(ISpecification<T> specification)
    {
        return await ApplySpecification(specification).FirstOrDefaultAsync();
    }

    public async Task<IReadOnlyList<T>> GetAllWithSpec(ISpecification<T> specification)
    {
        return await ApplySpecification(specification).ToListAsync();
    }

    public async Task<IReadOnlyList<TResult>> GetAllWithSpec<TResult>(ISpecification<T> specification, IConfigurationProvider configuration, CancellationToken cancellationToken = default)
    {
        return await ApplySpecification(specification)
            .ProjectTo<TResult>(configuration)
            .ToListAsync(cancellationToken);
    }

    public async Task<int> CountAsync(ISpecification<T> specification)
    {
        return await ApplySpecification(specification).CountAsync();
    }

    private IQueryable<T> ApplySpecification(ISpecification<T> specification)
    {
        return SpecificationEvaluator<T>.GetQuery(_dbContext.Set<T>().AsQueryable(), specification);
    }

    public async Task<TResult?> GetFirstAsync<TResult>(Expression<Func<T, bool>> predicate, IConfigurationProvider configuration, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<T>()
            .Where(predicate)
            .ProjectTo<TResult>(configuration)
            .FirstOrDefaultAsync(cancellationToken);
    }
}
