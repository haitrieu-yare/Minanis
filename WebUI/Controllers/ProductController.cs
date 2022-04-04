using System.ComponentModel.DataAnnotations;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : BaseApiController
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet(Name = "GetAllProducts")]
    public async Task<IActionResult> GetAllProducts(
        [Required] int pageNo,
        [Required] int pageSize,
        CancellationToken cancellationToken
    )
    {
        return HandleResult(await _productService.GetAllProducts(pageNo, pageSize, cancellationToken));
    }

    [HttpGet("{id:int}", Name = "GetProductById")]
    public async Task<IActionResult> GetProductById(int id, CancellationToken cancellationToken)
    {
        return HandleResult(await _productService.GetProductById(id, cancellationToken));
    }
}