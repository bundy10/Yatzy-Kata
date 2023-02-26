namespace Yatzy_Kata.Interfaces;

public interface IRandom
{
    public List<int> GetDiceNumbersBetweenRange();
    public int GetDiceRoll();
}