namespace TestProject1.DTO.BookStore;

public record DeleteBookResponseDTO(
    string UserId,
    string Isbn,
    string Message
    );