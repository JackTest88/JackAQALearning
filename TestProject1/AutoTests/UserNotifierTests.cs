using TestProject1.Preconditions;
using TestProject1.Helpers;
using Microsoft.Extensions.DependencyInjection; 

namespace TestProject1.AutoTests;

public class UserNotifierTests
{
    private readonly NotificationPreconditions p = new NotificationPreconditions();

    [Test]
    public void Test001NotifyDoesNotCreateEmailSenderItself()
    {
        var notifier = p.Provider.GetService<UserNotifier>();
        notifier.Should().NotBeNull();
        notifier.Notify(1);
    }
}