namespace Yatzy_Kata.Interfaces;

public interface IRoundFactory
{
    IRound CreateRound(IEnumerable<Player> players);
}