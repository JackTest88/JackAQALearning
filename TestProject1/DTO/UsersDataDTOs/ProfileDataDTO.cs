using System.Text.Json.Serialization;

namespace TestProject1.DTO.UsersDataDTOs;

public record ProfileDataDTO(
    [property: JsonPropertyName("fullName")]
    string FullName,
    [property: JsonPropertyName("age")]
    int Age,
    [property: JsonPropertyName("address")]
    ProfileAddressDTO Address,
    [property: JsonPropertyName("tags")]
    List<string> Tags
);