using Yatzy_Kata.Interfaces;

namespace Yatzy_Kata;

public class Game
{
    private readonly IPlayer[] _players;

    public Game(IEnumerable<IPlayer> players)
    {
        _players = players.ToArray();
    }

    public void PlayGame()
    {   
        Winner().Winner = true;
        _players.All(player => player.PlayAgain());
    }

    private IPlayer Winner() => _players.OrderByDescending(player => player.TotalPoints).First();
}