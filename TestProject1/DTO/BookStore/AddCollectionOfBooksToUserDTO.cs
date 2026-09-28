using System.Text.Json.Serialization;

namespace TestProject1.DTO.BookStore;

public record AddCollectionOfBooksToUserDTO(
    [property: JsonPropertyName("userId")] string UserId,
    [property: JsonPropertyName("collectionOfIsbns")] List<CollectionOfIsbnsDTO> CollectionOfIsbns
);