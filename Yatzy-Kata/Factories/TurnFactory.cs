using Yatzy_Kata.Data;
using Yatzy_Kata.Interfaces;

namespace Yatzy_Kata.Factories;

public class TurnFactory : ITurnFactory
{
    private readonly RandomDiceRoll _diceRoll;

    public TurnFactory()
    {
        _diceRoll = new RandomDiceRoll();
    }
    public ITurn CreateTurn(IPlayer player)
    {
        return new Turn(_diceRoll, new Scorer());
    }
}