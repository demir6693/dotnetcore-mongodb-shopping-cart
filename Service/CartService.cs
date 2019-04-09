using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

public class CartService : ICartService
{
    private readonly IMongoCollection<Cart> _carts;

    public CartService(IConfiguration config)
    {
        var client = new MongoClient(config.GetConnectionString("WebShopDb"));
        var database = client.GetDatabase("WebShopDb");
        _carts = database.GetCollection<Cart>("Carts");
    }

    public List<Cart> Get()
    {
        return _carts.Find(cart => true).ToList();
    }

    public Cart Get(string id)
    {
        return _carts.Find<Cart>(cart => cart.Id == id).FirstOrDefault();
    }

    public Cart Create(Cart cart)
    {   
        _carts.InsertOne(cart);
        return cart;
    }

    public void Update(string id, Cart cartIn)
    {
        _carts.ReplaceOne(cart => cart.Id == id, cartIn);
    }

    public void Remove(Cart cartIn)
    {
        _carts.DeleteOne(cart => cart.Id == cartIn.Id);
    }

    public void Remove(string id)
    {
        _carts.DeleteOne(cart => cart.Id == id);
    }

    public Cart AddToCart(string id, Product product)
    {   
        DateTime dt = DateTime.Now;

        string Status = "Active";

        string modofied_on = dt.ToShortDateString();

        var filter = Builders<Cart>
                    .Filter.Eq(cart => cart.Id, id);

        var update = Builders<Cart>.Update
                    .Push<Product>(p => p.Product, product)
                    .Set(cart => cart.Status, Status)
                    .Set(cart => cart.modified_on, modofied_on);;

        _carts.FindOneAndUpdate(filter, update);
        
        return _carts.Find<Cart>(cart => cart.Id == id).FirstOrDefault();
    }

    public void NotEnoughInventory(string id, Product productIn)
    {   
        DateTime dt = DateTime.Now;
        string modofied_on = dt.ToShortDateString();

        var filter = Builders<Cart>
                    .Filter.Eq(cart => cart.Id, id);
        
        var update = Builders<Cart>.Update
                     .Set(cart => cart.modified_on, modofied_on)
                     .Pull(cart => cart.Product, productIn);
        
        _carts.FindOneAndUpdate(filter, update);
    }

    public Cart RemoveFromCart(string id, Product product)
    {
        DateTime dt = DateTime.Now;
        string modofied_on = dt.ToShortDateString();

        var filter = Builders<Cart>
                    .Filter.Eq(cart => cart.Id, id);

        var update = Builders<Cart>.Update
                    .Set(cart => cart.modified_on, modofied_on)
                    .Pull(cart => cart.Product, product);
        
        var result = _carts.FindOneAndUpdate(filter, update);
        /* 
        if(result != null)
        {   
            Reserved reserved = new Reserved{
                Id = id
            };
            ProductService ps = new ProductService();
            ps.UpdateAfterDeleteFromCart(product, reserved);
        }*/
        
        return result;
    }
}