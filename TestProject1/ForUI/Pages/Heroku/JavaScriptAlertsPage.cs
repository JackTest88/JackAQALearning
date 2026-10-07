using Microsoft.Playwright;
namespace TestProject1.ForUI.Pages.Heroku;

public class JavaScriptAlertsPage
{
    private readonly IPage Page;

    private ILocator JsAlertButton => Page.GetByRole(AriaRole.Button, new() { Name = "Click for JS Alert" });
    private ILocator JsConfirmButton => Page.GetByRole(AriaRole.Button, new() { Name = "Click for JS Confirm" });
    private ILocator JsPromptButton => Page.GetByRole(AriaRole.Button, new() { Name = "Click for JS Prompt" });
    private ILocator Result => Page.Locator("#result");

    public JavaScriptAlertsPage(IPage page)
    {
        Page = page;
    }

    public async Task OpenAlertsPageAsync()
    {
        await Page.GotoAsync("https://the-internet.herokuapp.com/javascript_alerts");
    }

    public async Task ClickJsAlertButtonAsync()
    {
        await JsAlertButton.ClickAsync();
    }

    public async Task ClickJsConfirmButtonAsync()
    {
        await JsConfirmButton.ClickAsync();
    }

    public async Task ClickJsPromptButtonAsync()
    {
        await JsPromptButton.ClickAsync();
    }

    // Ждём, пока страница запишет результат в #result, и возвращаем его текст
    public async Task<string> GetResultTextAsync()
    {
        await Assertions.Expect(Result).Not.ToBeEmptyAsync();
        return (await Result.InnerTextAsync()).Trim();
    }
}
