using Yatzy_Kata.Interfaces;
using Yatzy_Kata.Outcomes;

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
        var round = _roundFactory.CreateRound();
        var roundOutcome = round.PlayRound();
        if (roundOutcome is RoundOver) return;
        Winner().Winner = true;
        _players.All(player => player.PlayAgain());
    }

    private IPlayer Winner() => _players.OrderByDescending(player => player.TotalPoints).First();
}