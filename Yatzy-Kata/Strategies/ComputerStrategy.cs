using Yatzy_Kata.Data;
using Yatzy_Kata.Interfaces;

namespace Yatzy_Kata.Strategies;

public class ComputerStrategy : IStrategy
{
    private List<int> _currentDiceRoll;
    private readonly ComputerDiceRollStrategy _diceRollStrategy;
    private bool _abandoned;

    public ComputerStrategy()
    {
        _abandoned = false;
        _currentDiceRoll = new List<int>();
        _diceRollStrategy = new ComputerDiceRollStrategy( new RandomDiceRoll());
    }
    
    public TurnResults CalculateScore(List<int> diceRoll, List<Category> remainingCategories)
    {
        _currentDiceRoll = diceRoll;
        var selectedCategory = SelectCategoryStrategy(remainingCategories);
        return new TurnResults(selectedCategory.CalculateScore(diceRoll), selectedCategory);
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

    public TurnResults GetTurnResults(List<Category> remainingCategories)
    {
        _diceRollStrategy.RollDice();
        return CalculateScore(_diceRollStrategy.GetDiceHand(), remainingCategories);
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