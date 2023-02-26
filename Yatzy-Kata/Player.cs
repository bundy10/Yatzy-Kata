using Yatzy_Kata.Interfaces;

namespace Yatzy_Kata;

public class Player : IPlayer
{
    public bool Winner { get; set; }
    public bool PlayAgain()
    {
        return true;
    }
    public IPlayerRecordHolder RecordHolder { get; set; }
    public IStrategy Strategy { get; set; }
    public IDiceRollStrategy DiceRollStrategy { get; set; }

    public Player(IStrategy strategy, IDiceRollStrategy diceRollStrategy)
    {
        Strategy = strategy;
        RecordHolder = new PlayerRecordHolder();
        DiceRollStrategy = diceRollStrategy;
    }

    
}