namespace Yatzy_Kata.Interfaces;

public interface ITurnFactory
{
    ITurn CreateTurn(Player player);
}