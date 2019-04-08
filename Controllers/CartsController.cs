using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class CartsController : ControllerBase
{
    private readonly ICartService _cartService;

    public CartsController(ICartService cartService)
    {
        _cartService = cartService;
    }

    [HttpGet]
    public ActionResult<List<Cart>> Get()
    {
        return _cartService.Get();
    }

    [HttpGet("{id:length(24)}", Name = "GetCart")]
    public ActionResult<Cart> Get(string id)
    {
        var cart = _cartService.Get(id);

        if (cart == null)
        {
            return NotFound();
        }

        return cart;
    }

    [HttpPost]
    public ActionResult<Cart> Create(Cart cart)
    {
        _cartService.Create(cart);

        return CreatedAtRoute("GetCart", new { id = cart.Id.ToString() }, cart);
    }

    [HttpPost("addtocart/{id:length(24)}")]
    public ActionResult<Cart> AddToCart(string id, Product Product)
    {
        return _cartService.AddToCart(id, Product);
    }

    [HttpPut("{id:length(24)}")]
    public IActionResult Update(string id, Cart cartIn)
    {
        var cart = _cartService.Get(id);

        if (cart == null)
        {
            return NotFound();
        }

        _cartService.Update(id, cartIn);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public IActionResult Delete(string id)
    {
        var cart = _cartService.Get(id);

        if (cart == null)
        {
            return NotFound();
        }

        _cartService.Remove(cart.Id);

        return NoContent();
    }
}