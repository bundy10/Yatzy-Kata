using Yatzy_Kata.Interfaces;
using Yatzy_Kata.Outcomes;
using Yatzy_Kata.Records;

namespace Yatzy_Kata;

public class Game
{
    private readonly IEnumerable<Player> _players;
    private readonly IRoundFactory _roundFactory;
    private int _roundCount;
    private readonly ConsoleWriter _writer;
    private RoundOutcomes? _roundResult;

    public Game(IEnumerable<Player> players, IRoundFactory roundFactory)
    {
        _players = players;
        _roundFactory = roundFactory;
        _roundCount = 0;
        _writer = new ConsoleWriter();
    }

    public void PlayGame()
    {
        _writer.WriteLine(ConsoleMessages.Welcome);
        while (!PreviousRoundAbandoned && AreAnyCategoriesLeft())
        {
            IncrementRoundCount();
            PlayRound();
            PrintRoundResult();
            PlayersDecisionToAbandonGame();

        }
        PrintFinalResults();
    }

    private void PlayRound()
    {
        _writer.WriteLine(ConsoleMessages.RoundCount(_roundCount));
        var round = _roundFactory.CreateRound(_players);
        round.PlayTurns();
        _roundResult = round.GetRoundResult();
    }

    private void PlayersDecisionToAbandonGame()
    {
        foreach (var player in _players)
        {
            player.AbandonGameOrNot();
            if (!player.AbandonedGame()) continue;
            _roundResult = new RoundOver();
            PrintRoundResult();
        }
    }

    private void PrintRoundResult()
    {
        _writer.WriteLine(ConsoleMessages.RoundResults(_roundResult));
    }
    
    private void PrintFinalResults()
    {
        if (PreviousRoundAbandoned) return;
        var winner = PlayerWithHighestScoreAtEndOfGame();
        _writer.WriteLine(ConsoleMessages.Winner(winner));
        _writer.WriteLine(ConsoleMessages.Farewell);
    }
    private bool PreviousRoundAbandoned => _roundResult == new RoundOver();
    private Player PlayerWithHighestScoreAtEndOfGame() => _players.OrderByDescending(player => player.PlayerTotalPoints()).First();
    private bool AreAnyCategoriesLeft() =>
        _players.Any(players => players.GetRemainingCategories().Count != 0);
    private void IncrementRoundCount() => _roundCount += 1;
}