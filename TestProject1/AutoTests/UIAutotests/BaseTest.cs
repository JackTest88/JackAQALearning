using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Text;
using TestProject1.ForUI.Framework;

namespace TestProject1.UIAutotests;

// от него можно наследовать другие тесты

public class BaseTest
{
    protected IPage Page { get; private set; } // перед каждым тестом мы создаем страницу 
    protected PlaywrightFixture Fixture { get; } 

    protected BaseTest()
    {
        Fixture = new PlaywrightFixture();
        Fixture.InitializeAsync().Wait(); // чтобы немного подождать, пока браузер стартанет нормально
    }

    [SetUp] //этот метод отработает перед каждым автотестом - открытие страницы
    public async Task SetUp() 
    {
        Page = await Fixture.Browser.NewPageAsync(new BrowserNewPageOptions
        {
            ViewportSize = null
        });
    }

    [TearDown]
    public async Task TearDown()
    {
        await Page.CloseAsync();
    }

    [OneTimeTearDown]
    public async Task GlobalTearDown()
    {
        await Fixture.DisposeAsync();
    }
}