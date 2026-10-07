using Microsoft.Playwright;
namespace TestProject1.ForUI.Pages.Heroku;

public class MultipleWindowPage
{
    private readonly IPage Page;

    private ILocator ClickHereLink => Page.GetByRole(AriaRole.Link, new() { Name = "Click Here" });

    public MultipleWindowPage(IPage page)
    {
        Page = page;
    }

    public async Task OpenMultipleWindowPageAsync()
    {
        await Page.GotoAsync("https://the-internet.herokuapp.com/windows");
    }

    // Кликаем по ссылке и возвращаем страницу, которая открылась в новом окне/вкладке
    public async Task<IPage> OpenNewWindowAsync()
    {
        var newWindow = await Page.RunAndWaitForPopupAsync(async () =>
        {
            await ClickHereLink.ClickAsync();
        });
        await newWindow.WaitForLoadStateAsync();
        return newWindow;
    }
}
