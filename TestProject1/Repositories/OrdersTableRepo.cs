using Dapper;
using Microsoft.Data.Sqlite;
using TestProject1.DTO.Database1DTOs;
using TestProject1.Interfaces.DBase1Interfaces;

namespace TestProject1.Repositories;

public class OrdersTableRepo: IOrderRepository
{
    private readonly string _connection;

    public OrdersTableRepo(string connection)
    {
        _connection = connection;
    }

    public async Task<OrdersTableDTO?> GetOrderById(long id)
    {
        using var db = new SqliteConnection(_connection);
        var order = await db.QueryFirstOrDefaultAsync<OrdersTableDTO>(
            sql: "SELECT * from Orders WHERE Id = @id",
            param: new { id }
        );
        return order;
    }
    
    public async Task<IEnumerable<OrdersTableDTO>> GetAllOrders()
    {
        using var db = new SqliteConnection(_connection);
        var orders = await db.QueryAsync<OrdersTableDTO>(
            "SELECT * FROM orders");
        return orders;
    }

    public async Task<IEnumerable<OrdersTableDTO>> GetOrdersForUser(long userId)
    {
        using var db = new SqliteConnection(_connection);
        var ordersForUser = await db.QueryAsync<OrdersTableDTO>(
            sql: "SELECT * from Orders WHERE UserId = @userId",
            param: new { userId });
        return ordersForUser;
    }
}