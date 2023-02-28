using Yatzy_Kata.Data;

namespace Yatzy_Kata.Interfaces;

public interface IScorer
{
    public ScoreAndCategoryAtTurnEnd CalculateScore(List<int> diceRoll, List<Category> categories);
}