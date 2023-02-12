using Yatzy_Kata.Data;

namespace Yatzy_Kata.Interfaces;

public interface IScorer
{
    public DiceHandAndCategoryAtTurnEnd CalculateScore(List<int> diceRoll);
}