using E_Commerce.Data;
using E_Commerce.Models;
using E_Commerce.Repositories;

namespace E_Commerce.Services
{
    public class AddressService(
        IAddressRepository addressRepository,
        ApplicationDbContext dbContext) : IAddressService
    {
        private readonly IAddressRepository _addressRepository = addressRepository;
        private readonly ApplicationDbContext _dbContext = dbContext;

        public async Task<Address?> GetByIdAsync(int id)
        {
            return await _addressRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Address>> GetByUserIdAsync(int userId)
        {
            return await _addressRepository.GetAllByUserIdAsync(userId);
        }

        public async Task<Address> AddAsync(int userId, Address address)
        {
            address.UserId = userId;
            address.CreatedAt = DateTime.UtcNow;
            address.UpdatedAt = DateTime.UtcNow;

            // If first address, make it default
            var existingAddresses = await _addressRepository.GetAllByUserIdAsync(userId);
            if (!existingAddresses.Any())
            {
                address.IsDefault = true;
            }

            await _addressRepository.AddAsync(address);
            await _dbContext.SaveChangesAsync();

            return address;
        }

        public async Task UpdateAsync(int userId, Address address)
        {
            var existingAddress = await _addressRepository.GetByIdAsync(address.Id)
                ?? throw new Exception("Address not found");

            if (existingAddress.UserId != userId)
            {
                throw new Exception("Unauthorized");
            }

            await _addressRepository.UpdateAsync(address);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int userId, int addressId)
        {
            var address = await _addressRepository.GetByIdAsync(addressId)
                ?? throw new Exception("Address not found");

            if (address.UserId != userId)
            {
                throw new Exception("Unauthorized");
            }

            await _addressRepository.DeleteAsync(address);
            await _dbContext.SaveChangesAsync();
        }

        public async Task SetDefaultAsync(int userId, int addressId)
        {
            var addresses = await _addressRepository.GetAllByUserIdAsync(userId);

            foreach (var addr in addresses)
            {
                addr.IsDefault = addr.Id == addressId;
                addr.UpdatedAt = DateTime.UtcNow;
            }

            await _dbContext.SaveChangesAsync();
        }

    }
}
