using Data.Models;

public class GuestCheckoutViewModel
{
    public List<CartItemModel> CartItems { get; set; }
    public GuestOrderModel Order { get; set; }
    public string GuestId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Country { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string ZipCode { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string OrderNotes { get; set; }
    public string CouponCode { get; set; }
    public decimal TotalAmount { get; set; }
   
}
