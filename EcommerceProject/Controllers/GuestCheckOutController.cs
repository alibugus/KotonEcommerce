using EcommerceProject.Models;
using EcommerceProject.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace EcommerceProject.Controllers
{
    public class GuestCheckoutController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IOrderService _orderService;
        private readonly IGuestCouponService _guestCouponService;

        public GuestCheckoutController(ICartService cartService, IOrderService orderService, IGuestCouponService guestCouponService)
        {
            _cartService = cartService;
            _orderService = orderService;
            _guestCouponService = guestCouponService;
        }

        public async Task<IActionResult> Index()
        {
            var cart = _cartService.GetCart();
            var guestId = _guestCouponService.GetGuestId();
            var activeCoupons = await _guestCouponService.GetActiveCoupon(guestId);

            var subtotal = cart.Sum(item => item.Product.Price * item.Quantity);
            var discount = 0;
            var totalamount = 0;
            if (activeCoupons != null)
            {
                var cartItemViewModel = new CartItemViewModel
                {
                    Subtotal = cart.Sum(item => item.Product.Price * item.Quantity),
                    DiscountAmount = activeCoupons.DiscountAmount,
                    TotalAmount = subtotal - activeCoupons.DiscountAmount,

                };
                var model = new GuestCheckoutViewModel
                {
                    TotalAmount = cart.Sum(item => item.Product.Price * item.Quantity)- activeCoupons.DiscountAmount,
                    CartItems = cart,
                    Order = new GuestOrderModel()
                };
                return View(model);
            }
            else
            {
                var model = new GuestCheckoutViewModel
                {
                    TotalAmount = subtotal,
                    CartItems = cart,
                    Order = new GuestOrderModel()
                };
                return View(model);


            }
            
        }

        [HttpPost]
        public async Task<IActionResult> PlaceOrder(GuestCheckoutViewModel model)
        {
            var cart = _cartService.GetCart();
            var GuestId = _guestCouponService.GetGuestId();
            var activeCoupon=_guestCouponService.GetActiveCoupon(GuestId);



                var order = new GuestOrderModel
                {
                    GuestId = GuestId,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Address = model.Address,
                    City = model.City,
                    State = model.State,
                    ZipCode = model.ZipCode,
                    Phone = model.Phone,
                    TotalPrice = model.TotalAmount,
                    Country = model.Country,
                    OrderNotes = model.OrderNotes,
                    OrderDate = DateTime.Now,
                    OrderDetails = cart.Select(item => new GuestOrderDetailModel
                    {
                        ProductId = item.Product.Id,
                        Quantity = item.Quantity,
                        Price = model.TotalAmount
                    }).ToList()
                };

                _orderService.GuestPlaceOrder(order, cart);

                var savedOrder = _orderService.GetGuestOrdersByGuestId(order.GuestId)
                                          .OrderByDescending(o => o.Id)
                                          .FirstOrDefault();
                decimal totalAmount = Convert.ToDecimal(TempData["TotalAmount"]);

                if (savedOrder != null)
                {
                    foreach (var cartItem in cart)
                    {
                        var orderDetail = new GuestOrderDetailModel
                        {
                            OrderId = savedOrder.Id,
                            ProductId = cartItem.Product.Id,
                            Quantity = cartItem.Quantity,
                            Price = totalAmount
                        };
                        _orderService.AddGuestOrderDetail(orderDetail);
                    }

                    // Clear the cart
                    _cartService.ClearCart();

                 }

                model.CartItems = _cartService.GetCart();
                await _guestCouponService.MarkCouponAsUsedAsync(GuestId, activeCoupon.Result.Code);
                await _guestCouponService.DeactivateCoupon(GuestId,activeCoupon.Result.Code);
                return RedirectToAction("Index", "Shop");
                
         
        }

        
    }
}
