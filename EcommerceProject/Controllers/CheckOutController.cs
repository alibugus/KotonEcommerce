﻿using EcommerceProject.Models;
using EcommerceProject.Services.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceProject.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IOrderService _orderService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IAddressService _addressService;

        public CheckoutController(ICartService cartService, IOrderService orderService, UserManager<AppUser> userManager, IAddressService addressService)
        {
            _cartService = cartService;
            _orderService = orderService;
            _userManager = userManager;
            _addressService = addressService;
        }

        public async Task<IActionResult> Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "GuestCheckout");
            }

            var user = await _userManager.GetUserAsync(User);
            var cart = _cartService.GetCart();
            var addresses = _addressService.GetAddressesByUserId(user.Id);
            var model = new CheckOutViewModel
            {
                CartItems = cart,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                City = user.City,
                SavedAddresses = addresses
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> PlaceOrder(CheckOutViewModel model)
        {
            model.CartItems = _cartService.GetCart();
            var user = await _userManager.GetUserAsync(User);
            AddressModel selectedAddress;

            if (model.SelectedAddressId != 0)
            {
                selectedAddress = _addressService.GetAddressById(model.SelectedAddressId);
            }
            else
            {
                selectedAddress = new AddressModel
                {
                    UserId = user.Id,
                    Country = model.NewAddressCountry,
                    Address = model.NewAddressLine,
                    City = model.NewAddressCity,
                    State = model.NewAddressState,
                    ZipCode = model.NewAddressZipCode
                };
                if (string.IsNullOrEmpty(selectedAddress.Address))
                {
                    ModelState.AddModelError("", "Address cannot be empty.");
                    return View("Index", model);  // Return to the form with the error
                }
                _addressService.AddAddress(selectedAddress);
            }

            var order = new OrderModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Country = selectedAddress.Country,
                Address = selectedAddress.Address,
                City = selectedAddress.City,
                State = selectedAddress.State,
                ZipCode = selectedAddress.ZipCode,
                Phone = user.PhoneNumber,
                Email = user.Email,
                OrderNotes = model.OrderNotes,
                UserId = user.Id
            };

            _orderService.PlaceOrder(order, model.CartItems);

            var savedOrder = _orderService.GetOrdersByUserId(order.UserId)
                                          .OrderByDescending(o => o.Id)
                                          .FirstOrDefault();

            if (savedOrder != null)
            {
                foreach (var cartItem in model.CartItems)
                {
                    var orderDetail = new OrderDetailModel
                    {
                        OrderId = savedOrder.Id,
                        ProductId = cartItem.Product.Id,
                        Quantity = cartItem.Quantity,
                        Price = cartItem.Product.Price
                    };
                    _orderService.AddOrderDetail(orderDetail);
                }

                _cartService.ClearCart();
            }

            return RedirectToAction("Index", "Order");
        }

    }
}
