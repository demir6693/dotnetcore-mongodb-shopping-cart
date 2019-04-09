using System.Collections.Generic;

public interface ICartService
{
    List<Cart> Get();
    Cart Get(string id);
    Cart Create(Cart cart);
    Cart AddToCart(string id, Product product);
    void Update(string id, Cart cartIn);
    void Remove(Cart cartIn);
    void Remove(string id);
    Cart RemoveFromCart(string id, Product product);
}