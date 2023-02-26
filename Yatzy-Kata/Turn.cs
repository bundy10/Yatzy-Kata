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
        _player.Strategy.DiceRollStrategy.RollDice();
        var diceHand = _player.Strategy.DiceRollStrategy.GetDiceHand();
        return _player.Strategy.CalculateScore(diceHand, _player.RecordHolder.GetRemainingCategory());
    }
}
