namespace TestProject1.DTO.PetStore;

public record PaginationDTO(
    int Page,
    int Limit,
    int TotalItems,
    int TotalPages
    );