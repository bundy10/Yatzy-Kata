namespace Yatzy_Kata.Data;

public abstract record Category()
{
    public abstract int CalculateScore(List<int> dice);
}

public sealed record Aces() : Category
{
    public override int CalculateScore(List<int> dice) => dice.Where(ones => ones == 1).Sum();
}

public sealed record Twos() : Category
{
    public override int CalculateScore(List<int> dice) => dice.Where(twos => twos == 2).Sum();
}

public sealed record Threes() : Category
{
    public override int CalculateScore(List<int> dice) => dice.Where(threes => threes == 3).Sum();
}

public sealed record Fours() : Category
{
    public override int CalculateScore(List<int> dice) => dice.Where(fours => fours == 4).Sum();
}

public sealed record Fives() : Category
{
    public override int CalculateScore(List<int> dice) => dice.Where(fives => fives == 5).Sum();
}

public sealed record Sixes() : Category
{
    public override int CalculateScore(List<int> dice) => dice.Where(sixes => sixes == 6).Sum();
}

public sealed record ThreeOfAKind() : Category
{
    public override int CalculateScore(List<int> dice)
    {
        return dice.GroupBy(ints => ints).Any(threeOfAKind => threeOfAKind.Count() >= 3) ? dice.Sum() : 0;
    }
}

public sealed record FourOfAKind() : Category
{
    public override int CalculateScore(List<int> dice)
    {
        return dice.GroupBy(ints => ints).Any(fourOfAKind => fourOfAKind.Count() >= 4) ? dice.Sum() : 0;
    }
}

public sealed record FullHouse() : Category
{
    public override int CalculateScore(List<int> dice)
    {
        return dice.GroupBy(ints => ints).Any(pair => pair.Count() == 2) && dice.GroupBy(ints => ints).Any(threeOfAKind => threeOfAKind.Count() == 3) ? 25 : 0;
    }

}

public sealed record SmallStraight() : Category
{
    public override int CalculateScore(List<int> dice)
    {
        HashSet<int> smallStraight1 = new HashSet<int>() { 1, 2, 3, 4 };
        HashSet<int> smallStraight2 = new HashSet<int>() { 2, 3, 4, 5 };
        HashSet<int> smallStraight3 = new HashSet<int>() { 3, 4, 5, 6 };
        if (!smallStraight1.IsSubsetOf(dice) && !smallStraight2.IsSubsetOf(dice) &&
            !smallStraight3.IsSubsetOf(dice)) return 0;
        return 30;
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
