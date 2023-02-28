using Yatzy_Kata.Data;
using Yatzy_Kata.Interfaces;

namespace Yatzy_Kata;

public class PlayerRecordHolder : IPlayerRecordHolder
{
    private int _totalPoints;
    private int _roundScore;
    private List<Category> _remainingCategorys;

    public PlayerRecordHolder()
    {
        _roundScore = 0;
        _totalPoints = 0;
        _remainingCategorys = new List<Category>() { new Aces(), new Twos(), new Threes(), new Fours(), new Fives(), new Sixes(), new Chance(), new ThreeOfAKind(), new FourOfAKind(), new FullHouse(), new SmallStraight(), new LargeStraight(), new Yahtzee()};
    }

    public List<Category> GetRemainingCategories()
    {
        return _remainingCategorys;
    }

    public void RemoveUsedCategory(Category categoryToRemove)
    {
        _remainingCategorys.Remove(categoryToRemove);
    }

    public void SetRoundScore(int roundScore)
    {
        _roundScore = roundScore;
    }

    public void AddToTotalPoints(int roundScore)
    {
        _totalPoints += roundScore;
    }
    
    public int GetRoundScore()
    {
        return _roundScore;
    }

    public int GetTotalPoints()
    {
        return _totalPoints;
    }
}

