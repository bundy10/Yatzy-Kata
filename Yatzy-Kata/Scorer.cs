using Yatzy_Kata.Data;
using Yatzy_Kata.Interfaces;

namespace Yatzy_Kata;

public class Scorer : IScorer
{
    private int _totalScore;
    private int _currentScore;
    private DiceHandAndCategoryAtTurnEnd? _currentDiceHandAndCategoryAtTurnEnd;
    private List<int> _currentDiceRoll;
    private List<Category> _remainingCategorys;

    public Scorer()
    {
        _remainingCategorys = new List<Category>() { new Aces(), new Twos(), new Threes(), new Fours(), new Fives(), new Sixes(), new Chance(), new ThreeOfAKind(), new FourOfAKind(), new FullHouse(), new SmallStraight(), new LargeStraight(), new Yahtzee()};
    }
    public DiceHandAndCategoryAtTurnEnd CalculateScore(List<int> diceRoll)
    {
        _currentDiceRoll = diceRoll;
        _currentScore = 0;
        if (_remainingCategorys.Count != 0)
        {
            Strategy();
        }
        return _currentDiceHandAndCategoryAtTurnEnd;
    }

    private void Strategy()
    {
        for (var i = _remainingCategorys.Count - 1; i >= 0; i--)
        {
            if (_remainingCategorys[i].CalculateScore(_currentDiceRoll) > 0)
            {
                _currentScore = _remainingCategorys[i].CalculateScore(_currentDiceRoll);
                _totalScore += _currentScore;
                _currentDiceHandAndCategoryAtTurnEnd = new DiceHandAndCategoryAtTurnEnd(_currentDiceRoll, _remainingCategorys[i]);
                _remainingCategorys.Remove(_remainingCategorys[i]);
                break;
            }
        }

        if (_currentScore == 0)
        {
            _currentDiceHandAndCategoryAtTurnEnd = new DiceHandAndCategoryAtTurnEnd(_currentDiceRoll, _remainingCategorys[0]);
            _remainingCategorys.RemoveAt(0);
            
        }
    }

    public int GetTotalScore()
    {
        return _totalScore;
    }
}