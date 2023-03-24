using System;
using Yatzy_Kata.Data;
using Yatzy_Kata.Factories;
using Yatzy_Kata.Interfaces;
using Yatzy_Kata.Strategies;

namespace Yatzy_Kata 
{
    public static class Program
    {
        static void Main(string[] args)
        {
            var player1 = new Player("bundy", new ComputerStrategy(new RandomDiceRoll()));
            var player2 = new Player("james", new ComputerStrategy(new RandomDiceRoll()));
            var player3 = new Player("bundy", new ConsoleUserStrategy(new ConsoleReader(), new ConsoleWriter(), new RandomDiceRoll()));

            List<Player> players = new List<Player> { player1, player2, player3 };

            var game = new Game(players, new RoundFactory(new TurnFactory()));
            game.PlayGame();
        }
    }
}