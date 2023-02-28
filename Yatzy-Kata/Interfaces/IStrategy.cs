using Yatzy_Kata.Data;

namespace Yatzy_Kata.Interfaces;

public interface IStrategy
{
    ScoreAndCategoryAtTurnEnd CalculateScore(List<int> diceRoll, List<Category> remainingCategories);

    public void SelectCategoryStrategy(List<Category> remainingCategories);

    public ScoreAndCategoryAtTurnEnd GetScoreAndCategoryAtTurnEnd(List<Category> remainingCategories);
    
    

}