using Yatzy_Kata.Interfaces;

namespace Yatzy_Kata;

public class Game
{
    private readonly IPlayer[] _players;
    private readonly IRoundFactory _roundFactory;

    public Game(IEnumerable<IPlayer> players, IRoundFactory roundFactory)
    {
        _players = players.ToArray();
        _roundFactory = roundFactory;
    }

    public void PlayGame()
    {
        _roundFactory.CreateRound();
        Winner().Winner = true;
        _players.All(player => player.PlayAgain());
    }

    private IPlayer Winner() => _players.OrderByDescending(player => player.TotalPoints).First();
}