using Yatzy_Kata.Data;
using Yatzy_Kata.Interfaces;
using Yatzy_Kata.Outcomes;

namespace Yatzy_Kata;

public class Round : IRound
{
    private readonly ITurnFactory _turnFactory;
    private readonly Player[] _players;

    public Round(IEnumerable<Player> players, ITurnFactory turnFactory)
    {
        _turnFactory = turnFactory;
        _players = players.ToArray();
    }
    
    public void PlayTurns()
    {
        foreach (var player in _players)
        {
            var turn = _turnFactory.CreateTurn(player);
            turn.PlayerTurn();
        }
    }
    
    public RoundOutcomes GetRoundResult()
    {
        var highestScore = _players.Max(player => player.PlayerRoundScore());
        var playersWithHighestScore = _players.Where(players => players.PlayerRoundScore() == highestScore);
        var highestScoringPlayers = playersWithHighestScore.ToList();

        return highestScoringPlayers.Count switch
        {
            1 => new RoundWinner(highestScore, highestScoringPlayers.Single()),
            > 1 => new RoundTie(highestScore, highestScoringPlayers),
            _ => new RoundOver()
        };
    }
}