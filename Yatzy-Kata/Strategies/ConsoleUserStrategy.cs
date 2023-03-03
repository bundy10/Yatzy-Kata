using Yatzy_Kata.Data;
using Yatzy_Kata.Interfaces;

namespace Yatzy_Kata.Strategies;

public class ConsoleUserStrategy : IStrategy
{
    private readonly ConsoleDiceRollStrategy _diceRollStrategy;
    private readonly IReader _reader;
    private readonly IWriter _writer;
    private bool _abandoned;

    public ConsoleUserStrategy(IReader reader, IWriter writer)
    {
        _reader = reader;
        _writer = writer;
        _abandoned = false;
        _diceRollStrategy = new ConsoleDiceRollStrategy(reader, writer, new RandomDiceRoll());
    }

    public TurnResults CalculateScore(List<int> diceRoll, List<Category> remainingCategories)
    {
        var selectedCategory = SelectCategoryStrategy(remainingCategories);
        return new TurnResults(selectedCategory.CalculateScore(diceRoll), selectedCategory);
    }

    public Category SelectCategoryStrategy(List<Category> remainingCategories)
    {
        _writer.WriteLine(ConsoleMessages.SelectCategory);
        _writer.WriteLine(ConsoleMessages.RemainingCategoriesToString(remainingCategories));
        int selectedIndex;
        do
        {
            Console.Write("Enter a number from 1 to {0}: ", remainingCategories.Count);
        } while (!int.TryParse(_reader.ReadLine(), out selectedIndex) || selectedIndex < 1 || selectedIndex > remainingCategories.Count);

        return remainingCategories[selectedIndex - 1];
    }

    public TurnResults GetTurnResults(List<Category> remainingCategories)
    {
        _diceRollStrategy.RollDice();
        return CalculateScore(_diceRollStrategy.GetDiceHand(), remainingCategories);
    }

    public void DoesPlayerWantToAbandonGame()
    {
        _writer.WriteLine(ConsoleMessages.PlayAgainOrNot);
        var abandonChoice = _reader.ReadLine();

        if (abandonChoice?.ToLowerInvariant() == "y")
        {
            _abandoned = true;
        }
    }

    public bool GetAbandonChoice()
    {
        return _abandoned;
    }
}