using Domain.Entities;
using Domain.Interfaces;
using Domain.Specifications.Products;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly ILogger<ProductController> _logger;
    private readonly IGenericRepository<Product> _repository;

    public ProductController(
        ILogger<ProductController> logger,
        IGenericRepository<Product> repository
    )
    {
        _logger = logger;
        _repository = repository;
    }

    [HttpGet(Name = "GetProductByProfit")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult GetProductByProfit()
    {
        _logger.LogInformation("Successfully get product information by profit");
        var specification = new ProductByProfit();
        var products = _repository.FindWithSpecificationPattern(specification);
        return Ok(products);
    }
}