using EcommerceProject.Models;
using System.Collections.Generic;

public interface ICartService
{
    void AddProductToCart(ProductModel product, int quantity);
    public void RemoveProductFromCart(int productId);
    public void IncreaseProductQuantity(int productId);
    public void DecreaseProductQuantity(int productId);
    public void ClearCart();
    List<CartItemModel> GetCart();
}
