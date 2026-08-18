using Woodshed.Application.Contracts.Persistence;
using Woodshed.Domain.Common;
using Woodshed.Infrastructure.Persistence;

namespace Woodshed.Infrastructure.Repositories;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    private readonly AppDbContext _context = context;
    private readonly Dictionary<Type, object> _repositories = [];

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public IAsyncRepository<TEntity> Repository<TEntity>() where TEntity : BaseDomainModel
    {
        var type = typeof(TEntity);

        if (!_repositories.TryGetValue(type, out var repository))
        {
            repository = new RepositoryBase<TEntity>(_context);
            _repositories[type] = repository;
        }

        return (IAsyncRepository<TEntity>)repository;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            _context.Dispose();
        }
    }
}
