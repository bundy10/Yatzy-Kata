using Yatzy_Kata.Data;
using Yatzy_Kata.Interfaces;
using Yatzy_Kata.Outcomes;

namespace Yatzy_Kata;

public class Round : IRound
{
    private readonly ITurnFactory _turnFactory;
    private readonly IPlayer[] _players;
    private int _roundCount = 0;

    public Round(IEnumerable<IPlayer> players, ITurnFactory turnFactory)
    {
        _turnFactory = turnFactory;
        _players = players.ToArray();
    }
    public RoundOutcomes PlayRound()
    {
        IncrementRound();
        var turnResult = GetTurnResults();
        return RoundWinner();
    }

    public DiceHandAndCategoryAtTurn GetTurnResults()
    {
        var turn = _turnFactory.CreateTurn(_players[0]);
        return turn.PlayerTurn();
    }

    private RoundOutcomes RoundWinner()
    {
        return new RoundWinner
        {
            Winner = _players.OrderByDescending(player => player.RoundScore).First()
        };
    }

    private void IncrementRound()
    {
        _roundCount += 1;
    }

    public int GetRoundCount()
    {
        return _roundCount;
    }

}