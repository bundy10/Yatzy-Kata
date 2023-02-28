using Yatzy_Kata.Data;
using Yatzy_Kata.Interfaces;

namespace Yatzy_Kata.Strategies;

public class ConsoleUserStrategy : IStrategy
{
    public IDiceRollStrategy DiceRollStrategy { get; set; }
    public ConsoleUserStrategy()
    {
        DiceRollStrategy = new ConsoleDiceRollStrategy(new ConsoleReader(), new ConsoleWriter());
    }

    public ScoreAndCategoryAtTurnEnd CalculateScore(List<int> diceRoll, List<Category> remainingCategories)
    {
        return new ScoreAndCategoryAtTurnEnd(new Aces().CalculateScore(diceRoll), new Aces());
    }

    public void SelectCategoryStrategy(List<Category> remainingCategories)
    {
        
    }

    public ScoreAndCategoryAtTurnEnd GetScoreAndCategoryAtTurnEnd(List<Category> remainingCategories)
    {
        return new ScoreAndCategoryAtTurnEnd(32, new Aces());
    }
}