namespace Yatzy_Kata.Interfaces;

public interface IRandom
{
    public List<int> GetDiceHand();
    public int GetDiceRoll();
}