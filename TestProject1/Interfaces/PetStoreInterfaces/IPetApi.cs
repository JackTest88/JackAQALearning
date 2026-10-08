using Refit;
using TestProject1.DTO.PetStore;

namespace TestProject1.Interfaces.PetStoreInterfaces;

public interface IPetApi
{
    [Get("/pets")]
    Task<RootDTO> GetAllPetsAsync();

    [Get("/pets")]
    Task<RootDTO> GetAllPetsByMinAgeAndLimit100Async([Query] int minAge, [Query] int limit);

    [Get("/pets/{id}")]
    Task<PetDTO> GetPetByIdAsync(string id);
}