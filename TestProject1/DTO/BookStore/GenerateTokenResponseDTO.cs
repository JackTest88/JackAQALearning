namespace TestProject1.DTO.BookStore;

public record GenerateTokenResponseDTO(
        string Token,
        string Expires,
        string Status,
        string Result
    );