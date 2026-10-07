using Microsoft.Playwright;
namespace TestProject1.ForUI.Pages.SauceDemo;

public class LoginPage
{
    private readonly IPage Page;

    private ILocator UserNameTextBox => Page.GetByPlaceholder("Username");
    private ILocator PasswordTextBox => Page.GetByPlaceholder("Password");
    private ILocator LoginButton => Page.GetByRole(AriaRole.Button, new() { Name = "Login" });

    public LoginPage(IPage page)
    {
        Page = page;
    }

    public async Task OpenLoginPageAsync()
    {
        await Page.GotoAsync("https://www.saucedemo.com");
    }

    public async Task LoginAsync(string userName, string password)
    {
        await UserNameTextBox.FillAsync(userName);
        await PasswordTextBox.FillAsync(password);
        await LoginButton.ClickAsync();
    }
}
