using Microsoft.Playwright;
namespace TestProject1.ForUI.Pages.SauceDemo;

public class CheckoutInfoPage
{
    private readonly IPage Page;

    private ILocator Title => Page.Locator(".title");
    private ILocator FirstNameTextBox => Page.GetByPlaceholder("First Name");
    private ILocator LastNameTextBox => Page.GetByPlaceholder("Last Name");
    private ILocator PostalCodeTextBox => Page.GetByPlaceholder("Zip/Postal Code");
    private ILocator ContinueButton => Page.GetByRole(AriaRole.Button, new() { Name = "Continue" });

    public CheckoutInfoPage(IPage page)
    {
        Page = page;
    }

    public async Task CheckPageOpenAsync()
    {
        await Assertions.Expect(Page).ToHaveURLAsync("https://www.saucedemo.com/checkout-step-one.html");
        await Assertions.Expect(Title).ToHaveTextAsync("Checkout: Your Information");
    }

    public async Task FillFormAsync(string firstName, string lastName, string postalCode)
    {
        await FirstNameTextBox.FillAsync(firstName);
        await LastNameTextBox.FillAsync(lastName);
        await PostalCodeTextBox.FillAsync(postalCode);
    }

    public async Task ClickContinueAsync()
    {
        await ContinueButton.ClickAsync();
    }
}
