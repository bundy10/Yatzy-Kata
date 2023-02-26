using Yatzy_Kata.Data;
using Yatzy_Kata.Interfaces;

namespace Yatzy_Kata.Strategies;

public class ConsoleUserStrategy : IStrategy
{
    public DiceHandAndCategoryAtTurnEnd CalculateScore(List<int> diceRoll, List<Category> remainingCategories)
    {
        return new DiceHandAndCategoryAtTurnEnd(diceRoll, new Aces());
    }

    public void Strategy(List<Category> remainingCategories)
    {
        
    }
}