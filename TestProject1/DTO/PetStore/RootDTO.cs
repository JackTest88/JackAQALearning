namespace TestProject1.DTO.PetStore;

public record RootDTO(
    List<PetDTO> Data,
    PaginationDTO Pagination
    );