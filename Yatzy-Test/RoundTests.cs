using Moq;
using Yatzy_Kata;
using Yatzy_Kata.Data;
using Yatzy_Kata.Factories;
using Yatzy_Kata.Interfaces;
using Yatzy_Kata.Outcomes;

namespace Yatzy_Test;

public class RoundTests
{
    private readonly Mock<IPlayer> _player2Mock;
    private readonly Mock<IPlayer> _player1Mock;
    private readonly Round _round;
    private readonly Mock<ITurnFactory> _turnFactoryMock;
    private readonly Mock<ITurn> _turnMock;

    public RoundTests()
    {
        _player1Mock = new Mock<IPlayer>();
        _player2Mock = new Mock<IPlayer>();
        _turnFactoryMock = new Mock<ITurnFactory>();
        _turnMock = new Mock<ITurn>();
        _round = new Round(new List<IPlayer> { _player1Mock.Object, _player2Mock.Object }, _turnFactoryMock.Object);
    }

    [Fact]
    public void GivenARound_WhenPlayRoundIsCalled_ThenPromptsPlayersForTheirScores()
    {

        //Arrange
        _turnFactoryMock.Setup(turn => turn.CreateTurn(_player1Mock.Object)).Returns(_turnMock.Object);
        _player1Mock.SetupGet(player => player.RoundScore).Returns(50);
        _player2Mock.SetupGet(player => player.RoundScore).Returns(30);

        //Act
        _round.PlayRound();

        //Assert
        _player1Mock.VerifyGet(player => player.RoundScore);
        _player2Mock.VerifyGet(player => player.RoundScore);
    }

    [Fact]
    public void GivenARound_WhenPlayRoundIsCalled_ThenRoundCountIsIncrementedByOne()
    {
        //Arrange
        _turnFactoryMock.Setup(turn => turn.CreateTurn(_player1Mock.Object)).Returns(_turnMock.Object);
        
        //Act
        _round.PlayRound();
        
        //Assert
        Assert.Equal(1, _round.GetRoundCount());
    }

    [Fact]
    public void GivenARound_WhenGetTurnResultIsCalled_ThenATurnWillBePromptedForCreation()
    {
        //Arrange
        _turnFactoryMock.Setup(turn => turn.CreateTurn(_player1Mock.Object)).Returns(_turnMock.Object).Verifiable();
        
        //Act
        _round.GetTurnResults();
        
        //Assert
        _turnFactoryMock.Verify();
    }

    [Fact]
    public void GivenARound_WhenGetTurnResultIsCalled_ThenAListOfDiceAndACategoryIsReturned()
    {
        //Arrange
        _turnFactoryMock.Setup(turn => turn.CreateTurn(_player1Mock.Object)).Returns(_turnMock.Object);
        _turnMock.Setup(turn => turn.PlayerTurn())
            .Returns(new DiceHandAndCategoryAtTurnEnd(new List<int> { 1, 2, 3, 4, 5, 6 }, new Category())).Verifiable();

        //Act
        _round.GetTurnResults();

        //Assert
        _turnMock.Verify();
    }
}