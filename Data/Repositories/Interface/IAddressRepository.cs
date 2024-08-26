using Data.Models;

namespace Data.Repositories.Interface
{
    public interface IAddressRepository
    {
        List<AddressModel> GetAddressesByUserId(int userId);
        AddressModel GetAddressById(int addressId);
        public void AddAddress(AddressModel address);
    }
}
