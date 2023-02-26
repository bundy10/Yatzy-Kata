using Yatzy_Kata.Data;
using Yatzy_Kata.Interfaces;

namespace Yatzy_Kata;

public class Turn : ITurn
{
    private readonly IPlayer _player;

    public Turn(IPlayer player)
    {
        _player = player;
    }
    
    public DiceHandAndCategoryAtTurnEnd PlayerTurn()
    {
        var diceRoll = _player.DiceRollStrategy.RollDice();
        
        return _player.Strategy.CalculateScore(diceRoll, _player.RecordHolder.GetRemainingCategory());
    }
}
