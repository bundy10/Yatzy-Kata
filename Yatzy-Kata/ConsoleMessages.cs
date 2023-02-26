namespace Yatzy_Kata;

public static class ConsoleMessages
{
    public const string Welcome = "Welcome to Yatzy";
    public const string Farewell = "bye";
    public const string CategoryChoice = "please select a category";
    public const string ReRollDecision = "Do you want to reroll any dices? y/n";
    public const string InvalidReRollDecision = "Invalid input please enter y or n";
    public const string SelectWhichDieToReRoll = "Please enter which die to reroll from 1 to 5 for example 324";
    public const string InvalidDieInput = "Please enter a number between 1 and 5";
    public const string InvalidDieIndex = "you entered a number less than 1 or more than 5, Please enter a number between 1 and 5";
    public const string AddAnotherDie = "do you want to reroll another die? y/n";

    public static string DiceHandToString(List<int> diceHand)
    {
        return string.Join(",", diceHand.Select(d => "d" + d));
    }
    
}