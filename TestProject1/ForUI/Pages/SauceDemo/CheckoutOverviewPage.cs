using Microsoft.Playwright;
namespace TestProject1.ForUI.Pages.SauceDemo;

public class CheckoutOverviewPage
{
    private readonly IPage Page;

    private ILocator Title => Page.Locator(".title");
    private ILocator ItemNames => Page.Locator(".cart_item .inventory_item_name");
    private ILocator FinishButton => Page.GetByRole(AriaRole.Button, new() { Name = "Finish" });

    public CheckoutOverviewPage(IPage page)
    {
        Page = page;
    }

    public async Task CheckPageOpenAsync()
    {
        await Assertions.Expect(Page).ToHaveURLAsync("https://www.saucedemo.com/checkout-step-two.html");
        await Assertions.Expect(Title).ToHaveTextAsync("Checkout: Overview");
    }

    public async Task CheckItemsAsync(IEnumerable<string> expectedItems)
    {
        await Assertions.Expect(ItemNames).ToHaveTextAsync(expectedItems);
    }

    public async Task ClickFinishAsync()
    {
        await FinishButton.ClickAsync();
    }
}
