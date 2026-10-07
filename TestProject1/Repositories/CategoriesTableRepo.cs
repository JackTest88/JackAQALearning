using Dapper;
using Microsoft.Data.Sqlite;
using TestProject1.DTO.Database1DTOs;
using TestProject1.Interfaces.DBase1Interfaces;

namespace TestProject1.Repositories;

public class CategoriesTableRepo: ICategoryRepository
{
    private readonly string _connection;

    public CategoriesTableRepo(string connection)
    {
        _connection = connection;
    }

    public async Task<IEnumerable<CategoriesTableDTO>> GetAllCategories()
    {
        using var db = new SqliteConnection(_connection);
        var categories = await db.QueryAsync<CategoriesTableDTO>(
            "SELECT * FROM categories");
        return categories;
    }
}