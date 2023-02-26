using Yatzy_Kata.Data;
using Yatzy_Kata.Interfaces;

namespace Yatzy_Kata.Strategies;

public class ComputerDiceRollStrategy : IDiceRollStrategy
{
    private readonly IRandom _randomDiceRoll;
    private List<int> _diceHand = new();

    public ComputerDiceRollStrategy()
    {
        _randomDiceRoll = new RandomDiceRoll();
    }
    public void RollDice()
    {
        _diceHand = _randomDiceRoll.GetDiceNumbersBetweenRange();
    }

    public List<int> GetDiceHand()
    {
        return _diceHand;
    }
}