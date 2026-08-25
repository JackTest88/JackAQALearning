using System.Runtime.InteropServices.ComTypes;
using System.Text.Json;
using FluentAssertions;
using FluentAssertions.Execution;
using TestProject1.DTO.UsersDataDTOs;
namespace TestProject1.AutoTests;


public class UsersDataJsonTests
{
    private ResponceDataDTO data;

    [OneTimeSetUp]
    public void Setup()
    {
        var path = Path.Combine(TestContext.CurrentContext.TestDirectory, "Resources", "UsersData.json");
        string json = File.ReadAllText(path);
        
        data = JsonSerializer.Deserialize<ResponceDataDTO>(json);
    }
    
    [Test]
    public void UsersDataJsonTest1_Has10Users() //Проверить, что количество юзеров из файла равно 10
    {
        data.Data.Should().HaveCount(10); 
    }
    [Test]
    public void UsersDataJsonTest2_FirstIsAliceJohnson() //Проверить, что первый юзер - Alice Johnson
    {
        data.Data.First().Profile.FullName.Should().Be("Alice Johnson");
    }
    
    [Test]
    public void UsersDataJsonTest3_IDsShouldBeUnique() //Проверить, что все Id уникальны
    {
        List<int> userIDs = new List<int>();
        foreach (var user  in data.Data )
        {
            userIDs.Add(user.Id);
        }
        userIDs.Should().OnlyHaveUniqueItems();
    }
    
    [Test]
    public void UsersDataJsonTest4_AtLeastOnePremium() //Проверить, что есть хотя бы один премиум-пользователь
    {
        data.Data.Should().Contain(users => users.Profile.Tags.Contains("premium"));
    }
    
    [Test]
    public void UsersDataJsonTest5_CityIsNotEmptyForAnyUser() //Проверить, что у всех юзеров поле город - не пустой
    {
        data.Data.Select(users => users.Profile.Address.City)
            .Should().NotBeEmpty();
    }
    
    [Test]
    public void UsersDataJsonTest6_AtLeastOneFromStockholm() //Проверить, что есть хотя бы один пользователь из Стокгольма
    {
        bool cityIsStockholm = data.Data.Any(users => users.Profile.Address.City.Equals("Stockholm"));
        cityIsStockholm.Should().BeTrue();
    }
    
    [Test]
    public void UsersDataJsonTest7_AgeFrom18To60() //Проверить, что возраст всех юзеров в диапазоне 18-60 лет
    {
        bool ageFrom18To60 = data.Data.All(users => users.Profile.Age > 18 && users.Profile.Age < 60);
        ageFrom18To60.Should().BeTrue();
    }
    
    [Test]
    public void UsersDataJsonTest8_AtLeastOneAdmin() //Проверить, что есть хотя бы один юзер с ролью admin
    {
        bool hasAdminRole = data.Data.Any(users => users.Roles.Contains("admin"));
        hasAdminRole.Should().BeTrue();
    }
    
    [Test]
    public void UsersDataJsonTest9_AllInSweden() //Проверить, что все юзеры (их координаты) находятся в диапазоне Швеции
    {
        bool allInSweden = data.Data.All(users => users.Profile.Address.Geo.Lat >= 55.3
                                                  && users.Profile.Address.Geo.Lat <= 69.1
                                                  && users.Profile.Address.Geo.Lat >= 11.0
                                                  && users.Profile.Address.Geo.Lng <= 24.2);
        allInSweden.Should().BeTrue();
    }
}