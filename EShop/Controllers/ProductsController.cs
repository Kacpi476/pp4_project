using EShop.Models;
using EShop.Repositories;
using EShop.Services;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Controllers;

[ApiController]
[Route("api/products")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]

    public IActionResult GetProducts()
    {
        return Ok(_productService.GetProducts());
    }

    [HttpGet("{id}")]
    public IActionResult GetProductById(int id)
    {
        var product = _productService.GetProductById(id);
        if(product == null)
            return NotFound("Product not found");
        return Ok(product);
    }

    [HttpPost]
    public IActionResult CreateProduct([FromBody] Product product)
    {
        var newProduct = _productService.AddProduct(product);
        return CreatedAtAction(nameof(GetProductById), new { id = newProduct.Id }, newProduct);
    }
}