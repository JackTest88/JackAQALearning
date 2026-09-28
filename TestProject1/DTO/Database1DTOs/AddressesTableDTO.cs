namespace TestProject1.DTO.Database1DTOs;

public record AddressesTableDTO(
    long id,
    long userId,
    string city,
    string street,
    string house,
    string? apartment
    );