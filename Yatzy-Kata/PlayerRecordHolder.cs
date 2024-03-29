using Yatzy_Kata.Data;
using Yatzy_Kata.Interfaces;

namespace Yatzy_Kata;

public class PlayerRecordHolder
{
    private int _totalPoints;
    private int _roundScore;
    private readonly List<Category> _remainingCategories;
    private int _timeSpentInTurn;

    public PlayerRecordHolder()
    {
        _roundScore = 0;
        _totalPoints = 0;
        _remainingCategories = new List<Category>() { new Aces(), new Twos(), new Threes(), new Fours(), new Fives(), new Sixes(), new Chance(), new ThreeOfAKind(), new FourOfAKind(), new FullHouse(), new SmallStraight(), new LargeStraight(), new Yahtzee()};
    }

    public List<Category> GetRemainingCategories() => _remainingCategories;
    public void RemoveUsedCategory(Category categoryToRemove) => _remainingCategories.Remove(categoryToRemove);
    public void SetRoundScore(int roundScore) => _roundScore = roundScore;
    public void AddToTotalPoints(int roundScore) => _totalPoints += roundScore;
    public int GetRoundScore() => _roundScore;
    public int GetTotalPoints() => _totalPoints;
    public void AddToTimeSpentInTurn(int timeSpentInTurn) => _timeSpentInTurn += timeSpentInTurn;
    public int GetTimeSpentInTurns() => _timeSpentInTurn;
}

