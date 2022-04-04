using Application.Interfaces;
using Application.Services.ProductServices.ProductDTOs;
using Domain;
using Domain.Interfaces;
using Microsoft.Extensions.Logging;
using static Application.ApplicationConstants;

namespace Application.Services.ProductServices;

public class ProductService : IProductService
{
    private readonly ILogger<ProductService> _logger;
    private readonly IProductUnitOfWork _productUnitOfWork;

    public ProductService(
        ILogger<ProductService> logger,
        IProductUnitOfWork productUnitOfWork
    )
    {
        _logger = logger;
        _productUnitOfWork = productUnitOfWork;
    }

    public async Task<Result<List<ProductDto>>> GetAllProducts(
        int pageNo,
        int pageSize,
        CancellationToken cancellationToken
    )
    {
        try
        {
            cancellationToken.ThrowIfCancellationRequested();

            var totalRecord = await _productUnitOfWork.Products.CountAsync(cancellationToken);
            var totalPage = ApplicationUtils.CalculateLastPage(totalRecord, pageSize);

            List<ProductDto> productDtoList = new();
            if (pageNo <= totalPage)
            {
                var products = await _productUnitOfWork
                    .Products.GetAllAsync(pageNo, pageSize, x => x.Id, cancellationToken);
                productDtoList = products.Select(product => new ProductDto(product)).ToList();
            }

            var pagination = new Pagination(pageNo, pageSize, productDtoList.Count, totalRecord, totalPage);

            _logger.LogInformation(SuccessfullyGetAllProducts);
            return Result<List<ProductDto>>.Success(productDtoList, pagination, SuccessfullyGetAllProducts);
        }
        catch (Exception ex) when (ex is TaskCanceledException)
        {
            _logger.LogInformation(TaskCancelled);
            return Result<List<ProductDto>>.Failure(TaskCancelled);
        }
        catch (Exception ex)
        {
            _logger.LogInformation("{Exception}", ex.InnerException?.Message ?? ex.Message);
            return Result<List<ProductDto>>.Failure(ex.InnerException?.Message ?? ex.Message);
        }
    }

    public async Task<Result<ProductDto>> GetProductById(int id, CancellationToken cancellationToken)
    {
        try
        {
            cancellationToken.ThrowIfCancellationRequested();

            var product = await _productUnitOfWork.Products.GetByIdAsync(id, cancellationToken);

            if (product is null || product.Status.Equals(DomainConstants.Disabled))
            {
                _logger.LogInformation(ProductDoesNotExist);
                return Result<ProductDto>.NotFound(ProductDoesNotExist);
            }
            
            var productDto = new ProductDto(product);

            _logger.LogInformation(SuccessfullyGetProductById);
            return Result<ProductDto>.Success(productDto, SuccessfullyGetProductById);
        }
        catch (Exception ex) when (ex is TaskCanceledException)
        {
            _logger.LogInformation(TaskCancelled);
            return Result<ProductDto>.Failure(TaskCancelled);
        }
        catch (Exception ex)
        {
            _logger.LogInformation("{Exception}", ex.InnerException?.Message ?? ex.Message);
            return Result<ProductDto>.Failure(ex.InnerException?.Message ?? ex.Message);
        }
    }
}