using System.Diagnostics;
using Yatzy_Kata.Data;
using Yatzy_Kata.Interfaces;

namespace Yatzy_Kata;

public class Turn : ITurn
{
    private readonly Player _player;

    public Turn(Player player)
    {
        _player = player;
    }

    public void PlayerTurn()
    {
        var stopwatch = new Stopwatch();
        stopwatch.Start();
        _player.CompleteTurn();
        stopwatch.Stop();
        var timeSpentInTurn = (int)Math.Round(stopwatch.Elapsed.TotalSeconds);
        _player.UpdateRecordsAfterTurn(timeSpentInTurn);
    }
}