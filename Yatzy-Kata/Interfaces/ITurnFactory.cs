namespace Yatzy_Kata.Interfaces;

public interface ITurnFactory
{
    ITurn CreateTurn(IPlayer player);
}