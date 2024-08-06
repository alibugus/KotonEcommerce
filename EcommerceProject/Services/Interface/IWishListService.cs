using EcommerceProject.Models;

namespace EcommerceProject.Services.Interface
{
    public interface IWishListService
    {
        public void AddProductToWishList(ProductModel product);
        public void RemoveProductFromWishList(int productId);
        public bool IsProductInWishList(int productId);
        public void ClearWishList();
        List<WishListModel> GetWishList();
    }
}
