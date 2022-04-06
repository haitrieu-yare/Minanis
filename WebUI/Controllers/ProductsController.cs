using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Application.Interfaces;
using Application.Services.ProductServices.ProductDTOs;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductsController : BaseApiController
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProducts(
        [Required][DefaultValue(1)] int pageNo,
        [Required][DefaultValue(10)] int pageSize,
        CancellationToken cancellationToken
    )
    {
        return HandleResult(await _productService.GetAllProducts(pageNo, pageSize, cancellationToken));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetProductById(
        int id,
        CancellationToken cancellationToken
    )
    {
        return HandleResult(await _productService.GetProductById(id, cancellationToken));
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct(
        ProductCreationDto productCreationDto,
        CancellationToken cancellationToken
    )
    {
        return HandleResult(await _productService.CreateProduct(productCreationDto, cancellationToken));
    }

    [HttpPut]
    public async Task<IActionResult> UpdateProduct(
        ProductDto productDto,
        CancellationToken cancellationToken
    )
    {
        return HandleResult(await _productService.UpdateProduct(productDto, cancellationToken));
    }
}