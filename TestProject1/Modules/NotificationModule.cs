using Microsoft.Extensions.DependencyInjection;
using TestProject1.Helpers;
namespace TestProject1.Modules;

//тоже доделка по первой домашке
public static class NotificationModule
{
    public static IServiceCollection AddNotifications(this IServiceCollection services)
    {
        services.AddScoped<EmailSender>();
        services.AddScoped<UserNotifier>();
        return services;
    }
}