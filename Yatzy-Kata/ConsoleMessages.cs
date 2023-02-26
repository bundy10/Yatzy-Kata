namespace Yatzy_Kata;

public static class ConsoleMessages
{
    public const string Welcome = "Welcome to Yatzy";
    public const string Farewell = "bye";
    public const string CategoryChoice = "please select a category";

    public static string DiceHandToString(List<int> diceHand)
    {
        return string.Join(",", diceHand.Select(d => "d" + d));
    }
    
}