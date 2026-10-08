using System.Text.Json.Serialization;

namespace TestProject1.DTO.SauceDemo;

public record SauceUserDTO(
    [property: JsonPropertyName("userName")]
    string UserName,
    [property: JsonPropertyName("password")]
    string Password
);
