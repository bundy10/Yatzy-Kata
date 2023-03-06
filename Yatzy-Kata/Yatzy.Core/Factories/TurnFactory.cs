using Yatzy_Kata.Data;
using Yatzy_Kata.Interfaces;

namespace Yatzy_Kata.Factories;

public class TurnFactory : ITurnFactory
{
    public ITurn CreateTurn(Player player)
    {
        return new Turn(player);
    }
}