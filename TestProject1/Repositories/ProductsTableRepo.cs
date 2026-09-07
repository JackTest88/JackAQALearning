using Dapper;
using Microsoft.Data.Sqlite;
using TestProject1.DTO.Database1DTOs;
using TestProject1.Interfaces.DBase1Interfaces;

namespace TestProject1.Repositories;

public class ProductsTableRepo: IProductRepository
{
    private readonly string _connection;

    public ProductsTableRepo(string connection)
    {
        _connection = connection;
    }

    public async Task<ProductsTableDTO> GetProductById (int id)
    {
        using var db = new SqliteConnection(_connection);
        var productById = await db.QueryFirstOrDefaultAsync<ProductsTableDTO>(
            "SELECT * from Products WHERE Id = @id", 
            new { id }
        );
        return productById;
    }
}