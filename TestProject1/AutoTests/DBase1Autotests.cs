using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using TestProject1.DTO.Database1DTOs;
using TestProject1.Preconditions;
using TestProject1.Interfaces.DBase1Interfaces;
using TestProject1.Preconditions.DB1Precondition;
using TestProject1.Repositories;
using TestProject1.Helpers;

namespace TestProject1.AutoTests;

public class DBase1Autotests
{
    private readonly DataBasePreconditions p = new DataBasePreconditions();

    [Test]
    public async Task Test1_CheckUsersCount()
    {
        var repo = p.Provider.GetService<IUserRepository>();
        var users = await repo.GetAllUsers();
        users.Should().HaveCount(15);
    }

    [Test]
    public async Task Test2_CheckGetUserById()
    {
        var repo = p.Provider.GetService<IUserRepository>();
        var user = await repo.GetUserById(1);
        user.Should().NotBeNull();
    }
    
    [Test]
    public async Task Test3_GetUserByNameAndSurname()
    {
        var repo = p.Provider.GetService<IUserRepository>();
        var users = await repo.GetUserByNameAndSurname("Мария", "Павлова");
        users.Should().NotBeNull();
        users.firstName.Should().Be("Мария");
        users.lastName.Should().Be("Павлова");
    }

    [Test]
    public async Task Test4_CheckCategoriesCount()
    {
        var repo = p.Provider.GetService<ICategoryRepository>();
        var users = await repo.GetAllCategories();
        users.Should().HaveCount(6); 
    }

    [Test]
    public async Task Test5_GetProductById()
    {
        var repo = p.Provider.GetService<IProductRepository>();
        var product = await repo.GetProductById(1);
        product.id.Should().Be(1);
        product.name.Should().Be("iPhone 15");
        product.description.Should().Be("Смартфон Apple");
        product.price.Should().Be(79990);
        product.stock.Should().Be(15);
        product.categoryId.Should().Be(1);
    }

    [Test]
    public async Task Test6_CheckOrderByRandomUser()
    {
        var userRepo = p.Provider.GetService<IUserRepository>();
        var orderItemsRepo = p.Provider.GetService<IOrderItemsRepository>();
        
        // берем случайного юзера через рандомайзер
        var allUsers = (await userRepo.GetAllUsers()).ToList();
        var randomUser = RandomItAll.GetRandomItem(allUsers);
        
        // ищем все товары в заказе этого юзера
        var userOrderItems = (await orderItemsRepo.GetOrderItemsByUserId(randomUser.id)).ToList();
        userOrderItems.Should().NotBeEmpty();
    }
   
    
    [Test]
    public async Task Test7_CheckOrdersAndItemsForUserId()
    {
        var orderItemsRepo = p.Provider.GetService<IOrderItemsRepository>();
        var orderRepo = p.Provider.GetService<IOrderRepository>();

        long userIdHardCode = 1; // хардкодим юзера для детальной проверки айтемов в заказе

        // проверяем, что 2 заказа и номера строк для того, чтобы удостовериться
        var userOrders = (await orderRepo.GetOrdersForUser(userIdHardCode)).ToList();
        userOrders.Should().HaveCount(2);
        userOrders[0].id.Should().Be(1);
        userOrders[1].id.Should().Be(16);
        
        // проверяем, что в этих двух заказах 4 позиции и номера строк
        var userOrderItems = (await orderItemsRepo.GetOrderItemsByUserId(userIdHardCode)).ToList();
        userOrderItems.Should().HaveCount(4);
        userOrderItems[0].id.Should().Be(1);
        userOrderItems[0].productId.Should().Be(1);
        userOrderItems[0].quantity.Should().Be(1);
        userOrderItems[0].unitPrice.Should().Be(79990);
        
        userOrderItems[1].id.Should().Be(2);
        userOrderItems[1].productId.Should().Be(15);
        userOrderItems[1].quantity.Should().Be(1);
        userOrderItems[1].unitPrice.Should().Be(4990);
        
        userOrderItems[2].id.Should().Be(22);
        userOrderItems[2].productId.Should().Be(18);
        userOrderItems[2].quantity.Should().Be(1);
        userOrderItems[2].unitPrice.Should().Be(5990);
        
        userOrderItems[3].id.Should().Be(23);
        userOrderItems[3].productId.Should().Be(15);
        userOrderItems[3].quantity.Should().Be(2);
        userOrderItems[3].unitPrice.Should().Be(4990);
    }
}