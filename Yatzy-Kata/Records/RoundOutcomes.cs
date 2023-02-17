using Yatzy_Kata.Interfaces;

namespace Yatzy_Kata.Outcomes;

public record RoundOutcomes();

public sealed record RoundOver() : RoundOutcomes;
public sealed record RoundTie(int Score, IEnumerable<IPlayer> Players) : RoundOutcomes;
public sealed record RoundWinner(int Score, IPlayer Player) : RoundOutcomes;