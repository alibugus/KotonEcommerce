using EcommerceProject.Models;

namespace EcommerceProject.Repositories.Interface
{
    public interface IAddressRepository
    {
        List<AddressModel> GetAddressesByUserId(int userId);
        AddressModel GetAddressById(int addressId);
        public void AddAddress(AddressModel address);
    }
}
