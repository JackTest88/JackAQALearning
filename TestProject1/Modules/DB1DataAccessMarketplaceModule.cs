using Microsoft.Extensions.DependencyInjection;
using TestProject1.Interfaces.DBase1Interfaces;
using TestProject1.Repositories;

namespace TestProject1.Modules;

public static class DataAccessMarketplaceModule
{
    public static IServiceCollection AddDataAccessMarketplace(this IServiceCollection services, string connectionString)
    {
        services.AddScoped<IUserRepository>(p => new UsersTableRepo(connectionString));
        services.AddScoped<ICategoryRepository>(p => new CategoriesTableRepo(connectionString));
        services.AddScoped<IProductRepository>(p => new ProductsTableRepo(connectionString));
        services.AddScoped<IOrderRepository>(p => new OrdersTableRepo(connectionString));
        services.AddScoped<IOrderItemsRepository>(p => new OrderItemsTableRepo(connectionString));
        services.AddScoped<IAddressesRepository>(p => new AdressesTableRepo(connectionString));
        
        return services;
    }
}