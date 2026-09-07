using TestProject1.DTO.Database1DTOs;

namespace TestProject1.Interfaces.DBase1Interfaces;

public interface IOrderItemsRepository
{
    Task<IEnumerable<OrderItemsTableDTO>> GetOrderItemsByOrderId(long orderId);
    
    Task<IEnumerable<OrderItemsTableDTO>> GetOrderItemsByUserId(long userId);
}