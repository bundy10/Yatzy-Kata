using Yatzy_Kata.Data;
using Yatzy_Kata.Interfaces;

namespace Yatzy_Kata.Strategies;

public class ConsoleDiceRollStrategy : IDiceRollStrategy
{
    private readonly IRandom _randomDiceRoll;
    private List<int>? _diceHand;
    private readonly IReader _reader;
    private readonly IWriter _writer;

    public ConsoleDiceRollStrategy(IReader reader, IWriter writer)
    {
        _reader = reader;
        _writer = writer;
        _randomDiceRoll = new RandomDiceRoll();
    }
    public List<int> RollDice()
    {
        _diceHand = _randomDiceRoll.GetDiceNumbersBetweenRange();
        return _diceHand;
    }

    private void ReRoll()
    {
        
    }

    private void SelectWhichDieToReRoll()
    {
        
    }
}