using Yatzy_Kata.Data;

namespace Yatzy_Kata.Interfaces;

public interface IStrategy
{
    void CalculateTurnResults(List<Category> remainingCategories);
    public TurnResults? GetTurnResults();
    bool GetAbandonChoice();
    void DoesPlayerWantToAbandonGame();
}