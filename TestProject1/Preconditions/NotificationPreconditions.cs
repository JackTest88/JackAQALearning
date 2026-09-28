using Microsoft.Extensions.DependencyInjection;
namespace TestProject1.Preconditions;

public class NotificationPreconditions
{
    public ServiceProvider Provider { get; }

    public NotificationPreconditions()
    {
        var services = new ServiceCollection();
        services.AddNotifications();
        Provider = services.BuildServiceProvider();
    }
}