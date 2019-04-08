using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

public class ProductService : IProductService
{
    private readonly IMongoCollection<Product> _products;

    public ProductService(IConfiguration config)
    {
        var client = new MongoClient(config.GetConnectionString("WebShopDb"));
        var database = client.GetDatabase("WebShopDb");
        _products = database.GetCollection<Product>("Products");
    }

    public List<Product> Get()
    {
        return _products.Find(product => true).ToList();
    }

    public Product Get(string id)
    {
        return _products.Find<Product>(product => product.Id == id).FirstOrDefault();
    }

    public Product Create(Product product)
    {
        _products.InsertOne(product);
        return product;
    }

    public void Update(string id, Product productIn)
    {
        _products.ReplaceOne(product => product.Id == id, productIn);
    }

    public void Remove(Product productIn)
    {
        _products.DeleteOne(product => product.Id == productIn.Id);
    }

    public void Remove(string id)
    {
        _products.DeleteOne(product => product.Id == id);
    }

    public Product ReservedItems(string id, Reserved reserved)
    {
        var filter = Builders<Product>.Filter
            .Gte(product => product.Quantity, reserved.Quantity);

        var update = Builders<Product>.Update
            .Push<Reserved>(r => r.Reserved, reserved)
            .Inc(product => product.Quantity, -reserved.Quantity);


        _products.FindOneAndUpdate(filter, update);

        return _products.Find<Product>(product => product.Id == id).FirstOrDefault();        
    }
}