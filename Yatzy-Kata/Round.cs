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
        GetTurnResults();
        return RoundWinner();
    }

    public void GetTurnResults()
    {
        foreach (var player in _players)
        {
            var turn = _turnFactory.CreateTurn(player);
            var playerTurnResult = turn.PlayerTurn();
            player.RecordHolder.SetRoundScore(playerTurnResult.Category.CalculateScore(playerTurnResult.Dice));
            player.RecordHolder.SetTotalPoints(playerTurnResult.Category.CalculateScore(playerTurnResult.Dice));
            player.RecordHolder.RemoveUsedCategory(playerTurnResult.Category);
        }
    }

    private RoundOutcomes RoundWinner()
    {
        var highestScore = _players.Max(player => player.RecordHolder.GetRoundScore());
        var playersWithHighestScore = _players.Where(players => players.RecordHolder.GetRoundScore() == highestScore);
        var highestScoringPlayers = playersWithHighestScore.ToList();
        
        if (highestScoringPlayers.Count == 1)
        {
            return new RoundWinner(highestScore, highestScoringPlayers.Single() );
        }

        if  (highestScoringPlayers.Count > 1)
        {
            return new RoundTie(highestScore, highestScoringPlayers);
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