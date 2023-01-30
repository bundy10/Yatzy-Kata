using Yatzy_Kata.Data;
using Yatzy_Kata.Interfaces;

namespace Yatzy_Kata;

public class Turn : ITurn
{
    private readonly TurnEventHandler _turnEventHandler;

    public Turn(IRandom randomDiceRoll)
    {
        _turnEventHandler = new TurnEventHandler(randomDiceRoll);
    }
    
    public DiceHandAndCategoryAtTurn PlayerTurn()
    {
        return new DiceHandAndCategoryAtTurn(_turnEventHandler.RollDice(), "asd");
    }
}