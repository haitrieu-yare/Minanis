using System.Linq.Expressions;
using Domain.Interfaces;
using Domain.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly DataContext _context;

    public GenericRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<int> CountAsync(CancellationToken cancellationToken)
    {
        return await _context.Set<T>()
            .CountAsync(cancellationToken);
    }

    public async Task<IEnumerable<T>> GetAllAsync(
        int pageNo,
        int pageSize,
        Expression<Func<T, object>> orderBy,
        CancellationToken cancellationToken
    )
    {
        return await _context.Set<T>()
            .OrderBy(orderBy)
            .Skip((pageNo - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }

    public async Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _context.Set<T>().FindAsync(new object?[] {id}, cancellationToken);
    }

    public async Task<List<T>> FindWithSpecificationPattern(
        ISpecification<T> specification,
        CancellationToken cancellationToken
    )
    {
        return await SpecificationEvaluator<T>
            .GetQuery(_context.Set<T>().AsQueryable(), specification)
            .ToListAsync(cancellationToken);
    }

    public async Task CreateAsync(T t, CancellationToken cancellationToken)
    {
        await _context.Set<T>().AddAsync(t, cancellationToken);
    }
}