namespace TestProject1.Helpers;
using System.IO;
using System.Text.Json;
using NUnit.Framework;

public class FileReader
{
    public static T ReadJson<T> (string fileName)
    {
        var path = Path.Combine(TestContext.CurrentContext.TestDirectory, "Resources", fileName);
        string json = File.ReadAllText(path);
        return JsonSerializer.Deserialize<T> (json);
    }
}