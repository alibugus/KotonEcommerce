namespace EcommerceProject.Models
{
    public class GuestOrderDetailModel
    {
       
            public int Id { get; set; }
            public int OrderId { get; set; }
            public int ProductId { get; set; }
            public int Quantity { get; set; }
            public decimal Price { get; set; }
            public string SelectedSize { get; set; }

            public GuestOrderModel Order { get; set; }
        
    }
}
