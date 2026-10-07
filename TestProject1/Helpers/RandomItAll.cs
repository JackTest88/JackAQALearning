using System;
using System.Collections.Generic;
namespace TestProject1.Helpers;

public class RandomItAll
{
    public static T GetRandomItem<T>(IList<T> items)
    {
        if (items == null || items.Count == 0)
            throw new ArgumentException("List is null or empty", nameof(items));

        int chosenIndex = Random.Shared.Next(items.Count);
        return items[chosenIndex];
    }
}