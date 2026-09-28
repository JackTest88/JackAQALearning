using System.Text.Json.Serialization;

namespace TestProject1.DTO.UsersDataDTOs;

public record ProfileAddressDTO(
    [property: JsonPropertyName("street")]
    string Street,
    [property: JsonPropertyName("city")]
    string City,
    [property: JsonPropertyName("geo")]
    GeoDTO Geo
);