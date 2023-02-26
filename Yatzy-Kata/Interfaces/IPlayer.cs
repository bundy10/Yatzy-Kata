using Yatzy_Kata.Data;

namespace Yatzy_Kata.Interfaces;

public interface IPlayer
{
    bool Winner { get; set; }
    bool PlayAgain();
    IPlayerRecordHolder RecordHolder { get; set; }
    
    IStrategy Strategy { get; set; }
}