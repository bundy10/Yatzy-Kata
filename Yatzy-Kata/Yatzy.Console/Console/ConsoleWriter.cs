using Yatzy_Kata.Interfaces;

namespace Yatzy_Kata;

public class ConsoleWriter : IWriter
{
    public void WriteLine(string output)
    {
        Console.WriteLine(output);
    }

    public void ClearOutput()
    {
        Console.Clear(); 
    }
}