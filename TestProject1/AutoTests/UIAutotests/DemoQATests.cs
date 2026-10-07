using Microsoft.Playwright.NUnit;
using FluentAssertions;
using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Text;
using TestProject1.ForUI.Framework;
using TestProject1.ForUI.Pages.DemoQA;

namespace TestProject1.UIAutotests;

public class DemoQATests: BaseTest
{
    [Test]
    public async Task ShouldSelectSubItems()
    {
        /*
        код с урока закомментил
        await Page.GotoAsync("https://demoqa.com/select-menu");

        var dropdown = Page.Locator("#withOptGroup");
        await dropdown.ClickAsync();

        var option = Page.GetByText("Group 1, option 1");
        await option.ClickAsync();
        await Assertions.Expect(dropdown).ToContainTextAsync("Group 1, option 1");
        */
        // далее валидный код
        await Page.GotoAsync("https://demoqa.com/select-menu");
        await Page.Locator("#withOptGroup").ClickAsync();
        
        var option = Page.GetByRole(AriaRole.Option, new() { Name = "Group 1, option 1" });
        await option.ClickAsync();
        await Microsoft.Playwright.Assertions.Expect(Page.Locator("#withOptGroup"))
            .ToContainTextAsync("Group 1, option 1");
    }

    [Test]
    public async Task ShouldSelectProfInSelectOne()
    {
        var selectMenuPage = new SelectMenuPage(Page);
        await selectMenuPage.OpenSelectMenuPageAsync();
        await selectMenuPage.CheckPageOpenAsync();

        await selectMenuPage.SelectOneOptionAsync("Prof.");

        await selectMenuPage.CheckSelectOneValueAsync("Prof.");
    }
}
