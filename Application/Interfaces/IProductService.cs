using Application.Services.ProductServices.ProductDTOs;

namespace Application.Interfaces;

public interface IProductService
{
    public Task<Result<List<ProductDto>>> GetAllProducts(
        int pageNo,
        int pageSize,
        CancellationToken cancellationToken
    );

    public Task<Result<ProductDto>> GetProductById(int id, CancellationToken cancellationToken);

    public Task<Result<bool>> CreateProduct(ProductCreationDto productCreationDto, CancellationToken cancellationToken);
    public Task<Result<bool>> UpdateProduct(ProductDto productDto, CancellationToken cancellationToken);
}