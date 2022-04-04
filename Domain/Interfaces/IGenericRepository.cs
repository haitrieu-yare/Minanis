using System.Linq.Expressions;
using Domain.Specifications;

namespace Domain.Interfaces;

public interface IGenericRepository<T> where T : class
{
    Task<int> CountAsync(CancellationToken cancellationToken);

    Task<IEnumerable<T>> GetAllAsync(
        int pageNo,
        int pageSize,
        Expression<Func<T, object>> orderBy,
        CancellationToken cancellationToken
    );

    Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<List<T>> FindWithSpecificationPattern(ISpecification<T> specification, CancellationToken cancellationToken);

    Task CreateAsync(T t, CancellationToken cancellationToken);
}