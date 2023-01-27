using Yatzy_Kata.Data;
using Yatzy_Kata.Interfaces;

namespace Yatzy_Kata;

public class Turn : ITurn
{
    public DiceHandAndCategoryAtTurn PlayerTurn()
    {
        return new DiceHandAndCategoryAtTurn(new List<int>{1,2,3,4,5,6}, "asd");
    }
}