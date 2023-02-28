using Yatzy_Kata.Data;
using Yatzy_Kata.Interfaces;

namespace Yatzy_Kata.Strategies;

public class ConsoleDiceRollStrategy : IDiceRollStrategy
{
    private readonly IRandom _randomDiceRoll;
    private List<int> _diceHand = new();
    private readonly IReader _reader;
    private readonly IWriter _writer;
    private int _reRollCount;

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
        DisplayCurrentDiceHand();
        while (_reRollCount < 3 && GetReRollDecision())
        {
            ReRoll();
            _reRollCount += 1;
        }

        _reRollCount = 0;
    }

    private void ReRoll()
    {
        var selectedDices = GetSelectedDicesToReRoll();
        foreach (int dieIndex in selectedDices)
        {
            _diceHand[dieIndex] = _randomDiceRoll.GetDiceRoll();
        }
        DisplayCurrentDiceHand();
    }

    private List<int> GetSelectedDicesToReRoll()
    {
        var selectedDices = new List<int>();

        while (selectedDices.Count == 0)
        {
            _writer.WriteLine(ConsoleMessages.SelectWhichDieToReRoll);
            var dieSelected = _reader.ReadLine();

            foreach (var die in dieSelected)
            { 
                if (int.TryParse(die.ToString(), out var dieIndex) && (dieIndex < 1 || dieIndex > _diceHand.Count))
                {
                    selectedDices.Add(dieIndex - 1);
                }
                else
                {
                    _writer.WriteLine(ConsoleMessages.InvalidDieInput);
                    selectedDices.Clear(); 
                    break;
                }
            }
        }

        return selectedDices;
    }
    
    private void DisplayCurrentDiceHand()
    {
        _writer.WriteLine(ConsoleMessages.DiceHandToString(_diceHand));
    }

    private bool GetReRollDecision()
    {
        _writer.WriteLine(ConsoleMessages.ReRollDecision);
        string input = _reader.ReadLine();

        while (input != "y" && input != "n")
        {
            _writer.WriteLine(ConsoleMessages.InvalidReRollDecision);
            input = _reader.ReadLine();
        }

        return input == "y";
    }
}