namespace TestProject1.DependencyInjectionTask;

// тоже доделка к домашке 1
public class UserNotifier
{
    private readonly EmailSender sender;
    
    public UserNotifier(EmailSender sender)
    {
        this.sender = sender;
    }

    public void Notify(int userId)
    {
        sender.Send("user@mail.com", $"Hello, user {userId}!");
    }
}