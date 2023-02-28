using System;
using Yatzy_Kata.Factories;
using Yatzy_Kata.Interfaces;
using Yatzy_Kata.Strategies;

namespace Yatzy_Kata // Note: actual namespace depends on the project name.
{
    public static class Program
    {
        static void Main(string[] args)
        {
            var player1 = new Player(new ConsoleUserStrategy());
            var player2 = new Player(new ComputerStrategy());

            List<Player> players = new List<Player> { player1, player2 };

            var game = new Game(players, new RoundFactory(new TurnFactory()));
            game.PlayGame();
        }
    }
}