using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Repositories;

namespace Infrastructure.UnitOfWorks;

public class ProductUnitOfWork : IProductUnitOfWork
{
    private readonly DataContext _context;

    public ProductUnitOfWork(DataContext context)
    {
        _context = context;
        Products = new GenericRepository<Product>(_context);
    }

    public IGenericRepository<Product> Products { get; }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }
}