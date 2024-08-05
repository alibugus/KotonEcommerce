using EcommerceProject.Models;
using EcommerceProject.Services.Interface;
using Newtonsoft.Json;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace EcommerceProject.Services
{
    public class WishListService : IWishListService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public WishListService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void ClearWishList()
        {
            SaveWishList(new List<WishListModel>());
        }

        public void AddProductToWishList(ProductModel product)
        {
            
            var wishList = GetWishList();
            var wishListItem = wishList.Find(item => item.Product.Id == product.Id);
            Console.WriteLine("AddProductToWishListgirdi");
            if (wishListItem == null)
            {
                wishList.Add(new WishListModel
                {
                    Product = product
                });
                SaveWishList(wishList);
                Console.WriteLine("AddProductToWishListifgirdi");
            }
        }


        public List<WishListModel> GetWishList()
        {
            var wishlist = _httpContextAccessor.HttpContext?.Request.Cookies["wishlist"];
            if (wishlist == null)
            {
                return new List<WishListModel>();
            }

            try
            {
                return JsonConvert.DeserializeObject<List<WishListModel>>(wishlist);
            }
            catch (JsonException)
            {
                return new List<WishListModel>();
            }
        }


        private void SaveWishList(List<WishListModel> wishList)
        {
            var wishlistJSON = JsonConvert.SerializeObject(wishList);
            var cookieOptions = new CookieOptions
            {
                Expires = System.DateTime.Now.AddDays(7)
            };
            Console.WriteLine("Savewishlistgirdi");
            _httpContextAccessor.HttpContext?.Response.Cookies.Append("wishlist", wishlistJSON, cookieOptions);
        }

        public void RemoveProductFromWishList(int productId)
        {
            var wishList = GetWishList();

            var wishListItem = wishList.Find(item => item.Product.Id == productId);

            if (wishListItem != null)
            {
                wishList.Remove(wishListItem);
                SaveWishList(wishList);
            }
        }
    }
}
