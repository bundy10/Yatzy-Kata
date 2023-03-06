using System.Collections;
using Moq;
using Yatzy_Kata;
using Yatzy_Kata.Data;
using Yatzy_Kata.Factories;
using Yatzy_Kata.Interfaces;
using Yatzy_Kata.Outcomes;
using FluentAssertions;
using Yatzy_Kata.Strategies;

namespace Yatzy_Test;

public class RoundTests
{
    private readonly Round _round;
    private readonly Mock<ITurnFactory> _turnFactoryMock;
    private readonly Player _player1;
    private readonly Player _player2;
    private readonly Mock<ITurn> _player1TurnMock;
    private readonly Mock<ITurn> _player2TurnMock;
    private readonly Mock<IRandom> _mockPlayer1RandomDieRoll;
    private readonly Mock<IRandom> _mockPlayer2RandomDieRoll;

    public RoundTests()
    {
        _mockPlayer1RandomDieRoll = new Mock<IRandom>();
        _mockPlayer2RandomDieRoll = new Mock<IRandom>();
        _turnFactoryMock = new Mock<ITurnFactory>();
        _player1TurnMock = new Mock<ITurn>();
        _player2TurnMock = new Mock<ITurn>();
        _player1 = new Player("bundy", new ComputerStrategy(_mockPlayer1RandomDieRoll.Object));
        _player2 = new Player("john", new ComputerStrategy(_mockPlayer2RandomDieRoll.Object));
        _round = new Round(new List<Player> { _player1, _player2 }, _turnFactoryMock.Object);
    }

    [Fact]
    public void GivenARound_WhenPlayTurnsIsCalled_ThenPromptsTheTurnFactoryToCreateTurnsForEachPlayerAndEachTurnIsPlayed()
    {

        //Arrange
        _turnFactoryMock.Setup(turn => turn.CreateTurn(_player1)).Returns(_player1TurnMock.Object);
        _turnFactoryMock.Setup(turn => turn.CreateTurn(_player2)).Returns(_player2TurnMock.Object);
        _player1TurnMock.Setup(playerTurn => playerTurn.PlayerTurn()).Verifiable();
        _player2TurnMock.Setup(playerTurn => playerTurn.PlayerTurn()).Verifiable();
        

        //Act
        _round.PlayTurns();

        //Assert
        _turnFactoryMock.Verify();
    }
    
    public static IEnumerable<object[]> RoundWinnerTestObject()
    {
        yield return new object[] { 50, new List<int> {5,5,5,5,5}, new List<int> {1,2,3,3,3} };
        yield return new object[] { 40, new List<int> {1,2,3,4,5}, new List<int> {3,3,3,2,1} };
        yield return new object[] { 30, new List<int> {1,2,3,4,1}, new List<int> {1,3,3,1,5} };
        yield return new object[] { 21, new List<int> {4,4,4,4,5}, new List<int> {1,2,3,2,6} };
    }
    
    [Theory]
    [MemberData(nameof(RoundWinnerTestObject))]
    public void GivenARound_WhenGetRoundResultIsCalledWhereThereIsAPlayerThatScoredMorePointsThanOtherPlayers_ThenReturnTheRoundWinner(int score, List<int> diceHandOne, List<int> diceHandTwo)
    {

        //Arrange
        _turnFactoryMock.Setup(turn => turn.CreateTurn(_player1)).Returns(_player1TurnMock.Object);
        _turnFactoryMock.Setup(turn => turn.CreateTurn(_player2)).Returns(_player2TurnMock.Object);
        _mockPlayer1RandomDieRoll.Setup(diceHand => diceHand.GetDiceHand()).Returns(diceHandOne);
        _mockPlayer2RandomDieRoll.Setup(diceHand => diceHand.GetDiceHand()).Returns(diceHandTwo);
        var dummyInt = 10;
        _player1.CompleteTurn();
        _player2.CompleteTurn();
        _player1.UpdateRecordsAfterTurn(dummyInt);
        _player2.UpdateRecordsAfterTurn(dummyInt);
        var expectedWinner = new RoundWinner(score, _player1);
        
        //Act
        _round.PlayTurns();
        var roundResult = _round.GetRoundResult();

        //Assert
        roundResult.Should().BeEquivalentTo(expectedWinner);
    }
    
    public static IEnumerable<object[]> RoundTieTestObject()
    {
        yield return new object[] { 50, new List<int> {5,5,5,5,5}, new List<int> {5,5,5,5,5} };
        yield return new object[] { 40, new List<int> {1,2,3,4,5}, new List<int> {1,2,3,4,5} };
        yield return new object[] { 30, new List<int> {1,2,3,4,1}, new List<int> {1,2,3,4,1} };
        yield return new object[] { 21, new List<int> {4,4,4,4,5}, new List<int> {4,4,4,4,5} };
    }
    
    [Theory]
    [MemberData(nameof(RoundTieTestObject))]
    public void GivenARound_WhenGetRoundResultIsCalledWhereTheHighestScoreIsAccompaniedByTwoOrMorePlayers_ThenReturnARoundTie(int score, List<int> diceHandOne, List<int> diceHandTwo)
    {

        //Arrange
        _turnFactoryMock.Setup(turn => turn.CreateTurn(_player1)).Returns(_player1TurnMock.Object);
        _turnFactoryMock.Setup(turn => turn.CreateTurn(_player2)).Returns(_player2TurnMock.Object);
        _mockPlayer1RandomDieRoll.Setup(diceHand => diceHand.GetDiceHand()).Returns(diceHandOne);
        _mockPlayer2RandomDieRoll.Setup(diceHand => diceHand.GetDiceHand()).Returns(diceHandTwo);
        var dummyInt = 10;
        _player1.CompleteTurn();
        _player2.CompleteTurn();
        _player1.UpdateRecordsAfterTurn(dummyInt);
        _player2.UpdateRecordsAfterTurn(dummyInt);
        var expectedTie = new RoundTie(score, new List<Player>{_player1, _player2});
        
        //Act
        _round.PlayTurns();
        var roundResult = _round.GetRoundResult();

        //Assert
        roundResult.Should().BeEquivalentTo(expectedTie);
    }
}