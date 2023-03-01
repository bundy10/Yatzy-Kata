using Yatzy_Kata.Data;

namespace Yatzy_Kata.Interfaces;

public interface IStrategy
{
    ScoreAndCategoryAtTurnEnd CalculateScore(List<int> diceRoll, List<Category> remainingCategories);

    public Category SelectCategoryStrategy(List<Category> remainingCategories);

    public ScoreAndCategoryAtTurnEnd GetScoreAndCategoryAtTurnEnd(List<Category> remainingCategories);
    
    bool GetAbandonChoice();
    void DoesPlayerWantToAbandonGame();
}