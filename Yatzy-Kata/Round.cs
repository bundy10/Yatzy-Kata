using Yatzy_Kata.Interfaces;
using Yatzy_Kata.Outcomes;

namespace Yatzy_Kata;

public class Round : IRound
{
    private readonly IPlayer[] _players;

    public Round(IEnumerable<IPlayer> players)
    {
        _players = players.ToArray();
    }
    public RoundOutcomes PlayRound()
    {
        return new RoundWinner
        {
            Winner = _players.OrderByDescending(player => player.RoundScore).First()
        };
    }
}