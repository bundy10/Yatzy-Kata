using System;
using Yatzy_Kata.Factories;
using Yatzy_Kata.Interfaces;
using Yatzy_Kata.Strategies;

namespace Yatzy_Kata 
{
    public static class Program
    {
        static void Main(string[] args)
        {
            var player1 = new Player("bundy", new ConsoleUserStrategy(new ConsoleReader(), new ConsoleWriter()));
            var player2 = new Player("james", new ComputerStrategy());

            List<Player> players = new List<Player> { player1, player2 };

            var game = new Game(players, new RoundFactory(new TurnFactory()));
            game.PlayGame();
        }
    }
}