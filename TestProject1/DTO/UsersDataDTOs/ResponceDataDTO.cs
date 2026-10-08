using System.Text.Json.Serialization;

namespace TestProject1.DTO.UsersDataDTOs;

public record ResponceDataDTO (
    [property: JsonPropertyName("data")]
    List<UsersDTO> Data
    );