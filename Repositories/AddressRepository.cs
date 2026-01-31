using E_Commerce.Data;
using E_Commerce.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Repositories
{
    public class AddressRepository(ApplicationDbContext dbContext) : IAddressRepository
    {
        private readonly ApplicationDbContext _dbContext = dbContext;

        public async Task AddAsync(Address address)
        {
            await _dbContext.Addresses.AddAsync(address);
        }

        public async Task DeleteAsync(Address address)
        {
            var addressToDelete = await _dbContext.Addresses.FindAsync(address.Id)
                ?? throw new Exception("Address not found");

            _dbContext.Addresses.Remove(addressToDelete);
        }

        public async Task<IEnumerable<Address>> GetAllAsync()
        {
            return await _dbContext.Addresses.ToListAsync();
        }

        public async Task<Address?> GetByIdAsync(int id)
        {
            return await _dbContext.Addresses.FindAsync(id);
        }

        public async Task<Address?> GetByUserIdAsync(int userId)
        {
            return await _dbContext.Addresses
                .FirstOrDefaultAsync(a => a.UserId == userId);
        }

        public async Task UpdateAsync(Address address)
        {
            var addressToUpdate = await _dbContext.Addresses.FindAsync(address.Id)
                ?? throw new Exception("Address not found");

            addressToUpdate.RecipientName = address.RecipientName;
            addressToUpdate.PhoneNumber = address.PhoneNumber;
            addressToUpdate.StreetAddress = address.StreetAddress;
            addressToUpdate.Ward = address.Ward;
            addressToUpdate.District = address.District;
            addressToUpdate.City = address.City;
            addressToUpdate.Country = address.Country;
            addressToUpdate.PostalCode = address.PostalCode;
            addressToUpdate.IsDefault = address.IsDefault;
            addressToUpdate.UserId = address.UserId;
            addressToUpdate.UpdatedAt = DateTime.UtcNow;
        }
    }
}
