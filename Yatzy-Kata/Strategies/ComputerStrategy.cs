using Yatzy_Kata.Data;
using Yatzy_Kata.Interfaces;

namespace Yatzy_Kata.Strategies;

public class ComputerStrategy : IStrategy
{
    private List<int> _currentDiceRoll;
    private readonly ComputerDiceRollStrategy _diceRollStrategy;

    public ComputerStrategy()
    {
        _currentDiceRoll = new List<int>();
        _diceRollStrategy = new ComputerDiceRollStrategy();
    }
    
    public ScoreAndCategoryAtTurnEnd CalculateScore(List<int> diceRoll, List<Category> remainingCategories)
    {
        _currentDiceRoll = diceRoll;
        var selectedCategory = SelectCategoryStrategy(remainingCategories);
        return new ScoreAndCategoryAtTurnEnd(selectedCategory.CalculateScore(diceRoll), selectedCategory);
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

    public ScoreAndCategoryAtTurnEnd GetScoreAndCategoryAtTurnEnd(List<Category> remainingCategories)
    {
        _diceRollStrategy.RollDice();
        return CalculateScore(_diceRollStrategy.GetDiceHand(), remainingCategories);
    }
}