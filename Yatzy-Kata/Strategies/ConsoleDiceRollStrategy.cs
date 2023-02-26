using Yatzy_Kata.Data;
using Yatzy_Kata.Interfaces;

namespace Yatzy_Kata.Strategies;

public class ConsoleDiceRollStrategy : IDiceRollStrategy
{
    private readonly IRandom _randomDiceRoll;
    private List<int> _diceHand = new();
    private readonly IReader _reader;
    private readonly IWriter _writer;

    public ConsoleDiceRollStrategy(IReader reader, IWriter writer)
    {
        _reader = reader;
        _writer = writer;
        _randomDiceRoll = new RandomDiceRoll();
    }
    public List<int> GetDiceHand()
    {
        return _diceHand;
    }
    public void RollDice()
    {
        _diceHand = _randomDiceRoll.GetDiceNumbersBetweenRange();
        GetDiceSelection();
        
    }

    private void ReRoll()
    {
        
    }

    private void SelectWhichDieToReRoll()
    {
        
    }

    private void GetDiceSelection()
    {
        _writer.WriteLine(ConsoleMessages.DiceHandToString(_diceHand));
    }
}