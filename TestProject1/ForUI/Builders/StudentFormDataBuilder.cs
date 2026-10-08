using TestProject1.ForUI.Models;

namespace TestProject1.ForUI.Builders;

// Builder для данных формы: собираем только те поля, которые нужны конкретному тесту
public class StudentFormDataBuilder
{
    private string _firstName = string.Empty;
    private string _lastName = string.Empty;
    private string? _email;
    private Gender? _gender;
    private string _mobile = string.Empty;
    private DateOnly? _dateOfBirth;
    private readonly List<string> _subjects = new();
    private readonly List<Hobby> _hobbies = new();
    private string? _picturePath;
    private string? _address;
    private string? _state;
    private string? _city;

    public StudentFormDataBuilder WithFirstName(string firstName)
    {
        _firstName = firstName;
        return this;
    }

    public StudentFormDataBuilder WithLastName(string lastName)
    {
        _lastName = lastName;
        return this;
    }

    public StudentFormDataBuilder WithEmail(string email)
    {
        _email = email;
        return this;
    }

    public StudentFormDataBuilder WithGender(Gender gender)
    {
        _gender = gender;
        return this;
    }

    public StudentFormDataBuilder WithMobile(string mobile)
    {
        _mobile = mobile;
        return this;
    }

    public StudentFormDataBuilder WithDateOfBirth(DateOnly dateOfBirth)
    {
        _dateOfBirth = dateOfBirth;
        return this;
    }

    public StudentFormDataBuilder WithSubject(string subject)
    {
        _subjects.Add(subject);
        return this;
    }

    public StudentFormDataBuilder WithHobby(Hobby hobby)
    {
        _hobbies.Add(hobby);
        return this;
    }

    public StudentFormDataBuilder WithPicture(string picturePath)
    {
        _picturePath = picturePath;
        return this;
    }

    public StudentFormDataBuilder WithAddress(string address)
    {
        _address = address;
        return this;
    }

    public StudentFormDataBuilder WithStateAndCity(string state, string city)
    {
        _state = state;
        _city = city;
        return this;
    }

    public StudentFormData Build()
    {
        // обязательные поля формы: имя, фамилия, пол, телефон
        if (string.IsNullOrWhiteSpace(_firstName))
            throw new InvalidOperationException("FirstName is required");
        if (string.IsNullOrWhiteSpace(_lastName))
            throw new InvalidOperationException("LastName is required");
        if (_gender is null)
            throw new InvalidOperationException("Gender is required");
        if (string.IsNullOrWhiteSpace(_mobile))
            throw new InvalidOperationException("Mobile is required");

        return new StudentFormData
        {
            FirstName = _firstName,
            LastName = _lastName,
            Email = _email,
            Gender = _gender.Value,
            Mobile = _mobile,
            DateOfBirth = _dateOfBirth,
            Subjects = _subjects.ToList(),
            Hobbies = _hobbies.ToList(),
            PicturePath = _picturePath,
            Address = _address,
            State = _state,
            City = _city
        };
    }
}
