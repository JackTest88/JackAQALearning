using Microsoft.Playwright.NUnit;
using FluentAssertions;
using Microsoft.Playwright;
using System;
using System.Text.Json;
using TestProject1.DTO.SauceDemo;
using System.Collections.Generic;
using System.Text;
using TestProject1.ForUI.Framework;
using TestProject1.ForUI.Pages.SauceDemo;

namespace TestProject1.UIAutotests;

public class SauceDemoTests: BaseTest
{
    [Test]
    public async Task SauceDemoLoginSuccess()
    {
        await Page.GotoAsync("https://www.saucedemo.com");
        var userNameTextBox = Page.GetByRole(AriaRole.Textbox, new() { Name = "Username" });
        await userNameTextBox.FillAsync("visual_user");
        
        var passTextBox = Page.GetByRole(AriaRole.Textbox, new() { Name = "Password" }); 
        await passTextBox.FillAsync("secret_sauce");
        
        var loginButton = Page.Locator("//input[@id='login-button']");
        await loginButton.ClickAsync();
        
        // проверяем, что отрисовалась новая страница 
        var titleText = await Page.Locator(".title").TextContentAsync();
        titleText.Should().Be("Products");
    }

    [Test]
    public async Task SauceDemoCheckoutTwoItems()
    {
        string[] selectedItems = { "Sauce Labs Backpack", "Sauce Labs Bike Light" };

        // открываем сайт и логинимся
        var loginPage = new LoginPage(Page);
        await loginPage.OpenLoginPageAsync();
        await loginPage.LoginAsync("standard_user", "secret_sauce");

        // на странице продуктс
        var productsPage = new ProductsPage(Page);
        await productsPage.CheckPageOpenAsync();

        // добавляем два товара
        foreach (var item in selectedItems)
        {
            await productsPage.AddToCartAsync(item);
        }
        await productsPage.CheckCartBadgeAsync(selectedItems.Length);

        // проверяем содержимое
        await productsPage.OpenCartAsync();
        var cartPage = new CartPage(Page);
        await cartPage.CheckPageOpenAsync();
        await cartPage.CheckItemsInCartAsync(selectedItems);

        // checkout
        await cartPage.ClickCheckoutAsync();

        // заполняем форму заказа
        var checkoutInfoPage = new CheckoutInfoPage(Page);
        await checkoutInfoPage.CheckPageOpenAsync();
        await checkoutInfoPage.FillFormAsync("Ivan", "Petrov", "12345");
        await checkoutInfoPage.ClickContinueAsync();

        // проверяем
        var overviewPage = new CheckoutOverviewPage(Page);
        await overviewPage.CheckPageOpenAsync();
        await overviewPage.CheckItemsAsync(selectedItems);

        // финиш и проверка
        await overviewPage.ClickFinishAsync();
        var completePage = new CheckoutCompletePage(Page);
        await completePage.CheckPageOpenAsync();
        await completePage.CheckThankYouMessageAsync();
    }

    // Данные валидных пользователей лежат в Resources/SauceDemoUsers.json
    // (locked_out_user сюда не входит - он заблокирован и войти не сможет)
    public static IEnumerable<TestCaseData> ValidUsers()
    {
        var path = Path.Combine(AppContext.BaseDirectory, "Resources", "SauceDemoUsers.json");
        var users = JsonSerializer.Deserialize<List<SauceUserDTO>>(File.ReadAllText(path))!;

        foreach (var user in users)
        {
            yield return new TestCaseData(user).SetName($"SauceDemoLogin_{user.UserName}");
        }
    }

    [TestCaseSource(nameof(ValidUsers))]
    public async Task LoginAllValidUsers(SauceUserDTO user)
    {
        var loginPage = new LoginPage(Page);
        await loginPage.OpenLoginPageAsync();
        await loginPage.LoginAsync(user.UserName, user.Password);

        var productsPage = new ProductsPage(Page);
        await productsPage.CheckPageOpenAsync();
    }
}
