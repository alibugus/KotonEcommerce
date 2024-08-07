using EcommerceProject.Models;
using EcommerceProject.Services.Interface;
using Newtonsoft.Json;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace EcommerceProject.Services
{
    public class WishListService : IWishListService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<AppUser> _userManager;

        public WishListService(IHttpContextAccessor httpContextAccessor, UserManager<AppUser> userManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public void ClearWishList()
        {
            var userId = GetUserId();
            SaveWishList(new List<WishListModel>(), userId);
        }

        public void AddProductToWishList(ProductModel product)
        {
            var userId = GetUserId();
            var wishList = GetWishList(userId);
            var wishListItem = wishList.Find(item => item.Product.Id == product.Id);
            if (wishListItem == null)
            {
                wishList.Add(new WishListModel
                {
                    Product = product
                });
                SaveWishList(wishList, userId);
            }
        }

        public List<WishListModel> GetWishList()
        {
            var userId = GetUserId();
            return GetWishList(userId);
        }

        private List<WishListModel> GetWishList(string userId)
        {
            var wishlist = _httpContextAccessor.HttpContext?.Request.Cookies[$"wishlist_{userId}"];
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

        private void SaveWishList(List<WishListModel> wishList, string userId)
        {
            var wishlistJSON = JsonConvert.SerializeObject(wishList);
            var cookieOptions = new CookieOptions
            {
                Expires = System.DateTime.Now.AddDays(7)
            };
            _httpContextAccessor.HttpContext?.Response.Cookies.Append($"wishlist_{userId}", wishlistJSON, cookieOptions);
        }

        public void RemoveProductFromWishList(int productId)
        {
            var userId = GetUserId();
            var wishList = GetWishList(userId);

            var wishListItem = wishList.Find(item => item.Product.Id == productId);

            if (wishListItem != null)
            {
                wishList.Remove(wishListItem);
                SaveWishList(wishList, userId);
            }
        }

        public bool IsProductInWishList(int productId)
        {
            var userId = GetUserId();
            var wishList = GetWishList(userId);
            return wishList.Any(item => item.Product.Id == productId);
        }

        private string GetUserId()
        {
            return _userManager.GetUserId(_httpContextAccessor.HttpContext.User) ?? "guest";
        }
    }
}
