using Yatzy_Kata.Data;
using Yatzy_Kata.Outcomes;

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
    public const string AbandonOrNot = "Do you want to abandon the game? enter y to abandon or any other key to continue";
    public const string InvalidCategorySelectionInput = "Please Enter A valid Category Number Shown";

    public static string RoundCount(int roundNum) => $"Round: {roundNum.ToString()}";

    public static string DiceHandToString(IEnumerable<int> diceHand)
    {
        return string.Join(",", diceHand.Select(d => "d" + d));
    }

    public static string RemainingCategoriesToString(IEnumerable<Category> remainingCategories)
    {
        var categoryNames = remainingCategories.Select((category, index) => $"{index + 1}. {category.GetType().Name}");
        return string.Join("\n", categoryNames);
    }

    public static string RoundResults(RoundOutcomes? roundResult)
    {
        var result = roundResult switch
        {
            RoundTie roundTie => $"Round tied at {roundTie.Score} with players {string.Join(", ", roundTie.Players.Select(p => p.Name))}.",
            RoundWinner roundWinner => $"Round won by {roundWinner.Player.Name} with a score of {roundWinner.Score}.",
            _ => "New round abandoned. No one wins."
        };
    
        return result;
    }

    public static string Winner(Player player) => $"Player {player.Name} is the winner! with a total score of {player.PlayerTotalPoints()} and a total time spent of {player.PlayerTotalTimeSpentInTurns()} seconds ";
}