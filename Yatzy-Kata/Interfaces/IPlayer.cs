using Yatzy_Kata.Data;

namespace Yatzy_Kata.Interfaces;

public interface IPlayer
{
    
    int TotalPoints { get; set; }
    int RoundScore { get; set; }
    bool Winner { get; set; }
    bool PlayAgain();
    
}