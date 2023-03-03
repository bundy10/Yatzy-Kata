using Yatzy_Kata.Data;
using Yatzy_Kata.Interfaces;

namespace Yatzy_Kata.Strategies;

public class ComputerStrategy : IStrategy
{
    private List<int> _currentDiceRoll;
    private readonly ComputerDiceRollStrategy _diceRollStrategy;
    private bool _abandoned;
    private TurnResults? _turnResults;

    public ComputerStrategy()
    {
        _abandoned = false;
        _currentDiceRoll = new List<int>();
        _diceRollStrategy = new ComputerDiceRollStrategy( new RandomDiceRoll());
    }
    
    public TurnResults? GetTurnResults()
    {
        return _turnResults;
    }
    
    public void CalculateTurnResults(List<Category> remainingCategories)
    {
        _diceRollStrategy.RollDice();
        _currentDiceRoll = _diceRollStrategy.GetDiceHand();
        var selectedCategory = SelectCategoryStrategy(remainingCategories);
        var score = selectedCategory.CalculateScore(_currentDiceRoll);
        _turnResults = new TurnResults(score, selectedCategory);
    }

    public Category SelectCategoryStrategy(List<Category> remainingCategories)
    {
        for (var i = remainingCategories.Count - 1; i >= 0; i--)
        {
            if (remainingCategories[i].CalculateScore(_currentDiceRoll) > 0)
            {
                return remainingCategories[i];
            }
        }
        return remainingCategories[0];
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