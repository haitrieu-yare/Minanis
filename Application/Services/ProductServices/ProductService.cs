using System.Text;
using Application.Interfaces;
using Application.Services.ProductServices.ProductDTOs;
using Domain;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
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

    public async Task<Result<ProductDto>> GetProductById(
        int id,
        CancellationToken cancellationToken
    )
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

    public async Task<Result<bool>> CreateProduct(
        ProductCreationDto productCreationDto,
        CancellationToken cancellationToken
    )
    {
        try
        {
            cancellationToken.ThrowIfCancellationRequested();

            var product = new Product
            {
                Name = productCreationDto.Name!,
                BuyingPrice = productCreationDto.BuyingPrice!.Value,
                SellingPrice = productCreationDto.SellingPrice!.Value,
                Quantity = productCreationDto.Quantity!.Value,
            };

            await _productUnitOfWork.Products.CreateAsync(product, cancellationToken);
            var result = await _productUnitOfWork.SaveChangesAsync(cancellationToken) > 0;

            if (!result)
            {
                _logger.LogInformation(FailedToCreateNewProduct);
                return Result<bool>.Failure(FailedToCreateNewProduct);
            }

            _logger.LogInformation(SuccessfullyCreateNewProduct);
            return Result<bool>.Created(product.Id.ToString(), SuccessfullyCreateNewProduct);
        }
        catch (Exception ex) when (ex is TaskCanceledException)
        {
            _logger.LogInformation(TaskCancelled);
            return Result<bool>.Failure(TaskCancelled);
        }
        catch (DbUpdateException ex) when (ex.InnerException is SqlException)
        {
            if (ex.InnerException is SqlException {Number: SqlExceptionErrorCode})
            {
                var exceptionErrorMessage = ex.InnerException?.Message;
                var errorMessage = new StringBuilder(CanNotInsertDuplicatedValue);
                errorMessage.Append('.');

                if (exceptionErrorMessage is not null)
                {
                    errorMessage.Append(' ');
                    errorMessage.Append(TheDuplicatedValueIs);
                    errorMessage.Append(" (");
                    errorMessage.Append(exceptionErrorMessage.Split('(')[1].Split(')')[0]);
                    errorMessage.Append(").");
                }

                _logger.LogInformation("{Exception}", errorMessage.ToString());
                return Result<bool>.Failure(errorMessage.ToString());
            }

            _logger.LogInformation("{Exception}", ex.InnerException?.Message ?? ex.Message);
            return Result<bool>.Failure(ex.InnerException?.Message ?? ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogInformation("{Exception}", ex.InnerException?.Message ?? ex.Message);
            return Result<bool>.Failure(ex.InnerException?.Message ?? ex.Message);
        }
    }

    public async Task<Result<bool>> UpdateProduct(ProductDto productDto, CancellationToken cancellationToken)
    {
        try
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (!productDto.Id.HasValue)
            {
                _logger.LogInformation(FailedToUpdateProduct);
                return Result<bool>.Failure(FailedToUpdateProduct);
            }

            var product = await _productUnitOfWork.Products.GetByIdAsync(productDto.Id.Value, cancellationToken);

            if (product is null)
            {
                _logger.LogInformation(FailedToUpdateProduct);
                return Result<bool>.Failure(FailedToUpdateProduct);
            }

            product.Name = productDto.Name ?? product.Name;
            product.BuyingPrice = productDto.BuyingPrice ?? product.BuyingPrice;
            product.SellingPrice = productDto.SellingPrice ?? product.SellingPrice;
            product.Quantity = productDto.Quantity ?? product.Quantity;

            var result = await _productUnitOfWork.SaveChangesAsync(cancellationToken) > 0;

            if (!result)
            {
                _logger.LogInformation(FailedToUpdateProduct);
                return Result<bool>.Failure(FailedToUpdateProduct);
            }

            _logger.LogInformation(SuccessfullyUpdateProduct);
            return Result<bool>.Success(true, SuccessfullyUpdateProduct);
        }
        catch (Exception ex) when (ex is TaskCanceledException)
        {
            _logger.LogInformation(TaskCancelled);
            return Result<bool>.Failure(TaskCancelled);
        }
        catch (DbUpdateException ex) when (ex.InnerException is SqlException)
        {
            if (ex.InnerException is SqlException {Number: SqlExceptionErrorCode})
            {
                var exceptionErrorMessage = ex.InnerException?.Message;
                var errorMessage = new StringBuilder(CanNotInsertDuplicatedValue);
                errorMessage.Append('.');

                if (exceptionErrorMessage is not null)
                {
                    errorMessage.Append(' ');
                    errorMessage.Append(TheDuplicatedValueIs);
                    errorMessage.Append(" (");
                    errorMessage.Append(exceptionErrorMessage.Split('(')[1].Split(')')[0]);
                    errorMessage.Append(").");
                }

                _logger.LogInformation("{Exception}", errorMessage.ToString());
                return Result<bool>.Failure(errorMessage.ToString());
            }

            _logger.LogInformation("{Exception}", ex.InnerException?.Message ?? ex.Message);
            return Result<bool>.Failure(ex.InnerException?.Message ?? ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogInformation("{Exception}", ex.InnerException?.Message ?? ex.Message);
            return Result<bool>.Failure(ex.InnerException?.Message ?? ex.Message);
        }
    }
}