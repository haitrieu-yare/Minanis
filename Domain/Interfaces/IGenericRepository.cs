using Domain.Specifications;

namespace Domain.Interfaces;

public interface IGenericRepository<T> where T : class
{
    Task<int> CountAsync(CancellationToken cancellationToken);
    Task<IEnumerable<T>> GetAllAsync(int pageNo, int pageSize, CancellationToken cancellationToken);
    Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<List<T>> FindWithSpecificationPattern(ISpecification<T> specification, CancellationToken cancellationToken);

    // Task<int> CreateAsync(T t);
    // Task<bool> UpdateAsync(T t);
    // Task<bool> DeleteAsync(int id);
}