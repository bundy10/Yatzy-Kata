using Yatzy_Kata.Interfaces;

namespace Yatzy_Kata;

public class ConsoleReader : IReader
{
    public string ReadLine()
    {
        return Console.ReadLine() ?? string.Empty;
    }
}