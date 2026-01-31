using E_Commerce.Models;

namespace E_Commerce.Repositories
{
    public interface IAddressRepository
    {
        Task<Address?> GetByIdAsync(int id);
        Task<IEnumerable<Address>> GetAllAsync();
        Task<Address?> GetByUserIdAsync(int userId);
        Task AddAsync(Address address);
        Task UpdateAsync(Address address);
        Task DeleteAsync(Address address);
    }
}
