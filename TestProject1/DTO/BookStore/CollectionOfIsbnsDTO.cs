using System.Text.Json.Serialization;

namespace TestProject1.DTO.BookStore;

public record CollectionOfIsbnsDTO(
    [property: JsonPropertyName("isbn")] string Isbn
);