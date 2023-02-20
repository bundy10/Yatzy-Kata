using System;
using Yatzy_Kata.Factories;
using Yatzy_Kata.Interfaces;

namespace Yatzy_Kata // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var player1 = new Player();
            var player2 = new Player();

            List<IPlayer> players = new List<IPlayer> { player1, player2 };

            var game = new Game(players, new RoundFactory(new TurnFactory()));
            game.PlayGame();
        }
    }
}