using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public ActionResult<List<Product>> Get()
    {   
        return _productService.Get();
    }

    [HttpGet("{id:length(24)}", Name = "GetProduct")]
    public ActionResult<Product> Get(string id)
    {
        var product = _productService.Get(id);

        if (product == null)
        {
            return NotFound();
        }

        return product;
    }

    [HttpPost]
    public ActionResult<Product> Create(Product product)
    {
        _productService.Create(product);

        return CreatedAtRoute("GetProduct", new { id = product.Id.ToString() }, product);
    }

    [HttpPut("{id:length(24)}")]
    public IActionResult Update(string id, Product productIn)
    {
        var product = _productService.Get(id);

        if (product == null)
        {
            return NotFound();
        }

        _productService.Update(id, productIn);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public IActionResult Delete(string id)
    {
        var product = _productService.Get(id);

        if (product == null)
        {
            return NotFound();
        }

        _productService.Remove(product.Id);

        return NoContent();
    }

    [HttpPost("reserved_item/{id:length(24)}")]
    public ActionResult<Product> ReservedItems(string id, Reserved reserved)
    {
        return _productService.ReservedItems(id, reserved);
    }
}