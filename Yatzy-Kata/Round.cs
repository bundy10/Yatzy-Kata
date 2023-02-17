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

    public IEnumerable<DiceHandAndCategoryAtTurnEnd> GetTurnResults()
    {
        var turnResults = new List<DiceHandAndCategoryAtTurnEnd>();
        foreach (var player in _players)
        {
            var turn = _turnFactory.CreateTurn(player);
            var playerTurnResult = turn.PlayerTurn();
            turnResults.Add(playerTurnResult);
        }
        return turnResults;
    }

    private RoundOutcomes RoundWinner()
    {
        var highestScore = _players.Max(player => player.RoundScore);
        var playersWithHighestScore = _players.Where(players => players.RoundScore == highestScore);

        var highestScoringPlayers = playersWithHighestScore.ToList();
        if (highestScoringPlayers.Count == 1)
        {
            return new RoundWinner(highestScoringPlayers.Single().RoundScore, highestScoringPlayers.Single() );
        }

        if (highestScoringPlayers.Count > 1)
        {
            return new RoundTie(highestScoringPlayers.Single().RoundScore, highestScoringPlayers);
        }
        
        return new RoundOver();
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