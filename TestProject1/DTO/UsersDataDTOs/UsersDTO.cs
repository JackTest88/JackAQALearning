using System.Text.Json.Serialization;

namespace TestProject1.DTO.UsersDataDTOs;

public record UsersDTO(
    [property: JsonPropertyName("id")] 
    int Id,
    [property: JsonPropertyName("username")]
    string Username,
    [property: JsonPropertyName("profile")]
    ProfileDataDTO Profile,
    [property: JsonPropertyName("roles")]
    List<string> Roles
    );