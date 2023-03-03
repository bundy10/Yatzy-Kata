using Yatzy_Kata.Data;

namespace Yatzy_Kata.Interfaces;

public interface IStrategy
{
    TurnResults CalculateScore(List<int> diceRoll, List<Category> remainingCategories);

    public Category SelectCategoryStrategy(List<Category> remainingCategories);

    public TurnResults GetTurnResults(List<Category> remainingCategories);
    
    bool GetAbandonChoice();
    void DoesPlayerWantToAbandonGame();
}