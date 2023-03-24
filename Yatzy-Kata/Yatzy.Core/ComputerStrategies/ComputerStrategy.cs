using Yatzy_Kata.Data;
using Yatzy_Kata.Interfaces;

namespace Yatzy_Kata.Strategies;

public class ComputerStrategy : IStrategy
{
    private List<int> _currentDiceRoll;
    private readonly ComputerDiceRollStrategy _diceRollStrategy;
    private bool _abandoned;
    private TurnResults? _turnResults;
    private List<Category>? _categoriesAvailable;

    public ComputerStrategy(IRandom randomDiceRoll)
    {
        _abandoned = false;
        _currentDiceRoll = new List<int>();
        _diceRollStrategy = new ComputerDiceRollStrategy(randomDiceRoll);
    }
    
    public TurnResults? GetTurnResults()
    {
        return _turnResults;
    }
    
    public void CalculateTurnResults(List<Category> remainingCategories)
    {
        _categoriesAvailable = remainingCategories;
        _diceRollStrategy.RollDice();
        _currentDiceRoll = _diceRollStrategy.GetDiceHand();
        var selectedCategory = SelectCategoryStrategy();
        var score = selectedCategory.CalculateScore(_currentDiceRoll);
        _turnResults = new TurnResults(score, selectedCategory);
    }

    private Category SelectCategoryStrategy()
    {
        for (var i = _categoriesAvailable!.Count - 1; i >= 0; i--)
        {
            if (_categoriesAvailable[i].CalculateScore(_currentDiceRoll) > 0)
            {
                return _categoriesAvailable[i];
            }
        }
        return _categoriesAvailable[0];
    }
    
    public bool GetAbandonChoice()
    {
        return _abandoned;
    }

    public void DoesPlayerWantToAbandonGame()
    {
        _abandoned = false;
    }
}