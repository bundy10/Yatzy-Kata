using Yatzy_Kata.Data;
using Yatzy_Kata.Interfaces;

namespace Yatzy_Kata.Strategies;

public class ComputerStrategy : IStrategy
{
    private int _currentScore;
    private ScoreAndCategoryAtTurnEnd? _currentScoreAndCategoryAtTurnEnd;
    private List<int>? _currentDiceRoll;
    private readonly ComputerDiceRollStrategy _diceRollStrategy;

    public ComputerStrategy()
    {
        _diceRollStrategy = new ComputerDiceRollStrategy();
    }
    
    public ScoreAndCategoryAtTurnEnd CalculateScore(List<int> diceRoll, List<Category> remainingCategories)
    {
        _currentDiceRoll = diceRoll;
        _currentScore = 0;
        if (remainingCategories.Count != 0)
        {
            SelectCategoryStrategy(remainingCategories);
        }
        if (_currentScoreAndCategoryAtTurnEnd == null)
        {
            throw new InvalidOperationException("Something is wrong currentScoreAndCategoryAtTurnEnd cannot be null");
        }
    
        return _currentScoreAndCategoryAtTurnEnd;
    }

    public void SelectCategoryStrategy(List<Category> remainingCategories)
    {
        for (var i = remainingCategories.Count - 1; i >= 0; i--)
        {
            if (remainingCategories[i].CalculateScore(_currentDiceRoll) > 0)
            {
                _currentScore = remainingCategories[i].CalculateScore(_currentDiceRoll);
                _currentScoreAndCategoryAtTurnEnd = new ScoreAndCategoryAtTurnEnd(_currentScore, remainingCategories[i]);
                break;
            }
        }

        if (_currentScore == 0)
        {
            _currentScoreAndCategoryAtTurnEnd = new ScoreAndCategoryAtTurnEnd(_currentScore, remainingCategories[0]);

        }
    }

    public ScoreAndCategoryAtTurnEnd GetScoreAndCategoryAtTurnEnd(List<Category> remainingCategories)
    {
        _diceRollStrategy.RollDice();
        var diceHand = _diceRollStrategy.GetDiceHand();
        return CalculateScore(diceHand, remainingCategories);
    }
}