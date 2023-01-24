using Yatzy_Kata.Interfaces;

namespace Yatzy_Kata.Outcomes;

public record RoundOutcomes();

public sealed record RoundOver : RoundOutcomes;

public sealed record RoundWinner : RoundOutcomes
{
    public IPlayer Winner;
}