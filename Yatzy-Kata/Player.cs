using Yatzy_Kata.Interfaces;

namespace Yatzy_Kata;

public class Player : IPlayer
{
    public int TotalPoints { get; set; }
    public int RoundScore { get; set; }
    public bool Winner { get; set; }
    public bool PlayAgain()
    {
        return false;
    }
    public PlayerRecordHolder RecordHolder { get; set; }

    public Player()
    {
        RecordHolder = new PlayerRecordHolder();
    }
}