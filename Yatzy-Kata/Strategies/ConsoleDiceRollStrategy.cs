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
        DiceSelection();
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
        DiceSelection();
    }

    private List<int> GetSelectedDicesToReRoll()
    {
        var selectedDices = new List<int>();
        bool keepAdding = true;
        while (keepAdding)
        {
            selectedDices.Add(GetDieSelectedForReRoll());
            _writer.WriteLine(ConsoleMessages.AddAnotherDie);
            keepAdding = (_reader.ReadLine().ToLower() == "y");
        }
        return selectedDices;
    }

    private int GetDieSelectedForReRoll()
    {
        _writer.WriteLine(ConsoleMessages.SelectWhichDieToReRoll);
        int dieSelected;
        while (true)
        {
            if (!int.TryParse(_reader.ReadLine(), out dieSelected))
            {
                _writer.WriteLine(ConsoleMessages.InvalidDieInput);
                continue;
            }

            if (dieSelected < 1 || dieSelected > _diceHand.Count)
            {
                _writer.WriteLine(ConsoleMessages.InvalidDieIndex);
                continue;
            }

            break;
        }

        return dieSelected - 1;
        
    }

    private void DiceSelection()
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