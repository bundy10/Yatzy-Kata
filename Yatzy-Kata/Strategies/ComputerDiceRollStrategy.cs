using Yatzy_Kata.Data;
using Yatzy_Kata.Interfaces;

namespace Yatzy_Kata.Strategies;

public class ComputerDiceRollStrategy : IDiceRollStrategy
{
    private readonly IRandom _randomDiceRoll;
    private List<int> _diceHand = new();

    public ComputerDiceRollStrategy(IRandom randomDiceRoll)
    {
        _randomDiceRoll = randomDiceRoll;
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