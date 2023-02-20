using Yatzy_Kata.Data;
using Yatzy_Kata.Interfaces;

namespace Yatzy_Kata;

public class Turn : ITurn
{
    private readonly TurnEventHandler _turnEventHandler;
    private readonly IScorer _scorer;
    private readonly IPlayer _player;

    public Turn(IRandom randomDiceRoll, IScorer scorer, IPlayer player)
    {
        _player = player;
        _scorer = scorer;
        _turnEventHandler = new TurnEventHandler(randomDiceRoll);
    }
    
    public DiceHandAndCategoryAtTurnEnd PlayerTurn()
    {
        var diceRoll = _turnEventHandler.RollDice();
        return _scorer.CalculateScore(diceRoll, _player.RecordHolder.GetRemainingCategory());
    }
}