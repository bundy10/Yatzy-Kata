using Moq;
using Yatzy_Kata;
using Yatzy_Kata.Data;
using Yatzy_Kata.Factories;
using Yatzy_Kata.Interfaces;
using Yatzy_Kata.Outcomes;
using Yatzy_Kata.Strategies;

namespace Yatzy_Test;


public class GameTests
{
    private readonly List<Player> _players;
    private readonly Game _game;
    private readonly Mock<IRandom> _mockPlayer1RandomDieRoll;
    private readonly Mock<IRandom> _mockPlayer2RandomDieRoll;
    private readonly Mock<IReader> _mockReader;

    public GameTests()
    {
        _mockReader = new Mock<IReader>();
        _mockPlayer1RandomDieRoll = new Mock<IRandom>();
        _mockPlayer2RandomDieRoll = new Mock<IRandom>();
        _mockPlayer1RandomDieRoll.Setup(diceHand => diceHand.GetDiceHand()).Returns(new List<int> { 5, 5, 5, 5, 5 });
        _mockPlayer2RandomDieRoll.Setup(diceHand => diceHand.GetDiceHand()).Returns(new List<int> { 4, 4, 2, 3, 3 });
        _players = new List<Player> { new Player("bundy", new ComputerStrategy(_mockPlayer1RandomDieRoll.Object)), new Player("john", new ComputerStrategy(_mockPlayer2RandomDieRoll.Object)) };
        _game = new Game(_players, new RoundFactory(new TurnFactory()));
        
    }
    
    [Fact]
    public void GivenPlayGameIsCalled_WhileEveryPlayerPLaysTillNoCategoriesLeft_ThenAMaximumOfThirteenRoundsArePlayed()
    {
        //Arrange
        var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);

        //Act
        _game.PlayGame();
        
        //Assert
        Assert.Contains(ConsoleMessages.RoundCount(13), stringWriter.ToString());
        Assert.DoesNotContain(ConsoleMessages.RoundCount(14), stringWriter.ToString());
    }
    
    [Fact]
    public void GivenPlayGameIsCalled_ThenGamePromptsWelcomeMessage()
    {
        //Arrange
        var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);

        //Act
        _game.PlayGame();
        
        //Assert
        Assert.Contains(ConsoleMessages.Welcome, stringWriter.ToString());
    }

    [Fact]
    public void GivenPlayGameIsCalled_WhileEveryPlayerPLaysTillNoCategoriesLeft_ThenThePlayerWithTheMostPointsWillBePromptedAtTheEndOfTheGame()
    {
        //Arrange
        var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);

        //Act
        _game.PlayGame();
        
        //Assert
        Assert.Contains(ConsoleMessages.Winner(_players[0]), stringWriter.ToString());
    }
    
    [Fact]
    public void GivenPlayGameIsCalled_WhileEveryPlayerPLaysTillNoCategoriesLeft_ThenPLayersWillHaveNoCategoriesLeft()
    {
        //Act
        _game.PlayGame();
        var noCategoriesLeftAtEndOfGamePlayer1 = _players[0].GetRemainingCategories().Count == 0;
        var noCategoriesLeftAtEndOfGamePlayer2 = _players[1].GetRemainingCategories().Count == 0;
        
        //Assert
        Assert.True(noCategoriesLeftAtEndOfGamePlayer1);
        Assert.True(noCategoriesLeftAtEndOfGamePlayer2);
    }
    
    private static IEnumerable<object[]> TotalPointsScoredWhileDiceHandAlwaysStaysTheSameTestObjects()
    {
        yield return new object[] { 150, 70, new List<int>{5,5,5,5,5}, new List<int>{6,2,3,4,5}};
        yield return new object[] { 54, 70, new List<int>{3,2,1,4,2}, new List<int>{6,5,4,2,3}};
        yield return new object[] { 56, 32, new List<int>{4,3,2,1,3}, new List<int>{6,2,3,2,3}};
        yield return new object[] { 60, 56, new List<int>{1,2,3,5,4}, new List<int>{3,1,2,4,3}};
        yield return new object[] { 70, 72, new List<int>{6,5,3,4,2}, new List<int>{4,4,4,4,2}};
        yield return new object[] { 100, 56, new List<int>{1,2,3,4,5}, new List<int>{1,2,3,4,3}};
        yield return new object[] { 64, 61, new List<int>{3,2,4,3,5}, new List<int>{3,3,2,2,2}};
    }
    
    
    [Theory]
    [MemberData(nameof(TotalPointsScoredWhileDiceHandAlwaysStaysTheSameTestObjects))]
    public void GivenPlayGameIsCalled_WhileEveryPlayerPLaysTillNoCategoriesLeft_ThenPLayersWillHaveTheirTotalPointsUpdatedAccordingly(int expectedTotalPointsPlayer1, int expectedTotalPointsPlayer2, List<int> constantDiceHandPlayer1, List<int> constantDiceHandPlayer2 )
    {
        //Arrange
        _mockPlayer1RandomDieRoll.Setup(diceHand => diceHand.GetDiceHand()).Returns(constantDiceHandPlayer1);
        _mockPlayer2RandomDieRoll.Setup(diceHand => diceHand.GetDiceHand()).Returns(constantDiceHandPlayer2);
        
        //Act
        _game.PlayGame();
        var totalPointsAtEndOfGamePlayer1 = _players[0].PlayerTotalPoints();
        var totalPointsAtEndOfGamePlayer2 = _players[1].PlayerTotalPoints();

        //Assert
        Assert.Equal(expectedTotalPointsPlayer1, totalPointsAtEndOfGamePlayer1);
        Assert.Equal(expectedTotalPointsPlayer2, totalPointsAtEndOfGamePlayer2);
    }
    
    [Fact]
    public void GivenPlayGameIsCalled_WhileEveryPlayerPLaysTillNoCategoriesLeft_ThenPlayersAbandonChoiceWillBeFalseAtEndOfTheGame()
    {
        //Act
        _game.PlayGame();
        var abandonChoicePlayer1 = _players[0].AbandonedRound();
        var abandonChoicePlayer2 = _players[1].AbandonedRound();

        //Assert
        Assert.False(abandonChoicePlayer1);
        Assert.False(abandonChoicePlayer2);
    }

    [Fact] public void GivenPlayGameIsCalled_WhenAPlayerAbandonsAfterTheStartingRound_ThenGameWillEndImmediately()
    {
        //Arrange
        var player1 = new Player("bundy", new ConsoleUserStrategy(_mockReader.Object, new ConsoleWriter(), new RandomDiceRoll()));
        var player2 = new Player("john", new ComputerStrategy(new RandomDiceRoll()));
        var game = new Game(new List<Player> { player1, player2 }, new RoundFactory(new TurnFactory()));
        _mockReader.SetupSequence(input => input.ReadLine()).Returns("n").Returns("1").Returns("y");
        var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);
        
        //Act
        game.PlayGame();

        //Assert
        Assert.Contains(ConsoleMessages.RoundResults(new RoundOver()), stringWriter.ToString());
        Assert.Contains(ConsoleMessages.RoundCount(1), stringWriter.ToString());
        Assert.DoesNotContain(ConsoleMessages.RoundCount(2), stringWriter.ToString());
    }
}