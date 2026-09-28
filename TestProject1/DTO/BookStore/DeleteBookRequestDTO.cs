using System.Text.Json.Serialization;

namespace TestProject1.DTO.BookStore;

public record DeleteBookRequestDTO(
    [property: JsonPropertyName("isbn")] string Isbn,
    [property: JsonPropertyName("userId")] string UserId
);