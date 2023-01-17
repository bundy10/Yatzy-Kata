using Moq;
using Yatzy_Kata;
using Yatzy_Kata.Interfaces;

namespace Yatzy_Test;

//ok

public class GameTests
{
    [Fact]
    public void GivenPlayGameIsCalled_ThenGamePromptsPlayerToPlayAgain()
    {
        //Arrange
        var playerMock = new Mock<IPlayer>();
        var game = new Game(new[] { playerMock.Object});
        playerMock.Setup(player => player.PlayAgain()).Verifiable();
        
        //Act
        game.PlayGame();
        
        //Assert
        playerMock.Verify();
    }

    [Fact]
    public void PlayGame_DoesNotAskPlayerTwoToPlayAgain_WhenPlayerOneDoesNotPlayAgain()
    {
        var player1Mock = new Mock<IPlayer>();
        var player2Mock = new Mock<IPlayer>();
        var game = new Game(new[] { player1Mock.Object, player2Mock.Object });
        player1Mock.Setup(player => player.PlayAgain()).Returns(false);

        game.PlayGame();

        player2Mock.Verify(player => player.PlayAgain(), Times.Never);
    }

    [Fact]
    public void GivenPlayGameIsCalled_WhenTheGameIsOver_ThenThePlayerWithTheMostPointsWillPrompted()
    {
        var player1Mock = new Mock<IPlayer>();
        var player2Mock = new Mock<IPlayer>();
        var game = new Game(new[] { player1Mock.Object, player2Mock.Object });
        player1Mock.Setup(player => player.TotalPoints).Returns(100);
        player2Mock.Setup(player => player.TotalPoints).Returns(50);
        game.PlayGame();
        
        player1Mock.Verify();
    }
    
}