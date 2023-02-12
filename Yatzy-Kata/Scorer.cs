using Yatzy_Kata.Data;
using Yatzy_Kata.Interfaces;

namespace Yatzy_Kata;

public class Scorer : IScorer
{

    private int _currentScore;
    private DiceHandAndCategoryAtTurnEnd? _currentDiceHandAndCategoryAtTurnEnd;
    private List<int> _currentDiceRoll;
    private List<Category> _remainingCategorys;

    public Scorer()
    {
        _remainingCategorys = new List<Category>() {new Chance(), new Aces(), new Twos(), new Threes(), new Fours(), new Fives(), new Sixes(), new ThreeOfAKind(), new FourOfAKind(), new FullHouse(), new SmallStraight(), new LargeStraight(), new Yahtzee()};
    }
    public DiceHandAndCategoryAtTurnEnd CalculateScore(List<int> diceRoll)
    {
        _currentDiceRoll = diceRoll;
        _currentScore = 0;
        do
        {
            CheckForLargeStraight();
            CheckForSmallStraight();
            CheckForFullHouse();
            CheckForFourOfAKind();
            CheckForThreeOfAKind();
            CheckForFives();
            CheckForThrees();
            CheckForTwos();
            CheckForOnes();
            
        } while (_currentScore == 0);
        return _currentDiceHandAndCategoryAtTurnEnd;
    }

    private void Strategy()
    {
        foreach (var category in _remainingCategorys)
        {
            int score = category.CalculateScore(_currentDiceRoll);
        }
    }

    private void CheckForLowerCategory()
    {
        CheckForSixes();
        CheckForFives();
        CheckForFours();
        CheckForThrees();
        CheckForTwos();
        CheckForOnes();
    }
    
    private void CheckForUpperSectionCategory()
    {
        CheckForYatzy();
        CheckForLargeStraight();
        CheckForSmallStraight();
        CheckForFullHouse();
        CheckForFourOfAKind();
        CheckForThreeOfAKind();
    }

    private void CheckForSixes()
    {
        var category = new Sixes(_currentDiceRoll);
        if (category.CalculateScore() != 0)
        {
            _currentScore = category.CalculateScore();
            _currentDiceHandAndCategoryAtTurnEnd = new DiceHandAndCategoryAtTurnEnd(_currentDiceRoll, category);
        }
    }
    
    private void CheckForFives()
    {
        var category = new Fives(_currentDiceRoll);
        if (category.CalculateScore() != 0)
        {
            _currentScore = category.CalculateScore();
            _currentDiceHandAndCategoryAtTurnEnd = new DiceHandAndCategoryAtTurnEnd(_currentDiceRoll, category);
        }
    }
    
    private void CheckForFours()
    {
        var category = new Fours(_currentDiceRoll);
        if (category.CalculateScore() != 0)
        {
            _currentScore = category.CalculateScore();
            _currentDiceHandAndCategoryAtTurnEnd = new DiceHandAndCategoryAtTurnEnd(_currentDiceRoll, category);
        }
    }
    
    private void CheckForThrees()
    {
        var category = new Threes(_currentDiceRoll);
        if (category.CalculateScore() != 0)
        {
            _currentScore = category.CalculateScore();
            _currentDiceHandAndCategoryAtTurnEnd = new DiceHandAndCategoryAtTurnEnd(_currentDiceRoll, category);
        }
    }
    
    private void CheckForTwos()
    {
        var category = new Twos(_currentDiceRoll);
        if (category.CalculateScore() != 0)
        {
            _currentScore = category.CalculateScore();
            _currentDiceHandAndCategoryAtTurnEnd = new DiceHandAndCategoryAtTurnEnd(_currentDiceRoll, category);
        }
    }
    
    private void CheckForOnes()
    {
        var category = new Aces(_currentDiceRoll);
        if (category.CalculateScore() != 0)
        {
            _currentScore = category.CalculateScore();
            _currentDiceHandAndCategoryAtTurnEnd = new DiceHandAndCategoryAtTurnEnd(_currentDiceRoll, category);
        }
    }

    private void CheckForYatzy()
    {
        var category = new Yahtzee(_currentDiceRoll);
        if (category.CalculateScore() == 50)
        {
            _currentScore = category.CalculateScore();
            _currentDiceHandAndCategoryAtTurnEnd = new DiceHandAndCategoryAtTurnEnd(_currentDiceRoll, category);
        }
    }

    private void CheckForLargeStraight()
    {
        var category = new LargeStraight(_currentDiceRoll);
        if (category.CalculateScore() == 40)
        {
            _currentScore = category.CalculateScore();
            _currentDiceHandAndCategoryAtTurnEnd = new DiceHandAndCategoryAtTurnEnd(_currentDiceRoll, category);
        }
    }

    private void CheckForSmallStraight()
    {
        var category = new SmallStraight(_currentDiceRoll);
        if (category.CalculateScore() == 30)
        {
            _currentScore = category.CalculateScore();
            _currentDiceHandAndCategoryAtTurnEnd = new DiceHandAndCategoryAtTurnEnd(_currentDiceRoll, category);
        }
    }
    
    private void CheckForFullHouse()
    {
        var category = new FullHouse(_currentDiceRoll);
        if (category.CalculateScore() == 25)
        {
            _currentScore = category.CalculateScore();
            _currentDiceHandAndCategoryAtTurnEnd = new DiceHandAndCategoryAtTurnEnd(_currentDiceRoll, category);
        }
    }
    
    private void CheckForFourOfAKind()
    {
        var category = new FourOfAKind(_currentDiceRoll);
        if (category.CalculateScore() == _currentDiceRoll.Sum())
        {
            _currentScore = category.CalculateScore();
            _currentDiceHandAndCategoryAtTurnEnd = new DiceHandAndCategoryAtTurnEnd(_currentDiceRoll, category);
        }
    }
    
    private void CheckForThreeOfAKind()
    {
        var category = new ThreeOfAKind(_currentDiceRoll);
        if (category.CalculateScore() == _currentDiceRoll.Sum())
        {
            _currentScore = category.CalculateScore();
            _currentDiceHandAndCategoryAtTurnEnd = new DiceHandAndCategoryAtTurnEnd(_currentDiceRoll, category);
        }
    }
}