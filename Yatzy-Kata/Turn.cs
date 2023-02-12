using Yatzy_Kata.Data;
using Yatzy_Kata.Interfaces;

namespace Yatzy_Kata;

public class Turn : ITurn
{
    private readonly TurnEventHandler _turnEventHandler;
    private readonly IScorer _scorer;

    public Turn(IRandom randomDiceRoll, IScorer scorer)
    {
        _scorer = scorer;
        _turnEventHandler = new TurnEventHandler(randomDiceRoll);
    }
    
    public DiceHandAndCategoryAtTurnEnd PlayerTurn()
    {
        var diceRoll = _turnEventHandler.RollDice();
        return _scorer.CalculateScore(diceRoll);
    }
}