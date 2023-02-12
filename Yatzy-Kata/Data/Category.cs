namespace Yatzy_Kata.Data;

public abstract record Category()
{
    public abstract int CalculateScore(List<int> dice);
}

internal sealed record Aces() : Category
{
    public override int CalculateScore(List<int> dice) => dice.Where(ones => ones == 1).Sum();
}

internal sealed record Twos() : Category
{
    public override int CalculateScore(List<int> dice) => dice.Where(twos => twos == 2).Sum();
}

internal sealed record Threes() : Category
{
    public override int CalculateScore(List<int> dice) => dice.Where(threes => threes == 3).Sum();
}

internal sealed record Fours() : Category
{
    public override int CalculateScore(List<int> dice) => dice.Where(fours => fours == 4).Sum();
}

internal sealed record Fives() : Category
{
    public override int CalculateScore(List<int> dice) => dice.Where(fives => fives == 5).Sum();
}

internal sealed record Sixes() : Category
{
    public override int CalculateScore(List<int> dice) => dice.Where(sixes => sixes == 6).Sum();
}

internal sealed record ThreeOfAKind() : Category
{
    public override int CalculateScore(List<int> dice)
    {
        return dice.GroupBy(ints => ints).Any(threeOfAKind => threeOfAKind.Count() == 3) ? dice.Sum() : 0;
    }
}

internal sealed record FourOfAKind() : Category
{
    public override int CalculateScore(List<int> dice)
    {
        return dice.GroupBy(ints => ints).Any(fourOfAKind => fourOfAKind.Count() == 4) ? dice.Sum() : 0;
    }
}

internal sealed record FullHouse() : Category
{
    public override int CalculateScore(List<int> dice)
    {
        return dice.GroupBy(ints => ints).Any(threeOfAKind => threeOfAKind.Count() == 3 && threeOfAKind.Count() == 2) ? 25 : 0;
    }
}

internal sealed record SmallStraight() : Category
{
    public override int CalculateScore(List<int> dice)
    {
        return dice.SequenceEqual(Enumerable.Range(dice[0], 4)) ? 30 : 0;
    }
}

public sealed record LargeStraight() : Category
{
    public override int CalculateScore(List<int> dice)
    {
        return dice.SequenceEqual(Enumerable.Range(dice[0], 5)) ? 40 : 0;
    }
}

public sealed record Yahtzee() : Category
{
    public override int CalculateScore(List<int> dice)
    {
        return dice.GroupBy(ints => ints).Any(yahtzee => yahtzee.Count() == 5) ? 50 : 0;
    }
}

public sealed record Chance() : Category
{
    public override int CalculateScore(List<int> dice)
    {
        return dice.Sum();
    }
}
