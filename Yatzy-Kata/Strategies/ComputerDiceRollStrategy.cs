using Yatzy_Kata.Data;
using Yatzy_Kata.Interfaces;

namespace Yatzy_Kata.Strategies;

public class ComputerDiceRollStrategy : IDiceRollStrategy
{
    private readonly IRandom _randomDiceRoll;

    public ComputerDiceRollStrategy()
    {
        _randomDiceRoll = new RandomDiceRoll();
    }
    public List<int> RollDice()
    {
        return _randomDiceRoll.GetDiceNumbersBetweenRange();
    }
}