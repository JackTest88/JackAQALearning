using TestProject1.DTO.Database1DTOs;
namespace TestProject1.Interfaces.DBase1Interfaces;

public interface IAddressesRepository
{
    Task<IEnumerable<AddressesTableDTO>> GetAddressesByUserId(long userId);
}