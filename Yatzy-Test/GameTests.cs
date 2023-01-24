using Moq;
using Yatzy_Kata;
using Yatzy_Kata.Interfaces;
using Yatzy_Kata.Outcomes;

namespace Yatzy_Test;


public class GameTests
{
    private readonly Mock<IRoundFactory> _roundFactoryMock;
    private readonly List<Mock<IPlayer>> _playerMocks;
    private readonly Game _game;
    private readonly Mock<IRound> _roundMock;

    public GameTests()
    {
        _roundMock = new Mock<IRound>();
        _roundFactoryMock = new Mock<IRoundFactory>();
        _playerMocks = new List<Mock<IPlayer>> { new(), new() };
        _roundFactoryMock.Setup(roundFactory => roundFactory.CreateRound()).Returns(_roundMock.Object);
        _game = new Game(_playerMocks.Select(playerMock => playerMock.Object), _roundFactoryMock.Object);
    }
    [Fact]
    public void GivenPlayGameIsCalled_ThenGamePromptsPlayerToPlayAgain()
    {
        //Arrange
        _playerMocks[0].Setup(player => player.PlayAgain()).Verifiable();
        
        //Act
        _game.PlayGame();
        
        //Assert
        _playerMocks[0].Verify();
    }

    [Fact]
    public void PlayGame_DoesNotAskPlayerTwoToPlayAgain_WhenPlayerOneDoesNotPlayAgain()
    {
        _playerMocks[0].Setup(player => player.PlayAgain()).Returns(false);

        _game.PlayGame();

        _playerMocks[1].Verify(player => player.PlayAgain(), Times.Never);
    }

    [Fact]
    public void GivenPlayGameIsCalled_WhenTheGameIsOver_ThenThePlayerWithTheMostPointsWillBePrompted()
    {
        _playerMocks[0].Setup(player => player.TotalPoints).Returns(100);
        _playerMocks[1].Setup(player => player.TotalPoints).Returns(50);
        
        _playerMocks[0].SetupSet(player => player.Winner = It.IsAny<bool>()).Verifiable();
        
        _game.PlayGame();

        _playerMocks[0].VerifySet(player => player.Winner = true);
    }

    [Fact]
    public void GivenPlayGameIsCalled_WhenTheGameStarts_ThenARoundIsPrompted()
    {
        
        //Act
        _game.PlayGame();

        //Assert
        _roundFactoryMock.Verify(roundFactory => roundFactory.CreateRound(), Times.AtLeast(1));
    }

    [Fact]
    public void GivenPlayGameIsCalled_WhenThereIsARoundPresent_ThenTheRoundIsPlayed()
    {
        
        //Act
        _game.PlayGame();
        
        //Assert
        _roundMock.Verify(round => round.PlayRound(),Times.AtLeast(1));
    }

    [Fact]
    public void PlayGames_DoesNotAskToPlayAgain_WhenRoundsEndsWithAbandon()
    {
        //Arrange
        _roundMock.Setup(round => round.PlayRound()).Returns(new RoundOver());
        
        //Act
        _game.PlayGame();
        
        //Assert
        _playerMocks
            .ForEach(playerMock => playerMock
                .Verify(player => player.PlayAgain(), Times.Never));
    }
    
    [Fact]
    public void PlayGame_ContinuallyPlaysRounds_UntilPlayerDoesNotPlayAgain()
    {
        //Arrange
        _playerMocks[1].SetupSequence(player => player.PlayAgain())
            .Returns(true)
            .Returns(false);
        _playerMocks[0]
            .SetupSequence(player => player.PlayAgain())
            .Returns(true)
            .Returns(true);
        
        //Act
        _game.PlayGame();
        
        //Assert
        var expectedRoundCount = 2;
        _roundMock.Verify(round => round.PlayRound(), Times.Exactly(expectedRoundCount));
    }
}