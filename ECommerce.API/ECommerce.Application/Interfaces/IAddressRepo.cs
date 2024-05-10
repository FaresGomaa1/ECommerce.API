
using ECommerce.API.DTOs.Address;

namespace ECommerce.API.ECommerce.Application.Interfaces
{
    public interface IAddressRepo
    {
        Task<AddressGetDTO> GetByIdAsync(int id);
        Task<IEnumerable<AddressGetDTO>> GetAllAsync();
        Task<AddressAddResultDTO> AddAsync(AddressAddEditDTO addressDto);
        //Task<AddressAddEditDTO> UpdateAsync(AddressAddEditDTO address);
        Task<bool> DeleteAsync(int id);
    }
}
