﻿using Application.Services.ProductServices.ProductDTOs;

namespace Application.Interfaces;

public interface IProductService
{
    public Task<Result<List<ProductDto>>> GetAllProducts(
        int pageNo,
        int pageSize,
        CancellationToken cancellationToken
    );
}