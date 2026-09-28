using Microsoft.Extensions.DependencyInjection;
using Refit;
using TestProject1.Interfaces.PetStoreInterfaces;
using TestProject1.DTO.PetStore;
using FluentAssertions;
using TestProject1.Helpers;

namespace TestProject1.AutoTests;

public class PetStoreTests
{
    private IPetApi api;
    [OneTimeSetUp]
    public void Setup()
    {
        var services = new ServiceCollection();

        var handler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = (msg, cert, chain, errors) => true
        };

        services.AddRefitClient<IPetApi>()
            .ConfigureHttpClient(c =>
            {
                c.BaseAddress = new Uri("https://petstoreapi.com/v1");
            })
            .ConfigurePrimaryHttpMessageHandler(() => handler);

        var provider = services.BuildServiceProvider();
        api = provider.GetRequiredService<IPetApi>();
    }

    [Test]
    public async Task GetAllPets() // проверяем, что 20 животных возвращается
    {
        var allPets = await api.GetAllPetsAsync();
        allPets.Should().NotBeNull();
        allPets.Data.Should().HaveCount(20);
    }

    [Test]
    public async Task GetAllPetsAndGetPetByRandomId() // проверяем, что рандомное возвращается
    {
        var allPets = await api.GetAllPetsAsync();
        var randomPet = RandomHelper.GetRandomItem(allPets.Data);
        var petById = await api.GetPetByIdAsync(randomPet.Id);
        petById.Should().NotBeNull();
        petById.Should().BeEquivalentTo(randomPet);
    }

    [Test]
    public async Task GetAllPetsWithFilterByAgeAndLimit10() //
    {
        var pets = await api.GetAllPetsByMinAgeAndLimit100Async(3, 10);
        var result = pets.Data;
        pets.Should().NotBeNull();
        pets.Data.Should().HaveCount(10);
        bool res = result.All(p =>p.AgeMonths >= 3);
        res.Should().BeFalse();
    }
}