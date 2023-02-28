using Yatzy_Kata.Data;
using Yatzy_Kata.Interfaces;

namespace Yatzy_Kata;

public class Player
{
    private readonly IStrategy _strategy;
    private readonly PlayerRecordHolder _recordHolder;
    public bool Winner { get; set; }
    public bool PlayAgain()
    {
        return true;
    }

    public Player(IStrategy strategy)
    {
        _strategy = strategy;
        _recordHolder = new PlayerRecordHolder();
    }

    public void UpdateRecordsAfterTurn(ScoreAndCategoryAtTurnEnd turnResult)
    {
        _recordHolder.SetRoundScore(turnResult.Score);
        _recordHolder.AddToTotalPoints(turnResult.Score);
        _recordHolder.RemoveUsedCategory(turnResult.Category);
    }

    public ScoreAndCategoryAtTurnEnd CompleteTurn()
    {
        return _strategy.GetScoreAndCategoryAtTurnEnd(_recordHolder.GetRemainingCategories());
    }

    public int GetRoundScore() => _recordHolder.GetRoundScore();
    public int GetTotalPoints() => _recordHolder.GetTotalPoints();

    public List<Category> GetRemainingCategories() => _recordHolder.GetRemainingCategories();
}