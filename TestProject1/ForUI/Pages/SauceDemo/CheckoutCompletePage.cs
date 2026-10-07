using Microsoft.Playwright;
namespace TestProject1.ForUI.Pages.SauceDemo;

public class CheckoutCompletePage
{
    private readonly IPage Page;

    private ILocator Title => Page.Locator(".title");
    private ILocator CompleteHeader => Page.Locator(".complete-header");

    public CheckoutCompletePage(IPage page)
    {
        Page = page;
    }

    public async Task CheckPageOpenAsync()
    {
        await Assertions.Expect(Page).ToHaveURLAsync("https://www.saucedemo.com/checkout-complete.html");
        await Assertions.Expect(Title).ToHaveTextAsync("Checkout: Complete!");
    }

    public async Task CheckThankYouMessageAsync()
    {
        await Assertions.Expect(CompleteHeader).ToHaveTextAsync("Thank you for your order!");
    }
}
