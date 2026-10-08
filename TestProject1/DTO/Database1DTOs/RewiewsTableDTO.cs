namespace TestProject1.DTO.Database1DTOs;

public record RewiewsTableDTO(
    int Id,
    int UserId,
    int ProductId,
    int Rating,
    string? Comment,
    string CreatedAt
    );