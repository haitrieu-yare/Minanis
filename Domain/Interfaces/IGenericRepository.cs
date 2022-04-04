using Domain.Specifications;

namespace Domain.Interfaces;

public interface IGenericRepository<T> where T : class
{
    Task<T?> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();
    IEnumerable<T> FindWithSpecificationPattern(ISpecification<T> specification);
    // Task<int> CreateAsync(T t);
    // Task<bool> UpdateAsync(T t);
    // Task<bool> DeleteAsync(int id);
}