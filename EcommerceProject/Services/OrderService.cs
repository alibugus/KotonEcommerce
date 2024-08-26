using EcommerceProject.Models;
using EcommerceProject.Repositories.Interface;
using EcommerceProject.Services.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace EcommerceProject.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IProductRepository _productRepository;
        private readonly IProductSizeRepository _productSizeRepository;
        private readonly ICartService _cartService;
        private readonly HttpClient _httpClient;

        public OrderService(IOrderRepository orderRepository, IOrderDetailRepository orderDetailRepository, IProductRepository productRepository, HttpClient httpClient, IProductSizeRepository productSizeRepository, ICartService cartService)
        {
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
            _productRepository = productRepository;
            _httpClient = httpClient; // HttpClient'ı başlatıyoruz
            _productSizeRepository = productSizeRepository;
            _cartService = cartService;
        }

        public IEnumerable<ProductModel> GetOrderProducts(int orderId)
        {
            var orderDetails = _orderDetailRepository.GetOrderDetailsByOrderId(orderId);
            var productIds = orderDetails.Select(od => od.ProductId).ToList();
            var products = _productRepository.GetProductsByIds(productIds);
            return products;
        }

        public void PlaceOrder(OrderModel order, List<CartItemModel> cartItems)
        {
            // Siparişi kaydet
            _orderRepository.AddOrder(order);

            // Kaydedilen sipariş ID'sini al
            var savedOrder = _orderRepository.GetOrdersByUserId(order.UserId)
                                  .OrderByDescending(o => o.Id)
                                  .FirstOrDefault();

            if (savedOrder != null)
            {
                // Sipariş detaylarını kaydet ve stok miktarını güncelle
                foreach (var cartItem in cartItems)
                {
                    var orderDetail = new OrderDetailModel
                    {
                        OrderId = savedOrder.Id,
                        ProductId = cartItem.Product.Id,
                        Quantity = cartItem.Quantity,
                        Price = cartItem.Product.Price,
                        SelectedSize = cartItem.SelectedSize
                    };
                    _orderDetailRepository.AddOrderDetail(orderDetail);

                    // Ürünün stok miktarını güncelle
                    var productSize = _productSizeRepository.GetProductSizeByProductIdAndSize(cartItem.Product.Id, cartItem.SelectedSize);
                    if (productSize != null)
                    {
                        productSize.StockQuantity -= cartItem.Quantity;
                        _productSizeRepository.UpdateProductSize(productSize);

                        // Update the total stock quantity in the ProductModel
                        var product = _productRepository.GetProductById(cartItem.Product.Id);
                        product.StockQuantity = _productRepository.GetTotalStockQuantity(cartItem.Product.Id);
                        _productRepository.UpdateProduct(product);
                    }
                }
                _cartService.ClearCart();
                // HTTP isteği ile stok güncellemelerini diğer projeye gönder
                //SendHttpToUpdateQuantity(savedOrder.Id).GetAwaiter().GetResult();
            }
        }

    

        public void AddOrderDetail(OrderDetailModel orderDetail)
        {
            _orderDetailRepository.AddOrderDetail(orderDetail);
        }

        public void AddGuestOrderDetail(GuestOrderDetailModel guestOrderDetail)
        {
            _orderDetailRepository.AddGuestOrderDetail(guestOrderDetail);
        }

        public IEnumerable<OrderModel> GetAllOrders()
        {
            return _orderRepository.GetAllOrders();
        }

        public OrderModel GetOrderById(int orderId)
        {
            return _orderRepository.GetOrderById(orderId);
        }

        public IEnumerable<OrderModel> GetOrdersByUserId(int userId)
        {
            return _orderRepository.GetOrdersByUserId(userId);
        }

        public void GuestPlaceOrder(GuestOrderModel order, List<CartItemModel> cartItems)
        {
            // Save the guest order
            _orderRepository.AddGuestOrder(order);

            // Get the saved guest order ID
            var savedOrder = _orderRepository.GetGuestOrdersByGuestId(order.GuestId)
                                  .OrderByDescending(o => o.Id)
                                  .FirstOrDefault();

            if (savedOrder != null)
            {
                // Save the order details and update stock quantity
                foreach (var cartItem in cartItems)
                {
                    var orderDetail = new GuestOrderDetailModel
                    {
                        OrderId = savedOrder.Id,
                        ProductId = cartItem.Product.Id,
                        Quantity = cartItem.Quantity,
                        Price = cartItem.Product.Price,
                        SelectedSize = cartItem.SelectedSize // Assuming CartItemModel has a SelectedSize property
                    };
                    _orderDetailRepository.AddGuestOrderDetail(orderDetail);

                    // Update stock quantity for the specific size
                    var productSize = _productSizeRepository.GetProductSizeByProductIdAndSize(cartItem.Product.Id, cartItem.SelectedSize);
                    if (productSize != null)
                    {
                        productSize.StockQuantity -= cartItem.Quantity;
                        _productSizeRepository.UpdateProductSize(productSize);

                        // Update the total stock quantity in the ProductModel
                        var product = _productRepository.GetProductById(cartItem.Product.Id);
                        product.StockQuantity = _productRepository.GetTotalStockQuantity(cartItem.Product.Id);
                        _productRepository.UpdateProduct(product);
                    }
                }
                _cartService.ClearCart();
                // Send HTTP request to update quantity in the other project
               // SendHttpToUpdateQuantity(savedOrder.Id).GetAwaiter().GetResult();
            }
        }
        public async Task SendHttpToUpdateQuantity(int orderId)
        {
            var orderDetails = _orderDetailRepository.GetOrderDetailsByOrderId(orderId);

            var updateStockData = orderDetails.Select(od =>
            {
                var product = _productRepository.GetProductById(od.ProductId);
                return new
                {
                    ProductId = od.ProductId,
                    StockQuantity = product.StockQuantity // Product tablosundaki güncel stok miktarı
                };
            }).ToList();

            var content = new StringContent(JsonSerializer.Serialize(updateStockData), System.Text.Encoding.UTF8, "application/json");

            // Hangfire API URL'ini belirtin
            var response = await _httpClient.PostAsync("https://localhost:7195/api/Values/update", content);

            if (!response.IsSuccessStatusCode)
            {
                // Hata durumunu ele alın
                throw new HttpRequestException($"Failed to update stock in Hangfire project. Status code: {response.StatusCode}");
            }
        }

        public IEnumerable<GuestOrderModel> GetAllGuestOrders()
        {
            return _orderRepository.GetAllGuestOrders();
        }

        public IEnumerable<GuestOrderModel> GetGuestOrdersByGuestId(string GuestId)
        {
            return _orderRepository.GetGuestOrdersByGuestId(GuestId);
        }
    }
}
