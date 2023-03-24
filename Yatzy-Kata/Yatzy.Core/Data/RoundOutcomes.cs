using Yatzy_Kata.Interfaces;

namespace Yatzy_Kata.Outcomes;

public record RoundOutcomes();

public sealed record RoundOver() : RoundOutcomes;
public sealed record RoundTie(int Score, IEnumerable<Player> Players) : RoundOutcomes;
public sealed record RoundWinner(int Score, Player Player) : RoundOutcomes;