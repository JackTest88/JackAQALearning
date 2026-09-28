using Dapper;
using Microsoft.Data.Sqlite;
using TestProject1.DTO.Database1DTOs;
using TestProject1.Interfaces.DBase1Interfaces;
namespace TestProject1.Repositories;

public class AdressesTableRepo: IAddressesRepository
{
    private readonly string _connection;

    public AdressesTableRepo(string connection)
    {
        _connection = connection;
    }
    
    public async Task<IEnumerable<AddressesTableDTO>> GetAddressesByUserId(long userId)
    {
        using var db = new SqliteConnection(_connection);
        
        string sql = @"
        SELECT * 
        FROM Addresses 
        WHERE UserId = @userId";

        var addresses = await db.QueryAsync<AddressesTableDTO>(sql, new { userId });
        return addresses;
    }

}