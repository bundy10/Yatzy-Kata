using Yatzy_Kata.Data;

namespace Yatzy_Kata;

public static class ConsoleMessages
{
    public const string Welcome = "Welcome to Yatzy";
    public const string Farewell = "bye";
    public const string ReRollDecision = "Do you want to reroll any dices? y/n";
    public const string InvalidReRollDecision = "Invalid input please enter y or n";
    public const string SelectWhichDieToReRoll = "Please enter which die to reroll from 1 to 5 for example 324";
    public const string InvalidDieInput = "Please enter numbers between 1 and 5";
    public const string SelectCategory = "Select a category";

    public static string DiceHandToString(List<int> diceHand)
    {
        return string.Join(",", diceHand.Select(d => "d" + d));
    }

    public static string RemainingCategoriesToString(List<Category> remainingCategories)
    {
        var categoryNames = remainingCategories.Select((category, index) => $"{index + 1}. {category.GetType().Name}");
        return string.Join("\n", categoryNames);
    }
}