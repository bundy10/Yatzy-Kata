using Moq;
using Yatzy_Kata;
using Yatzy_Kata.Data;
using Yatzy_Kata.Interfaces;
using Yatzy_Kata.Outcomes;

namespace Yatzy_Test;


/*public class GameTests
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
        _playerMocks[0].Setup(player => player.RecordHolder.GetTotalPoints()).Returns(100);
        _playerMocks[1].Setup(player => player.RecordHolder.GetTotalPoints()).Returns(50);
        _roundFactoryMock.Setup(roundFactory => roundFactory.CreateRound(It.IsAny<IEnumerable<IPlayer>>())).Returns(_roundMock.Object);
        _roundMock.Setup(round => round.PlayRound()).Returns(new RoundOutcomes());
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
        //Arrange
        _playerMocks[0].Setup(player => player.PlayAgain()).Returns(false);
        
        //Act
        _game.PlayGame();
        
        //Assert
        _playerMocks[1].Verify(player => player.PlayAgain(), Times.Never);
    }

    [Fact]
    public void GivenPlayGameIsCalled_WhenTheGameIsOver_ThenThePlayerWithTheMostPointsWillBePrompted()
    {
        
        //Arrange
        _playerMocks[0].SetupSet(player => player.Winner = It.IsAny<bool>()).Verifiable();
        
        //Act
        _game.PlayGame();
        
        //Assert
        _playerMocks[0].VerifySet(player => player.Winner = true);
    }

    [Fact]
    public void GivenPlayGameIsCalled_WhenTheGameStarts_ThenARoundIsPromptedWhenPlayersHaveCategoriesAvailableAndPlayerWantsToPlay()
    {
        //Arrange
        _playerMocks[1].SetupSequence(player => player.PlayAgain())
            .Returns(true)
            .Returns(false);
        _playerMocks[0]
            .SetupSequence(player => player.PlayAgain())
            .Returns(true)
            .Returns(true);
        _playerMocks[0].Setup(player => player.RecordHolder.GetRemainingCategory()).Returns(new List<Category>{new Aces(), new Chance()});
        _playerMocks[1].Setup(player => player.RecordHolder.GetRemainingCategory()).Returns(new List<Category>{new Aces(), new Chance()});
        
        //Act
        _game.PlayGame();

        //Assert
        _roundFactoryMock.Verify(roundFactory => roundFactory.CreateRound(It.IsAny<IEnumerable<IPlayer>>()), Times.AtLeast(1));
    }

    [Fact]
    public void GivenPlayGameIsCalled_WhenThereIsNoRoundPresent_ThenNoRoundsWillBePlayed()
    {
        
        //Act
        _game.PlayGame();
        
        //Assert
        _roundMock.Verify(round => round.PlayRound(),Times.Never);
    }

    [Fact]
    public void GivenPlayGameIsCalled_WhenARoundEndsWithRoundOver_ThenPlayersWillNotGetAskedToPlayAnotherRound()
    {
        //Arrange
        _playerMocks.ForEach(players =>
        {
            players.Setup(player => player.PlayAgain()).Returns(true);
            players.Setup(player => player.RecordHolder.GetRemainingCategory()).Returns(new List<Category>{new Aces(), new Chance()});
        });
        _roundMock.Setup(round => round.PlayRound()).Returns(new RoundOver());
        
        //Act
        _game.PlayGame();
        
        //Assert
        _playerMocks.ForEach(players => players.Verify(player => player.PlayAgain(), Times.Once));
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
        _playerMocks[0].Setup(player => player.RecordHolder.GetRemainingCategory()).Returns(new List<Category>{new Aces(), new Chance()});
        _playerMocks[1].Setup(player => player.RecordHolder.GetRemainingCategory()).Returns(new List<Category>{new Aces(), new Chance()});
        
        //Act
        _game.PlayGame();
        
        //Assert
        var expectedRoundCount = 1;
        _roundMock.Verify(round => round.PlayRound(), Times.Exactly(expectedRoundCount));
    }

    [Fact]

    public void PlayGame_ContinuallyPlaysRounds_UntilPlayersDoNotHaveAnyCategoriesLeft()
    {
        //Arrange
        _playerMocks.ForEach(players =>
        {
            players.Setup(player => player.PlayAgain()).Returns(true);
            players.Setup(player => player.RecordHolder.GetRemainingCategory()).Returns(new List<Category>(0));
        });
        
        //Act
        _game.PlayGame();
        
        //Assert
        _roundMock.Verify(round => round.PlayRound(), Times.Never);
    }
}*/