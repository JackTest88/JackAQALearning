using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestProject1.ForUI.Framework;

public class PlaywrightFixture
{
    public IPlaywright Playwright { get; private set; }
    public IBrowser Browser { get; private set; }

    public async Task InitializeAsync()
    {
        Playwright = await Microsoft.Playwright.Playwright.CreateAsync(); // запуск движка (без этого шага создать браузер нельзя)
        
        Browser = await Playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = false, // чтоб могли смотреть
            SlowMo = 2000, // замедляем действия 
            Args = new[] { "--start-maximized" } // чтоб открыть на всю страницу (не работает)
        });
    }

    public async ValueTask DisposeAsync() // гасим браузер (обязательно!)
    {
        if (Browser != null)
        {
            await Browser.CloseAsync();
        }
        Playwright?.Dispose();
    }

}