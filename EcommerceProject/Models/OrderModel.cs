using System;
using System.Collections.Generic;

namespace EcommerceProject.Models
{
    public class OrderModel
    {
        public int Id { get; set; }
        public int UserId { get; set; } // Kullanıcı ID'si int olarak tanımlandı
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
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public List<OrderDetailModel> OrderDetails { get; set; }
    }
}
