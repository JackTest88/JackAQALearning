using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Dapper;
using TestProject1.DTO.Database1DTOs;
using TestProject1.Interfaces.DBase1Interfaces;

namespace TestProject1.Repositories;

public class UsersTableRepo : IUserRepository
{
    private readonly string _connection;

    public UsersTableRepo(string connection)
    {
        _connection = connection;
    }

    public async Task<IEnumerable<UsersTableDTO>> GetAllUsers()
    {
        using var db = new SqliteConnection(_connection);
        var users = await db.QueryAsync<UsersTableDTO>("SELECT * from Users");
        return users;
    }

    public async Task<UsersTableDTO> GetUserById(int id)
    {
        using var db = new SqliteConnection(_connection);
        var userById = await db.QueryFirstOrDefaultAsync<UsersTableDTO>(
            "SELECT * from Users WHERE Id = @id", 
            new { id }
        );
        return userById;
    }
    
    public async Task<UsersTableDTO> GetUserByNameAndSurname(string firstName, string lastName)
    {
        using var db = new SqliteConnection(_connection);
        var userByName = await db.QueryFirstOrDefaultAsync<UsersTableDTO>(
            "SELECT * from Users" +
            " WHERE FirstName = @firstName AND LastName = @lastName", 
            new { firstName, lastName });
        return userByName;
    }
}