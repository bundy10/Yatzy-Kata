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

    public ScoreAndCategoryAtTurnEnd PlayerTurn() => _player.CompleteTurn();
}
