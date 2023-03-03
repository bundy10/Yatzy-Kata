using Yatzy_Kata.Data;
using Yatzy_Kata.Interfaces;

namespace Yatzy_Kata;

public class Player
{
    public readonly string Name;
    private readonly IStrategy _strategy;
    private readonly PlayerRecordHolder _recordHolder;
    private TurnResults? _turnResults;
    
    public Player(string name, IStrategy strategy)
    {
        Name = name;
        _strategy = strategy;
        _recordHolder = new PlayerRecordHolder();
    }

    public void UpdateRecordsAfterTurn(int timeSpentInTurn)
    {
        if (_turnResults == null) return;
        _recordHolder.SetRoundScore(_turnResults.Score);
        _recordHolder.AddToTotalPoints(_turnResults.Score);
        _recordHolder.RemoveUsedCategory(_turnResults.Category);
        _recordHolder.AddToTimeSpentInTurn(timeSpentInTurn);
    }

    public void CompleteTurn()
    {
        _turnResults = _strategy.GetTurnResults(_recordHolder.GetRemainingCategories());
    }

    public void AbandonGameOrNot()
    {
        _strategy.DoesPlayerWantToAbandonGame();
    }
    
    public bool AbandonedGame() => _strategy.GetAbandonChoice();
    public int PlayerRoundScore() => _recordHolder.GetRoundScore();
    public int PlayerTotalPoints() => _recordHolder.GetTotalPoints();
    public List<Category> GetRemainingCategories() => _recordHolder.GetRemainingCategories();
}