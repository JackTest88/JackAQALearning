namespace TestProject1.DTO.Database1DTOs;

public record OrdersTableDTO(
    long id,
    long userId,
    string orderDate,
    string status,
    double totalPrice
    );