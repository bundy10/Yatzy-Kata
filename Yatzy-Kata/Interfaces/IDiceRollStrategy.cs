namespace Yatzy_Kata.Interfaces;

public interface IDiceRollStrategy
{
    public void RollDice();
    public List<int> GetDiceHand();
}