using EcommerceProject.Models;
using EcommerceProject.Repositories.Interface;
using EcommerceProject.Services.Interface;

namespace EcommerceProject.Services
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _addressRepository;

        public AddressService(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }

        public List<AddressModel> GetAddressesByUserId(int userId)
        {
            return _addressRepository.GetAddressesByUserId(userId);
        }

        public AddressModel GetAddressById(int addressId)
        {
            return _addressRepository.GetAddressById(addressId);
        }

        public void AddAddress(AddressModel address)
        {
            _addressRepository.AddAddress(address);
        }
    }
}
