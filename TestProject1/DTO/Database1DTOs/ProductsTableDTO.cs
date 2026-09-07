namespace TestProject1.DTO.Database1DTOs;

public record ProductsTableDTO(
    long id,
    string name,
    string? description,
    double price,
    long stock,
    long categoryId
    );