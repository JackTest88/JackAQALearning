using Microsoft.Playwright.NUnit;
using FluentAssertions;
using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Text;
using TestProject1.ForUI.Framework;
using FluentAssertions.Execution;
using TestProject1.ForUI.Builders;
using TestProject1.ForUI.Models;
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

    [Test]
    public async Task FormFillAndCheckResult()
    {
        var student = new StudentFormDataBuilder()
            .WithFirstName("Ivan")
            .WithLastName("Petrov")
            .WithEmail("ivan.petrov@example.com")
            .WithGender(Gender.Male)
            .WithMobile("9123456789")
            .WithDateOfBirth(new DateOnly(1990, 5, 15))
            .WithSubject("Maths")
            .WithSubject("Physics")
            .WithHobby(Hobby.Sports)
            .WithHobby(Hobby.Music)
            .WithPicture(Path.Combine(TestContext.CurrentContext.TestDirectory, "Resources", "student.png"))
            .WithAddress("Budva, Slovenska plaza 1")
            .WithStateAndCity("NCR", "Delhi")
            .Build();

        var practiceFormPage = new PracticeFormPage(Page);
        await practiceFormPage.OpenPracticeFormPageAsync();
        await practiceFormPage.CheckPageOpenAsync();

        await practiceFormPage.FillFormAsync(student);
        await practiceFormPage.ClickSubmitAsync();

        await practiceFormPage.CheckSuccessMessageAsync();

        // сверяем итоговую таблицу с тем, что мы заполнили
        var actualTable = await practiceFormPage.GetResultTableAsync();
        var expectedTable = student.ToExpectedResultTable();

        using (new AssertionScope())
        {
            foreach (var (label, expectedValue) in expectedTable)
            {
                actualTable.Should().ContainKey(label);
                actualTable.GetValueOrDefault(label).Should().Be(expectedValue, $"строка \"{label}\"");
            }
        }
    }
}
