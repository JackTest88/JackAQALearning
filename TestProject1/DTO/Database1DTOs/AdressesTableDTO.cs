namespace TestProject1.DTO.Database1DTOs;

public record AdressesTableDTO(
    int Id,
    int UserId,
    string City,
    string Street,
    string House,
    string? Apartment
    );