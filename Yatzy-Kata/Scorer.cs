using Yatzy_Kata.Data;
using Yatzy_Kata.Interfaces;

namespace Yatzy_Kata;

public class Scorer : IScorer
{
    private int _totalScore;
    private int _currentScore;
    private DiceHandAndCategoryAtTurnEnd? _currentDiceHandAndCategoryAtTurnEnd;
    private List<int> _currentDiceRoll;
    
    public DiceHandAndCategoryAtTurnEnd CalculateScore(List<int> diceRoll, List<Category> remainingCategories)
    {
        _currentDiceRoll = diceRoll;
        _currentScore = 0;
        if (remainingCategories.Count != 0)
        {
            Strategy(remainingCategories);
        }
        return _currentDiceHandAndCategoryAtTurnEnd;
    }

    private void Strategy(List<Category> remainingCategories)
    {
        for (var i = remainingCategories.Count - 1; i >= 0; i--)
        {
            if (remainingCategories[i].CalculateScore(_currentDiceRoll) > 0)
            {
                _currentScore = remainingCategories[i].CalculateScore(_currentDiceRoll);
                _totalScore += _currentScore;
                _currentDiceHandAndCategoryAtTurnEnd = new DiceHandAndCategoryAtTurnEnd(_currentDiceRoll, remainingCategories[i]);
                break;
            }
        }

        if (_currentScore == 0)
        {
            _currentDiceHandAndCategoryAtTurnEnd = new DiceHandAndCategoryAtTurnEnd(_currentDiceRoll, remainingCategories[0]);
            remainingCategories.RemoveAt(0);
            
        }
    }

    public int GetTotalScore()
    {
        return _totalScore;
    }
}