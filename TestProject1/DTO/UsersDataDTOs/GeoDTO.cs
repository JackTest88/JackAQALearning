using System.Text.Json.Serialization;

namespace TestProject1.DTO.UsersDataDTOs;

public record GeoDTO(
    [property: JsonPropertyName("lat")]
    double Lat,
    [property: JsonPropertyName("lng")]
    double Lng
);