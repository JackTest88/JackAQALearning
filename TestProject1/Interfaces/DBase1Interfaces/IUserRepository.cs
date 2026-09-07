using TestProject1.DTO.Database1DTOs;

namespace TestProject1.Interfaces.DBase1Interfaces;

public interface IUserRepository
{
    Task<IEnumerable<UsersTableDTO>> GetAllUsers();
    Task<UsersTableDTO> GetUserById(int id);
    Task<UsersTableDTO> GetUserByNameAndSurname(string firstName, string lastName);
}