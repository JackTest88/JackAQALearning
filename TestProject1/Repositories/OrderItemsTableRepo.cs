using TestProject1.Interfaces.DBase1Interfaces;
using Dapper;
using Microsoft.Data.Sqlite;
using TestProject1.DTO.Database1DTOs;

namespace TestProject1.Repositories;

public class OrderItemsTableRepo: IOrderItemsRepository
{
    private readonly string _connection;

    public OrderItemsTableRepo(string connection)
    {
        _connection = connection;
    }

    public async Task<IEnumerable<OrderItemsTableDTO>> GetOrderItemsByOrderId (long orderId) 
    {
        using var db = new SqliteConnection(_connection);
        var itemsByOrderId = await db.QueryAsync<OrderItemsTableDTO>(
            "SELECT * from OrderItems WHERE OrderId = @orderId", new { orderId });
        return itemsByOrderId;
    }
    
    public async Task<IEnumerable<OrderItemsTableDTO>> GetOrderItemsByUserId(long userId)
    {
        using var db = new SqliteConnection(_connection);
        
        string sql = @"
            SELECT * 
            FROM OrderItems 
            WHERE OrderId IN (SELECT Id FROM Orders WHERE UserId = @userId)";

        var itemsByUserId = await db.QueryAsync<OrderItemsTableDTO>(sql, new { userId });
        return itemsByUserId;
    }
}