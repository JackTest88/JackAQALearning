using Microsoft.Playwright;
namespace TestProject1.ForUI.Pages.SauceDemo;

public class CartPage
{
    private readonly IPage Page;

    private ILocator Title => Page.Locator(".title");
    private ILocator ItemNames => Page.Locator(".cart_item .inventory_item_name");
    private ILocator CheckoutButton => Page.GetByRole(AriaRole.Button, new() { Name = "Checkout" });

    public CartPage(IPage page)
    {
        Page = page;
    }

    public async Task CheckPageOpenAsync()
    {
        await Assertions.Expect(Page).ToHaveURLAsync("https://www.saucedemo.com/cart.html");
        await Assertions.Expect(Title).ToHaveTextAsync("Your Cart");
    }

    // Проверяет полный список названий в корзине (с авто-ожиданием)
    public async Task CheckItemsInCartAsync(IEnumerable<string> expectedItems)
    {
        await Assertions.Expect(ItemNames).ToHaveTextAsync(expectedItems);
    }

    public async Task ClickCheckoutAsync()
    {
        await CheckoutButton.ClickAsync();
    }
}
