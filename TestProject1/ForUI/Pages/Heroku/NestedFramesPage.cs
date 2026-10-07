using Microsoft.Playwright;
namespace TestProject1.ForUI.Pages.Heroku;

public class NestedFramesPage
{
    private readonly IPage Page;

    // Левый фрейм лежит внутри верхнего: frame-top -> frame-left
    private ILocator LeftFrameBody => Page
        .FrameLocator("frame[name='frame-top']")
        .FrameLocator("frame[name='frame-left']")
        .Locator("body");

    private ILocator BottomFrameBody => Page
        .FrameLocator("frame[name='frame-bottom']")
        .Locator("body");

    public NestedFramesPage(IPage page)
    {
        Page = page;
    }

    public async Task<string> GetTextFromLeftFrameAsync()
    {
        return await LeftFrameBody.InnerTextAsync();
    }

    public async Task<string> GetTextFromBottomFrameAsync()
    {
        return await BottomFrameBody.InnerTextAsync();
    }
}
