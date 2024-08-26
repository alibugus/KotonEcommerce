using Data.Database;
using Data.Models;
using Data.Services.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

public class GuestCouponService : IGuestCouponService
{
    private readonly ICartService _cartService;
    private readonly IProductService _productService;
    private readonly ICouponRepository _couponRepository; // Coupon repository eklendi
    private readonly ApplicationDbContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public GuestCouponService(ICartService cartService, IProductService productService, ICouponRepository couponRepository, ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
    {
        _cartService = cartService;
        _productService = productService;
        _couponRepository = couponRepository; // Coupon repository injected
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    // SetAmount method updated with correct async/await usage
    public async Task<CartItemViewModel> SetAmount(CartItemViewModel model)
    {
        var cart = _cartService.GetCart();

        // Assuming guest object is coming from some context, update this accordingly
        // var guest = HttpContext.User;
        var guestId = GetGuestId();  // Replace with real guest id logic

        var coupon = await GetCouponByCode(model.CouponCode) ?? null;

        decimal discountAmount = 0;
        decimal subtotal = cart.Sum(item => item.Product.Price * item.Quantity);
        decimal totalAmount = subtotal;

        if (coupon != null)
        {
            discountAmount = coupon.DiscountAmount;
            totalAmount = subtotal - discountAmount;
            return new CartItemViewModel
            {
                CouponCode = coupon.Code ?? null,
                CartItem = cart,
                DiscountAmount = discountAmount,
                Subtotal = subtotal,
                TotalAmount = totalAmount,
            };
        }

        else 
        { 
            return new CartItemViewModel
            {
                CouponCode = null,
                CartItem = cart,
                DiscountAmount = discountAmount,
                Subtotal = subtotal,
                TotalAmount = totalAmount,
            };
        }
        
    }
    public string GetGuestId()
    {
        if (_httpContextAccessor.HttpContext.Request.Cookies["GuestId"] == null)
        {
            var guestId = Guid.NewGuid().ToString();
            _httpContextAccessor.HttpContext.Response.Cookies.Append("GuestId", guestId, new CookieOptions { Expires = DateTime.Now.AddDays(7) });
            return guestId;
        }
        return _httpContextAccessor.HttpContext.Request.Cookies["GuestId"];
    }
    // Correct async implementation for GetCouponByCode method using repository
    public async Task<CouponModel> GetCouponByCode(string code)
    {
        return await _couponRepository.GetCouponByCodeAsync(code); // Repository'den kupon al
    }

    // Correct async implementation for ValidateCoupon method using repository
    public async Task<bool> ValidateCoupon(string code)
    {
        var coupon = await GetCouponByCode(code);
        return coupon != null && coupon.ExpiryDate >= DateTime.Now;
    }

    // Correct async implementation for ApplyCouponForGuest method using repository
    public async Task<CartItemViewModel> ApplyCouponForGuest(string guestId, CartItemViewModel model)
    {
        var coupon = await GetCouponByCode(model.CouponCode);

        if (coupon != null && await ValidateCoupon(model.CouponCode))
        {
            var guestCoupon = new GuestCouponModel
            {
                GuestId = guestId,
                CouponId = coupon.Id,
                AppliedDate = DateTime.Now
            };

            _context.GuestCoupons.Add(guestCoupon);
            await _context.SaveChangesAsync();
        }
        var model2 = new CartItemViewModel()
        {
            CouponCode = coupon.Code,
            DiscountAmount = coupon.DiscountAmount,
            Subtotal = model.Subtotal,
            TotalAmount = model.TotalAmount

        };
        return model2;
    }
    public async Task ActivateCoupon(string guestId, string couponCode)
    {
        var guestCoupon = await _context.GuestCoupons
            .FirstOrDefaultAsync(gc => gc.GuestId == guestId && gc.Coupon.Code == couponCode);

        if (guestCoupon != null)
        {
            guestCoupon.IsActiveCart = true;

            // Store the active coupon in cookies
            _httpContextAccessor.HttpContext.Response.Cookies.Append($"ActiveCoupon_{guestCoupon.Coupon.Code}", guestCoupon.Coupon.Code, new CookieOptions
            {
                Expires = DateTime.Now.AddDays(7) // Keep coupon active for 7 days
            });

            _context.GuestCoupons.Update(guestCoupon);
            await _context.SaveChangesAsync();
        }
    }
    public async Task MarkCouponAsUsedAsync(string guestId, string couponCode)
    {
        var guestCoupon = await _context.GuestCoupons
            .FirstOrDefaultAsync(gc => gc.GuestId == guestId && gc.Coupon.Code == couponCode);

        if (guestCoupon != null)
        {
            guestCoupon.IsUsed = true;

            // Update the coupon in the database
            _context.GuestCoupons.Update(guestCoupon);
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeactivateCoupon(string guestId, string couponCode)
    {
        

           

            // Remove the active coupon from cookies
            _httpContextAccessor.HttpContext.Response.Cookies.Delete($"ActiveCoupon_{couponCode}");

            
        
    }
    public List<string> GetActiveCouponsFromCart()
    {
        var activeCoupons = new List<string>();

        // Iterate through the request cookies to find active coupons
        foreach (var cookie in _httpContextAccessor.HttpContext.Request.Cookies)
        {
            if (cookie.Key.StartsWith("ActiveCoupon_"))
            {
                activeCoupons.Add(cookie.Value);
            }
        }

        return activeCoupons;
    }
    public async Task<CouponModel> GetActiveCoupon(string guestId)
    {
        var activeCouponsFromCookies = GetActiveCouponsFromCart();

        // Return null if no active coupons are found in the cookies
        if (activeCouponsFromCookies == null || !activeCouponsFromCookies.Any())
        {
            return null;
        }

        // Fetch the first active coupon from the database based on the cookies
        var activeCoupon = await _context.Coupons
            .FirstOrDefaultAsync(c => activeCouponsFromCookies.Contains(c.Code));

        return activeCoupon; // Return the first matching coupon or null if not found
    }
    public async Task<Dictionary<string, string>> ValidateCouponAsync(string guestId, string couponCode)
    {
        var errors = new Dictionary<string, string>();

        // Get active coupons for the guest
        var activeCoupons = GetActiveCouponsFromCart();
        if (activeCoupons.Any())
        {
            errors.Add(string.Empty, "You already have an active coupon. Please remove it before applying a new one.");
            return errors;
        }

        // Validate coupon code input
        if (string.IsNullOrEmpty(couponCode))
        {
            errors.Add(string.Empty, "Coupon code is required.");
            return errors;
        }

        // Fetch the coupon from the repository
        var coupon = await _couponRepository.GetCouponByCodeAsync(couponCode);
        if (coupon == null)
        {
            errors.Add(string.Empty, "Invalid coupon code.");
            return errors;
        }

        // Check if the coupon is expired
        if (coupon.ExpiryDate < DateTime.Now)
        {
            errors.Add(string.Empty, "This coupon has expired.");
            return errors;
        }

        // Check if the coupon has already been used
        var guestCoupon = await _context.GuestCoupons
            .FirstOrDefaultAsync(gc => gc.GuestId == guestId && gc.CouponId == coupon.Id);

        if (guestCoupon != null && guestCoupon.IsUsed)
        {
            errors.Add(string.Empty, "This coupon has already been used.");
            return errors;
        }

        // Check conflicting coupon codes (SUMMER20 and WELCOME10)
        var appliedCoupons = await _context.GuestCoupons
            .Where(gc => gc.GuestId == guestId)
            .Include(gc => gc.Coupon)
            .ToListAsync();

        if (couponCode == "SUMMER20")
        {
            errors.Add(string.Empty, "You cannot apply SUMMER20");
            return errors;
        }

      

        return errors;
    }


}
