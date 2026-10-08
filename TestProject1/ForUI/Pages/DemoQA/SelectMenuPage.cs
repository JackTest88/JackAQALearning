using Microsoft.Playwright;
namespace TestProject1.ForUI.Pages.DemoQA;

public class SelectMenuPage
{
    private readonly IPage Page;

    private ILocator Heading => Page.Locator("h1");
    private ILocator SelectOneDropdown => Page.Locator("#selectOne");
    // Выбранное значение в react-select (хэш в имени класса меняется, поэтому ищем по "singleValue")
    private ILocator SelectOneSelectedValue => SelectOneDropdown.Locator("[class*='singleValue']");

    public SelectMenuPage(IPage page)
    {
        Page = page;
    }

    public async Task OpenSelectMenuPageAsync()
    {
        await Page.GotoAsync("https://demoqa.com/select-menu");
    }

    public async Task CheckPageOpenAsync()
    {
        await Assertions.Expect(Page).ToHaveURLAsync("https://demoqa.com/select-menu");
        await Assertions.Expect(Heading).ToHaveTextAsync("Select Menu");
    }

    // Раскрываем дропдаун Select One и выбираем опцию по названию (Dr., Mr., Mrs., Ms., Prof., Other)
    public async Task SelectOneOptionAsync(string optionName)
    {
        await SelectOneDropdown.ClickAsync();
        await Page.GetByRole(AriaRole.Option, new() { Name = optionName, Exact = true }).ClickAsync();
    }

    public async Task CheckSelectOneValueAsync(string expectedOption)
    {
        await Assertions.Expect(SelectOneSelectedValue).ToHaveTextAsync(expectedOption);
    }
}
