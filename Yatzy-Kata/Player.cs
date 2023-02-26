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

    public Player(IStrategy strategy)
    {
        Strategy = strategy;
        RecordHolder = new PlayerRecordHolder();
    }

    
}