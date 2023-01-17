namespace Yatzy_Kata.Interfaces;

public interface IPlayer
{
    int TotalPoints { get; set; }
    bool Winner { get; set; }
    bool PlayAgain();
}