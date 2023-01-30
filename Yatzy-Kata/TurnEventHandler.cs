using Yatzy_Kata.Data;
using Yatzy_Kata.Interfaces;

namespace Yatzy_Kata;

public class TurnEventHandler
{
    private readonly IRandom _randomDiceRoll;

    public TurnEventHandler(IRandom randomDiceRoll)
    {
        _randomDiceRoll = randomDiceRoll;
    }
    public List<int> RollDice()
    {
        return _randomDiceRoll.GetDiceNumbersBetweenRange();
    }
}