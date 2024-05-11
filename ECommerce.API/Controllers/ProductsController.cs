using ECommerce.API.Repositories;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductRepository _product;

    public ProductsController(IProductRepository product)
    {
        _product = product ?? throw new ArgumentNullException(nameof(product));
    }

    [HttpGet("some")]
    public async Task<IActionResult> GetSomeOfProducts([FromQuery] int page = 1, [FromQuery] int pageSize = 6)
    {
        try
        {
            if (page > 0 && pageSize > 0)
            {
                var products = await _product.GetSomeProductsAsync(page, pageSize);
                return Ok(products);
            }
            else
            {
                return BadRequest(new {message = "page and pageSize should be greater than 0." });
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
    [HttpGet("ById")]
    public async Task<IActionResult> GetProductById([FromQuery] int id)
    {
        try
        {
            var product = await _product.GetProductByIdAsync(id);

            if (product != null)
            {
                return Ok(product);
            }
            else
            {
                return NotFound(new {message = $"Product with ID {id} not found." });
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
    [HttpGet("all")]
    public async Task<IActionResult> GetAllProducts()
    {
        try
        {
            var products = await _product.GetAllProductsAsync();
            return Ok(products);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}