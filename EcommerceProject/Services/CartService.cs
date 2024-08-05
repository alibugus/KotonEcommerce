using EcommerceProject.Models;
using EcommerceProject.Services.Interface;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Collections.Generic;

public class CartService : ICartService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CartService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public void RemoveProductFromCart(int productId)
    {
        //Mevcut sepeti al
        var cart = GetCart();
        //Sepetten ürünü bul
        var cartItem = cart.Find(item => item.Product.Id == productId);
        //Eğer ürün sepette varsa sepetten çıkar
        if (cartItem != null)
        {
            cart.Remove(cartItem);
            SaveCart(cart);
        }
    }
    public void ClearCart()
    {
        //Sepeti temizle
        SaveCart(new List<CartItemModel>());
    }
    public void IncreaseProductQuantity(int productId)
    {
        //Mevcut sepeti al
        var cart = GetCart();
        //Sepetten ürünü bul
        var cartItem = cart.Find(item => item.Product.Id == productId);
        //Eğer ürün sepette varsa, miktarını güncelle
        if (cartItem != null)
        {
            cartItem.Quantity++; 
            SaveCart(cart);
        }
    }
    public void DecreaseProductQuantity(int productId)
    {
        //Mevcut sepeti al
        var cart = GetCart();
        //Sepetten ürünü bul
        var cartItem = cart.Find(item => item.Product.Id == productId);
        //Eğer ürün sepette varsa, miktarını güncelle
        if (cartItem != null)
        {
            cartItem.Quantity--;
            if (cartItem.Quantity == 0)
            {
                cart.Remove(cartItem);
            }
            SaveCart(cart);
        }
    }
    public void AddProductToCart(ProductModel product, int quantity)
    {
        //Mevcut sepeti al
        var cart = GetCart();
        var cartItem = cart.Find(item => item.Product.Id == product.Id);
        //Eğer ürün sepette varsa, miktarını arttır
        if (cartItem != null)
        {
            cartItem.Quantity += quantity;
        }
        //Eğer ürün sepette yoksa, yeni bir ürün olarak ekle
        else
        {
            cart.Add(new CartItemModel
            {
                Product = product,
                Quantity = quantity
            });
        }

        SaveCart(cart);
    }

    public List<CartItemModel> GetCart()
    {
        //Cookie'den sepeti al
        var cart = _httpContextAccessor.HttpContext.Request.Cookies["Cart"];
        if (cart == null)
        {
            return new List<CartItemModel>();
        }
        //Cookie'den alınan sepeti listeye çevirerek döndür
        return JsonConvert.DeserializeObject<List<CartItemModel>>(cart);
    }
    //Sepeti kaydet
    private void SaveCart(List<CartItemModel> cart)
    {
        //Sepeti JSON formatına çevir
        var cartJson = JsonConvert.SerializeObject(cart);
        //Cookie'ye sepeti kaydet
        var cookieOptions = new CookieOptions
        {
            Expires = System.DateTime.Now.AddDays(7)
        };
        //Cookie'yi response'a ekle
        _httpContextAccessor.HttpContext.Response.Cookies.Append("Cart", cartJson, cookieOptions);
    }
}
