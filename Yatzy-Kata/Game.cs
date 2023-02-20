using Yatzy_Kata.Interfaces;
using Yatzy_Kata.Outcomes;
using Yatzy_Kata.Records;

namespace Yatzy_Kata;

public class Game : IGame
{
    private readonly IEnumerable<IPlayer> _players;
    private readonly IRoundFactory _roundFactory;
    private Records.Action<RoundOutcomes> _actionResult;

    private bool PreviousRoundAbandoned => _actionResult is Some<RoundOutcomes>(RoundOver _);


    public Game(IEnumerable<IPlayer> players, IRoundFactory roundFactory)
    {
        _players = players;
        _roundFactory = roundFactory;
        _actionResult = new NoActionRecord<RoundOutcomes>();
    }

    public void PlayGame()
    {
        do PlayRound();
        while (ShouldPlayAnotherRound());
        Winner().Winner = true;
    }
    
    private void PlayRound()
    {
        var round = _roundFactory.CreateRound(_players);
        var roundOutcome = round.PlayRound();
        _actionResult = new Some<RoundOutcomes>(roundOutcome);
    }

    private bool DoAllPlayersWantToPlayAgain() => _players.All(player => player.PlayAgain());

    private bool ShouldPlayAnotherRound() => !PreviousRoundAbandoned && DoAllPlayersWantToPlayAgain();

    private IPlayer Winner() => _players.OrderByDescending(player => player.RecordHolder.GetTotalPoints()).First();
    
    
}