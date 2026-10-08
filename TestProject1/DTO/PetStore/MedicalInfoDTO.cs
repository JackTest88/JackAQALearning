namespace TestProject1.DTO.PetStore;

public record MedicalInfoDTO(
    bool Vaccinated,
    bool SpayedNeutered,
    bool Microchipped,
    bool SpecialNeeds,
    string HealthNotes
    );