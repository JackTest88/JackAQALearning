namespace TestProject1.DTO.Database1DTOs;

public record UsersTableDTO(
    long id,
    string firstName,
    string lastName,
    string email,
    string phone,
    string createdAt
);
