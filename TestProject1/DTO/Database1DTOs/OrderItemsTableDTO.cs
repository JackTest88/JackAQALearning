namespace TestProject1.DTO.Database1DTOs;

public record OrderItemsTableDTO(
    long id,
    long orderId,
    long productId,
    long quantity,
    double unitPrice
    );