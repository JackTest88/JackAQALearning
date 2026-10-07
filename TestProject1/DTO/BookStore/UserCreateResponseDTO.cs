namespace TestProject1.DTO.BookStore;

public record UserCreateResponseDTO(
    string UserId,
    string UserName,
    List<UserCreateResponseBookDTO> Books
    );