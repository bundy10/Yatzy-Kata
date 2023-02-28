using Yatzy_Kata.Interfaces;

namespace Yatzy_Kata.Factories;

public class RoundFactory : IRoundFactory
{
    private readonly ITurnFactory _turnFactory;

    public RoundFactory(ITurnFactory turnFactory)
    {
        _turnFactory = turnFactory;
        
    }
    public IRound CreateRound(IEnumerable<Player> players)
    {
        return new Round(players, _turnFactory);
    }
}