namespace TestProject1.ForUI.Models;

// Номера совпадают с id радиокнопок / чекбоксов на форме (gender-radio-N, hobbies-checkbox-N)
public enum Gender
{
    Male = 1,
    Female = 2,
    Other = 3
}

public enum Hobby
{
    Sports = 1,
    Reading = 2,
    Music = 3
}

// Данные, которыми заполняется форма https://demoqa.com/automation-practice-form
public class StudentFormData
{
    public string FirstName { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;
    public string? Email { get; init; }
    public Gender Gender { get; init; }
    public string Mobile { get; init; } = string.Empty;
    public DateOnly? DateOfBirth { get; init; }
    public IReadOnlyList<string> Subjects { get; init; } = new List<string>();
    public IReadOnlyList<Hobby> Hobbies { get; init; } = new List<Hobby>();
    public string? PicturePath { get; init; }
    public string? Address { get; init; }
    public string? State { get; init; }
    public string? City { get; init; }

    // Ожидаемое содержимое итоговой таблицы: "название строки" -> "значение".
    // Заполняются только те строки, поля для которых мы реально вводили.
    public Dictionary<string, string> ToExpectedResultTable()
    {
        var table = new Dictionary<string, string>
        {
            ["Student Name"] = $"{FirstName} {LastName}",
            ["Gender"] = Gender.ToString(),
            ["Mobile"] = Mobile
        };

        if (!string.IsNullOrEmpty(Email))
            table["Student Email"] = Email;

        if (DateOfBirth.HasValue)
            table["Date of Birth"] = DateOfBirth.Value.ToString("dd MMMM,yyyy", System.Globalization.CultureInfo.InvariantCulture);

        if (Subjects.Count > 0)
            table["Subjects"] = string.Join(", ", Subjects);

        // в таблице хобби идут в порядке формы (Sports, Reading, Music)
        if (Hobbies.Count > 0)
            table["Hobbies"] = string.Join(", ", Hobbies.OrderBy(h => (int)h));

        if (!string.IsNullOrEmpty(PicturePath))
            table["Picture"] = Path.GetFileName(PicturePath);

        if (!string.IsNullOrEmpty(Address))
            table["Address"] = Address;

        if (!string.IsNullOrEmpty(State))
            table["State and City"] = $"{State} {City}".Trim();

        return table;
    }
}
