using Microsoft.Extensions.DependencyInjection;
using TestProject1.Interfaces.BookStoreInterfaces;
using TestProject1.DTO.BookStore;
using Refit;
using FluentAssertions;
using TestProject1.Helpers;

namespace TestProject1.AutoTests;

// https://demoqa.com/swagger#/ <- сюда ходим

public class BookStoreTests
{
    private IBookStoreApi api;

    [OneTimeSetUp]
    public void Setup()
    {
        var services = new ServiceCollection();

        services
            .AddRefitClient<IBookStoreApi>()
            .ConfigureHttpClient(c => { c.BaseAddress = new Uri("https://demoqa.com"); });

        var provider = services.BuildServiceProvider();
        api = provider.GetRequiredService<IBookStoreApi>();
    }
    
    [Test]     // этот тест больше не будет работать, юзер уже создан
    public async Task Test1_CreateNewUser() //создание юзера
    {
        var credentials = new UserCreateRequestDTO("JackStrong", "StrongPass123!");
        var result = await api.CreateUserAsync(credentials); 
        result.Should().NotBeNull();
    }
    
    [Test]
    public async Task Test2_GetUserToken() // получение токена
    {
        var credentials = new UserCreateRequestDTO("JackStrong", "StrongPass123!");
        var result = await api.GenerateTokenAsync(credentials);
        result.Token.Should().NotBeNullOrEmpty();
    }

    [Test]
    public async Task Test3_GetUserId() // получение пользователя
    {
        var credentials = new UserCreateRequestDTO("JackStrong", "StrongPass123!");
        var result = await api.GetUserIdAsync(credentials);
        result.UserId.Should().NotBeNullOrEmpty();
    }

    [Test]
    public async Task Test4_GetBookListAsync() // приходит 8 книг
    {
        var result = await api.GetBookListAsync();
        result.Should().NotBeNull();
        result.Books.Should().NotBeNullOrEmpty();
        result.Books.Should().HaveCount(8);
    }

    [Test]
    public async Task Test5_GetBookByIsbnAsync() // поиск по id
    {
        var result = await api.GetBookByIsbnAsync("9781449337711");
        result.Should().NotBeNull();
    }

    [Test]
    public async Task Test6_AddBookToUserAsync() // добавление книги юзеру
    {
        var token = await GetTokenAsync();
        var userId = await GetUsersIdAsync();
        var listOfBooks = await api.GetBookListAsync();
        var rndIsbn = RandomHelper.GetRandomItem(listOfBooks.Books).Isbn;

        var request = new AddCollectionOfBooksToUserDTO
        (
            userId,
            new List<CollectionOfIsbnsDTO> { new CollectionOfIsbnsDTO(rndIsbn) }
        );
        
        // тест начинает падать по мере прохождения тестов, поэтому переписал с удалением книги после проверки
        try
        {
            // добавляем книгу и чекаем
            var response = await api.AddBookToUserAsync(request, token);
            response.Should().NotBeNull();
        }
        finally
        {   
            // удаляем книгу
            var deleteRequest = new DeleteBookRequestDTO(rndIsbn, userId);
            await api.DeleteBookFromUserAsync(deleteRequest, token);
        }
    }

    [Test]
    public async Task Test7_DeleteBookByIsbn() // удаление книги у юзер
    {
        var token = await GetTokenAsync();
        var userId = await GetUsersIdAsync();
        var isbn = "9781593277574";

        var addRequest = new AddCollectionOfBooksToUserDTO
        (
            userId,
            new List<CollectionOfIsbnsDTO> { new CollectionOfIsbnsDTO(isbn) }
        );
        await api.AddBookToUserAsync(addRequest, token);

        var deleteRequest = new DeleteBookRequestDTO(isbn, userId);
        var response = await api.DeleteBookFromUserAsync(deleteRequest, token);
        
        response.IsSuccessStatusCode.Should().BeTrue();
    }

    [Test]
    public async Task Test8_SendInvalidRequest() //без авторизации
    {
        var listOfBooks = await api.GetBookListAsync();
        var rndIsbn = RandomHelper.GetRandomItem(listOfBooks.Books).Isbn;
        var userId = await GetUsersIdAsync();

        var request = new AddCollectionOfBooksToUserDTO
        (
            userId,
            new List<CollectionOfIsbnsDTO> { new CollectionOfIsbnsDTO(rndIsbn) }
        );

        Func<Task> act = async () => await api.AddBookToUserAsync(request, token: null);
        act.Should()
            .ThrowAsync<ApiException>(); 
    }

    [Test]
    public async Task Test9_AddBookWithInvalidIsbn() //несуществующий исбн
    {
        var token = await GetTokenAsync();
        var userId = await GetUsersIdAsync();
        
        var request = new AddCollectionOfBooksToUserDTO
        (
            userId,
            new List<CollectionOfIsbnsDTO> { new CollectionOfIsbnsDTO("000InvalidIsbn") }
        );

        Func<Task> act = async () => await api.AddBookToUserAsync(request, token);
        act.Should().ThrowAsync<ApiException>();
    }

    
    private async Task<string> GetTokenAsync()
    {
        var credentials = new UserCreateRequestDTO("JackStrong", "StrongPass123!");
        var token = await api.GenerateTokenAsync(credentials);
        var result = $"Bearer {token.Token}";
        return result;
    }

    private async Task<string> GetUsersIdAsync()
    {
        var credentials = new UserCreateRequestDTO("JackStrong", "StrongPass123!");
        var result = await api.GetUserIdAsync(credentials);
        return result.UserId;
    }

}