using Microsoft.Playwright;
namespace TestProject1.ForUI.Pages.SauceDemo;

public class ProductsPage
{
    private readonly IPage Page;

    private ILocator Title => Page.Locator(".title");
    private ILocator CartLink => Page.Locator(".shopping_cart_link");
    private ILocator CartBadge => Page.Locator(".shopping_cart_badge");

    public ProductsPage(IPage page)
    {
        Page = page;
    }

    public async Task CheckPageOpenAsync()
    {
        await Assertions.Expect(Page).ToHaveURLAsync("https://www.saucedemo.com/inventory.html");
        await Assertions.Expect(Title).ToHaveTextAsync("Products");
    }

    // Находим карточку товара по названию и жмём Add to cart внутри неё
    public async Task AddToCartAsync(string itemName)
    {
        var item = Page.Locator(".inventory_item")
            .Filter(new() { Has = Page.GetByText(itemName, new() { Exact = true }) });

        await item.GetByRole(AriaRole.Button, new() { Name = "Add to cart" }).ClickAsync();
    }

    public async Task CheckCartBadgeAsync(int expectedCount)
    {
        await Assertions.Expect(CartBadge).ToHaveTextAsync(expectedCount.ToString());
    }

    public async Task OpenCartAsync()
    {
        await CartLink.ClickAsync();
    }
}
