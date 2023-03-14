using Yatzy_Kata.Data;
using Yatzy_Kata.Interfaces;

namespace Yatzy_Kata.Strategies;

public class ConsoleUserStrategy : IStrategy
{
    private readonly ConsoleDiceRollStrategy _diceRollStrategy;
    private readonly IReader _reader;
    private readonly IWriter _writer;
    private bool _abandoned;
    private TurnResults? _turnResults;
    private List<Category>? _categoriesAvailable;

    public ConsoleUserStrategy(IReader reader, IWriter writer, IRandom random)
    {
        _reader = reader;
        _writer = writer;
        _abandoned = false;
        _diceRollStrategy = new ConsoleDiceRollStrategy(reader, writer, random);
    }
    
    public TurnResults? GetTurnResults()
    {
        return _turnResults;
    }

    public void CalculateTurnResults(List<Category> remainingCategories)
    {
        _categoriesAvailable = remainingCategories;
        _diceRollStrategy.RollDice();
        var diceRoll = _diceRollStrategy.GetDiceHand();
        var selectedCategory = SelectCategoryStrategy();
        var score = selectedCategory.CalculateScore(diceRoll);
        _turnResults = new TurnResults(score, selectedCategory);
    }

    private Category SelectCategoryStrategy()
    {
        _writer.WriteLine(ConsoleMessages.SelectCategory);
        _writer.WriteLine(ConsoleMessages.RemainingCategoriesToString(_categoriesAvailable));
        int selectedIndex;
        do
        {
            _writer.WriteLine(ConsoleMessages.InvalidCategorySelectionInput);
            _writer.WriteLine(ConsoleMessages.SelectCategory);
            _writer.WriteLine(ConsoleMessages.RemainingCategoriesToString(_categoriesAvailable));
        } while (!int.TryParse(_reader.ReadLine(), out selectedIndex) || selectedIndex < 1 || selectedIndex > _categoriesAvailable!.Count);

        return _categoriesAvailable[selectedIndex - 1];
    }

    public void DoesPlayerWantToAbandonGame()
    {
        _writer.WriteLine(ConsoleMessages.AbandonOrNot);
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