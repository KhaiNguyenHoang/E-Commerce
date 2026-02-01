using E_Commerce.Models;

namespace E_Commerce.Services
{
    public interface IAddressService
    {
        Task<Address?> GetByIdAsync(int id);
        Task<IEnumerable<Address>> GetByUserIdAsync(int userId);
        Task<Address> AddAsync(int userId, Address address);
        Task UpdateAsync(int userId, Address address);
        Task DeleteAsync(int userId, int addressId);
        Task SetDefaultAsync(int userId, int addressId);
    }
}
