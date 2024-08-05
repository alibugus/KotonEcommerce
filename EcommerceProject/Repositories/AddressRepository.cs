using EcommerceProject.Database;
using EcommerceProject.Models;
using EcommerceProject.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
namespace EcommerceProject.Repositories

{
    public class AddressRepository : IAddressRepository
    {
        private readonly ApplicationDbContext _context;

        public AddressRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<AddressModel> GetAddressesByUserId(int userId)
        {
            return _context.Addresses.Where(a => a.UserId == userId).ToList();
        }

        public AddressModel GetAddressById(int addressId)
        {
            return _context.Addresses.Find(addressId);
        }
        public void AddAddress(AddressModel address)
        {
            _context.Addresses.Add(address);
            _context.SaveChanges();
        }
    }
}
