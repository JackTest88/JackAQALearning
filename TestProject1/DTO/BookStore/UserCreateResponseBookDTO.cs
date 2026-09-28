namespace TestProject1.DTO.BookStore;

public record UserCreateResponseBookDTO(
    string Isbn,
    string Title,
    string SubTitle,
    string Author,
    string PublishDate,
    string Publisher,
    int Pages,
    string Description,
    string Website
    );