using System.Collections;
using Moq;
using Yatzy_Kata;
using Yatzy_Kata.Data;
using Yatzy_Kata.Factories;
using Yatzy_Kata.Interfaces;
using Yatzy_Kata.Outcomes;
using FluentAssertions;

namespace Yatzy_Test;

public class RoundTests
{
    private readonly Round _round;
    private readonly Mock<ITurnFactory> _turnFactoryMock;
    private readonly Player _player1;
    private readonly Player _player2;
    private readonly Mock<ITurn> _player1TurnMock;
    private readonly Mock<ITurn> _player2TurnMock;

    public RoundTests()
    {
        _turnFactoryMock = new Mock<ITurnFactory>();
        _player1TurnMock = new Mock<ITurn>();
        _player2TurnMock = new Mock<ITurn>();
        _player1 = new Player();
        _player2 = new Player();
        _round = new Round(new List<IPlayer> { _player1, _player2 }, _turnFactoryMock.Object);


        
    }

    [Fact]
    public void GivenARound_WhenPlayRoundIsCalled_ThenPromptsPlayersForTheirTurnResults()
    {

        //Arrange
        _turnFactoryMock.Setup(turn => turn.CreateTurn(_player1)).Returns(_player1TurnMock.Object);
        _turnFactoryMock.Setup(turn => turn.CreateTurn(_player2)).Returns(_player2TurnMock.Object);
        _player1TurnMock.Setup(turn => turn.PlayerTurn())
            .Returns(new DiceHandAndCategoryAtTurnEnd(new List<int> { 1, 1, 1, 1, 1 }, new Yahtzee())).Verifiable();
        _player2TurnMock.Setup(turn => turn.PlayerTurn())
            .Returns(new DiceHandAndCategoryAtTurnEnd(new List<int> { 4, 4, 4, 4, 5 }, new FourOfAKind())).Verifiable();

        //Act
        _round.PlayRound();

        //Assert
        _player1TurnMock.Verify();
        _player2TurnMock.Verify();
    }
    
    [Fact]
    public void GivenARound_WhenPlayRoundIsCalled_ThenReturnTheRoundWinner()
    {

        //Arrange
        _turnFactoryMock.Setup(turn => turn.CreateTurn(_player1)).Returns(_player1TurnMock.Object);
        _turnFactoryMock.Setup(turn => turn.CreateTurn(_player2)).Returns(_player2TurnMock.Object);
        _player1TurnMock.Setup(turn => turn.PlayerTurn())
            .Returns(new DiceHandAndCategoryAtTurnEnd(new List<int> { 1, 1, 1, 1, 1 }, new Yahtzee()));
        _player2TurnMock.Setup(turn => turn.PlayerTurn())
            .Returns(new DiceHandAndCategoryAtTurnEnd(new List<int> { 4, 4, 4, 4, 5 }, new FourOfAKind()));
        var expectedWinner = new RoundWinner(50, _player1);

        //Act
        var roundOutcome = _round.PlayRound();

        //Assert
        Assert.Equal(expectedWinner, roundOutcome);
    }
    
    [Fact]
    public void GivenARound_WhenPlayRoundIsCalledAndThereIsATie_ThenReturnARoundTie()
    {

        //Arrange
        _turnFactoryMock.Setup(turn => turn.CreateTurn(_player1)).Returns(_player1TurnMock.Object);
        _turnFactoryMock.Setup(turn => turn.CreateTurn(_player2)).Returns(_player2TurnMock.Object);
        _player1TurnMock.Setup(turn => turn.PlayerTurn())
            .Returns(new DiceHandAndCategoryAtTurnEnd(new List<int> { 1, 1, 1, 1, 1 }, new Yahtzee()));
        _player2TurnMock.Setup(turn => turn.PlayerTurn())
            .Returns(new DiceHandAndCategoryAtTurnEnd(new List<int> { 4, 4, 4, 4, 4 }, new Yahtzee()));
        var expectedRoundTie = new RoundTie(50, new List<IPlayer> { _player1, _player2 });

        //Act
        var roundOutcome = _round.PlayRound();

        //Assert
        roundOutcome.Should().BeEquivalentTo(expectedRoundTie);
    }

}