using Microsoft.Playwright;
namespace TestProject1.ForUI.Pages.Heroku;

public class AddRemovePage
{
    private readonly IPage Page;

    private ILocator Heading => Page.Locator("h3");

    public AddRemovePage(IPage page)
    {
        Page = page;
    }

    private ILocator ButtonsByName(string name)
    {
        return Page.GetByRole(AriaRole.Button, new() { Name = name, Exact = true });
    }

    public async Task OpenAddRemovePageAsync()
    {
        await Page.GotoAsync("https://the-internet.herokuapp.com/add_remove_elements/");
    }

    public async Task CheckPageOpenAsync()
    {
        await Assertions.Expect(Page).ToHaveTitleAsync("The Internet");
        await Assertions.Expect(Page).ToHaveURLAsync("https://the-internet.herokuapp.com/add_remove_elements/");
        await Assertions.Expect(Heading).ToHaveTextAsync("Add/Remove Elements");
    }

    // Клик по кнопке, которая на странице одна (например, "Add Element").
    // Если таких кнопок несколько - Playwright выдаст ошибку, для этого есть метод с номером.
    public async Task ClickButtonByNameAsync(string name)
    {
        await ButtonsByName(name).ClickAsync();
    }

    // Клик по N-й кнопке с таким названием (нумерация с 1)
    public async Task ClickButtonByNameAndNumberAsync(string name, int number)
    {
        await ButtonsByName(name).Nth(number - 1).ClickAsync();
    }

    public async Task CheckNumberOfButtonAsync(string name, int expectedCount)
    {
        await Assertions.Expect(ButtonsByName(name)).ToHaveCountAsync(expectedCount);
    }
}
