using Moq;
using Yatzy_Kata;
using Yatzy_Kata.Interfaces;

namespace Yatzy_Test;

//ok

public class GameTests
{
    private readonly Mock<IPlayer> _player1Mock;
    private readonly Mock<IPlayer> _player2Mock;
    public GameTests()
    {
        _player1Mock = new Mock<IPlayer>();
        _player2Mock = new Mock<IPlayer>();
    }
    [Fact]
    public void GivenPlayGameIsCalled_ThenGamePromptsPlayerToPlayAgain()
    {
        //Arrange
        var game = new Game(new[] { _player1Mock.Object});
        _player1Mock.Setup(player => player.PlayAgain()).Verifiable();
        
        //Act
        game.PlayGame();
        
        //Assert
        _player1Mock.Verify();
    }

    [Fact]
    public void PlayGame_DoesNotAskPlayerTwoToPlayAgain_WhenPlayerOneDoesNotPlayAgain()
    {
        var game = new Game(new[] { _player1Mock.Object, _player2Mock.Object });
        _player1Mock.Setup(player => player.PlayAgain()).Returns(false);

        game.PlayGame();

        _player2Mock.Verify(player => player.PlayAgain(), Times.Never);
    }

    [Fact]
    public void GivenPlayGameIsCalled_WhenTheGameIsOver_ThenThePlayerWithTheMostPointsWillBePrompted()
    {
        var game = new Game(new[] { _player1Mock.Object, _player2Mock.Object });
        _player1Mock.Setup(player => player.TotalPoints).Returns(100);
        _player2Mock.Setup(player => player.TotalPoints).Returns(50);
        
        _player1Mock.SetupSet(player => player.Winner = It.IsAny<bool>()).Verifiable();
        
        game.PlayGame();

        _player1Mock.VerifySet(player => player.Winner = true);
    }

    [Fact]
    public void GivenPlayGameIsCalled_WhenTheGameStarts_ThenARoundIsCreated()
    {
        //Arrange
        var roundFactoryMock = new Mock<IRoundFactory>();
        var game = new Game(new[] { _player1Mock.Object, _player2Mock.Object }, roundFactoryMock.Object);
        
        
        //Act
        game.PlayGame();

        //Assert
        roundFactoryMock.Verify(roundFactory => roundFactory.CreateRound(), Times.AtLeast(1));
    }

}