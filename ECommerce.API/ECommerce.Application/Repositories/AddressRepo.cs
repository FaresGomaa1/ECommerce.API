using AutoMapper;
using ECommerce.API.DTOs.Address;
using ECommerce.API.ECommerce.Application.Interfaces;
using ECommerce.API.ECommerce.Domain.Model;
using ECommerce.API.ECommerce.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.API.ECommerce.Application.Repositories
{
    public class AddressRepo : IAddressRepo
    {
        private readonly IMapper _mapper;
        private readonly ECommerceDbContext _dbContext;

        public AddressRepo(ECommerceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<AddressAddResultDTO> AddAsync(AddressAddEditDTO addressDto)
        {
            if (addressDto == null)
                throw new ArgumentNullException(nameof(addressDto));

            var addressEntity = _mapper.Map<Address>(addressDto);

            await _dbContext.Addresses.AddAsync(addressEntity);
            await _dbContext.SaveChangesAsync();

            return new AddressAddResultDTO
            {
                AddressId = addressEntity.Id,
                Address = _mapper.Map<AddressAddEditDTO>(addressEntity)
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var addressEntity = await _dbContext.Addresses.FindAsync(id);
            if (addressEntity == null)
                return false;

            _dbContext.Addresses.Remove(addressEntity);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<AddressGetDTO>> GetAllAsync()
        {
            var addresses = await _dbContext.Addresses.ToListAsync();
            return _mapper.Map<IEnumerable<AddressGetDTO>>(addresses);
        }

        public async Task<AddressGetDTO> GetByIdAsync(int id)
        {
            var address = await _dbContext.Addresses.FindAsync(id);
            return _mapper.Map<AddressGetDTO>(address);
        }

        public async Task<AddressAddEditDTO> UpdateAsync(AddressAddEditDTO addressDto)
        {
            if (addressDto == null)
                throw new ArgumentNullException(nameof(addressDto));

            var existingAddress = await _dbContext.Addresses.FindAsync(addressDto.Id);
            if (existingAddress == null)
                throw new ArgumentException($"Address with ID {addressDto.Id} not found");

            _mapper.Map(addressDto, existingAddress);

            _dbContext.Entry(existingAddress).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<AddressAddEditDTO>(existingAddress);
        }
    }
}