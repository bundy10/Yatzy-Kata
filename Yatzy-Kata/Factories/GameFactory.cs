using Yatzy_Kata.Interfaces;

namespace Yatzy_Kata.Factories;

public class GameFactory : IGameFactory
{
    public IGame CreateGame(IEnumerable<Player> players)
    {
        return new Game(players, new RoundFactory(new TurnFactory()));
    }
    
}