using Yatzy_Kata.Data;

namespace Yatzy_Kata.Interfaces;

public interface ITurn
{
    DiceHandAndCategoryAtTurnEnd PlayerTurn();
}