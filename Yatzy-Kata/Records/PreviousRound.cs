namespace Yatzy_Kata.Records;

public record PreviousRound();

internal sealed record Some<T>(T Value): PreviousRound;

internal sealed record NoPreviousRoundRecord : PreviousRound;