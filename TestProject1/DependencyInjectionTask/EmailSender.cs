namespace TestProject1.DependencyInjectionTask;

// это для первой домашки доделка
public class EmailSender
{
    public void Send(string to, string text)
    {
        Console.WriteLine($"Sending mail to {to}: {text}");
    }
}