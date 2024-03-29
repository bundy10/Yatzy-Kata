using Yatzy_Kata.Interfaces;

namespace Yatzy_Kata.Data;

public class RandomDiceRoll : IRandom
{
    private readonly Random _random = new();
    
    public List<int> GetDiceHand()
    {
        return new List<int> { _random.Next(1,7), _random.Next(1,7), _random.Next(1,7), _random.Next(1,7), _random.Next(1,7) };
    }

    public int GetDiceRoll()
    {
        return _random.Next(1, 7);
    }
}