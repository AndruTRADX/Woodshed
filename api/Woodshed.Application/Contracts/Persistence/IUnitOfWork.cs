using Woodshed.Domain.Common;

namespace Woodshed.Application.Contracts.Persistence;

public interface IUnitOfWork : IDisposable
{
    IAsyncRepository<TEntity> Repository<TEntity>() where TEntity : BaseDomainModel;
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
