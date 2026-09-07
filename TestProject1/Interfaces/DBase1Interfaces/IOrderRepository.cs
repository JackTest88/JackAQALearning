using TestProject1.DTO.Database1DTOs;

namespace TestProject1.Interfaces.DBase1Interfaces;

public interface IOrderRepository
{
    Task<OrdersTableDTO?> GetOrderById(long id);
    Task<IEnumerable<OrdersTableDTO>> GetAllOrders();
    Task<IEnumerable<OrdersTableDTO>> GetOrdersForUser (long userId);
}