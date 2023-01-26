using Yatzy_Kata.Interfaces;
using Yatzy_Kata.Outcomes;
using Yatzy_Kata.Records;

namespace Yatzy_Kata;

public class Game
{
    private readonly IPlayer[] _players;
    private readonly IRoundFactory _roundFactory;
    private PreviousRound _previousRoundResult;

    private bool PreviousRoundAbandoned => _previousRoundResult is Some<RoundOutcomes>(RoundOver _);


    public Game(IEnumerable<IPlayer> players, IRoundFactory roundFactory)
    {
        _players = players.ToArray();
        _roundFactory = roundFactory;
        _previousRoundResult = new NoPreviousRoundRecord();
    }

    public void PlayGame()
    {
        do PlayRound();
        while (ShouldPlayAnotherRound());
        Winner().Winner = true;
    }
    
    private void PlayRound()
    {
        var round = _roundFactory.CreateRound();
        var roundOutcome = round.PlayRound();
        _previousRoundResult = new Some<RoundOutcomes>(roundOutcome);
    }

    private bool DoAllPlayersWantToPlayAgain() => _players.All(player => player.PlayAgain());

    private bool ShouldPlayAnotherRound() => !PreviousRoundAbandoned && DoAllPlayersWantToPlayAgain();

    private IPlayer Winner() => _players.OrderByDescending(player => player.TotalPoints).First();
    
    
}