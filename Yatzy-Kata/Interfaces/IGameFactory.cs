namespace Yatzy_Kata.Interfaces;

public interface IGameFactory
{
    IGame CreateGame(IEnumerable<Player> players);
}