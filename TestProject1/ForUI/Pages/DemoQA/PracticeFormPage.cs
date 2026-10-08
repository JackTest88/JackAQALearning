using System.Globalization;
using Microsoft.Playwright;
using TestProject1.ForUI.Models;
namespace TestProject1.ForUI.Pages.DemoQA;

public class PracticeFormPage
{
    private readonly IPage Page;

    private ILocator Heading => Page.Locator("h1");
    private ILocator FirstNameTextBox => Page.GetByPlaceholder("First Name");
    private ILocator LastNameTextBox => Page.GetByPlaceholder("Last Name");
    private ILocator EmailTextBox => Page.GetByPlaceholder("name@example.com");
    private ILocator MobileTextBox => Page.GetByPlaceholder("Mobile Number");
    private ILocator DateOfBirthTextBox => Page.Locator("#dateOfBirthInput");
    private ILocator SubjectsTextBox => Page.Locator("#subjectsInput");
    private ILocator PictureInput => Page.Locator("#uploadPicture");
    private ILocator AddressTextArea => Page.GetByPlaceholder("Current Address");
    private ILocator StateDropdown => Page.Locator("#state");
    private ILocator CityDropdown => Page.Locator("#city");
    private ILocator SubmitButton => Page.GetByRole(AriaRole.Button, new() { Name = "Submit" });

    // окно с результатом после Submit
    private ILocator ResultModal => Page.GetByRole(AriaRole.Dialog);
    private ILocator ResultRows => ResultModal.Locator("tbody tr");

    public PracticeFormPage(IPage page)
    {
        Page = page;
    }

    public async Task OpenPracticeFormPageAsync()
    {
        await Page.GotoAsync("https://demoqa.com/automation-practice-form");
    }

    public async Task CheckPageOpenAsync()
    {
        await Assertions.Expect(Page).ToHaveURLAsync("https://demoqa.com/automation-practice-form");
        await Assertions.Expect(Heading).ToHaveTextAsync("Practice Form");
    }

    // Заполняем только те поля, которые заданы в данных
    public async Task FillFormAsync(StudentFormData data)
    {
        await FirstNameTextBox.FillAsync(data.FirstName);
        await LastNameTextBox.FillAsync(data.LastName);

        if (!string.IsNullOrEmpty(data.Email))
            await EmailTextBox.FillAsync(data.Email);

        // радиокнопка скрыта стилями, поэтому кликаем по её label
        await Page.Locator($"label[for='gender-radio-{(int)data.Gender}']").ClickAsync();

        await MobileTextBox.FillAsync(data.Mobile);

        if (data.DateOfBirth.HasValue)
            await SetDateOfBirthAsync(data.DateOfBirth.Value);

        foreach (var subject in data.Subjects)
            await AddSubjectAsync(subject);

        foreach (var hobby in data.Hobbies)
            await Page.Locator($"label[for='hobbies-checkbox-{(int)hobby}']").ClickAsync();

        if (!string.IsNullOrEmpty(data.PicturePath))
            await PictureInput.SetInputFilesAsync(data.PicturePath);

        if (!string.IsNullOrEmpty(data.Address))
            await AddressTextArea.FillAsync(data.Address);

        if (!string.IsNullOrEmpty(data.State))
        {
            await SelectFromDropdownAsync(StateDropdown, data.State);
            // список городов становится доступен только после выбора штата
            if (!string.IsNullOrEmpty(data.City))
                await SelectFromDropdownAsync(CityDropdown, data.City);
        }
    }

    public async Task ClickSubmitAsync()
    {
        await SubmitButton.ClickAsync();
    }

    public async Task CheckSuccessMessageAsync()
    {
        await Assertions.Expect(ResultModal.GetByText("Thanks for submitting the form"))
            .ToBeVisibleAsync();
    }

    // Итоговая таблица в виде "название строки" -> "значение"
    public async Task<Dictionary<string, string>> GetResultTableAsync()
    {
        await Assertions.Expect(ResultRows.First).ToBeVisibleAsync();

        var table = new Dictionary<string, string>();
        var count = await ResultRows.CountAsync();
        for (var i = 0; i < count; i++)
        {
            var cells = ResultRows.Nth(i).Locator("td");
            var label = (await cells.Nth(0).InnerTextAsync()).Trim();
            var value = (await cells.Nth(1).InnerTextAsync()).Trim();
            table[label] = value;
        }
        return table;
    }

    private async Task SetDateOfBirthAsync(DateOnly date)
    {
        // формат поля на странице: "07 Oct 2026"
        await DateOfBirthTextBox.ClickAsync();
        await Page.Keyboard.PressAsync("ControlOrMeta+A");
        await Page.Keyboard.TypeAsync(date.ToString("dd MMM yyyy", CultureInfo.InvariantCulture));
        await Page.Keyboard.PressAsync("Enter");
    }

    private async Task AddSubjectAsync(string subject)
    {
        await SubjectsTextBox.FillAsync(subject);
        await Page.GetByRole(AriaRole.Option, new() { Name = subject, Exact = true }).ClickAsync();
    }

    private async Task SelectFromDropdownAsync(ILocator dropdown, string optionName)
    {
        await dropdown.ClickAsync();
        await Page.GetByRole(AriaRole.Option, new() { Name = optionName, Exact = true }).ClickAsync();
    }
}
