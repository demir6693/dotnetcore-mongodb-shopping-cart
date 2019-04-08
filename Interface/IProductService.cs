using System.Collections.Generic;

public interface IProductService
{
    List<Product> Get();
    Product Get(string id);
    Product Create(Product product);
    void Update(string id, Product productIn);
    void Remove(Product productIn);
    void Remove(string id);
    Product ReservedItems(string id, Reserved reserved);
}