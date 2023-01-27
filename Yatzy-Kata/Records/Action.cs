namespace Yatzy_Kata.Records;

public record Action<T>
{
    public static implicit operator Action<T>(T value) => new Some<T>(value);
}

internal sealed record Some<T>(T Value): Action<T>;

internal sealed record NoActionRecord<T> : Action<T>;