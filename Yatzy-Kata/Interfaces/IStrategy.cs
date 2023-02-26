using Yatzy_Kata.Data;

namespace Yatzy_Kata.Interfaces;

public interface IStrategy
{
    DiceHandAndCategoryAtTurnEnd CalculateScore(List<int> diceRoll, List<Category> remainingCategories);

    public void Strategy(List<Category> remainingCategories);
    
    public IDiceRollStrategy DiceRollStrategy { get; set; }

}