using Domain.Entities;

namespace Domain.Interfaces;

public interface IProductUnitOfWork
{
    IGenericRepository<Product> Products { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}