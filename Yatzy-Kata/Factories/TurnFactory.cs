using Yatzy_Kata.Interfaces;

namespace Yatzy_Kata.Factories;

public class TurnFactory : ITurnFactory
{
    public ITurn CreateTurn(IPlayer player)
    {
        return new Turn();
    }
}