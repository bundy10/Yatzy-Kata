using Moq;
using Yatzy_Kata;
using Yatzy_Kata.Interfaces;
using Yatzy_Kata.Outcomes;

namespace Yatzy_Test;

public class RoundTests
{
    private readonly Mock<IPlayer> _player2Mock;
    private readonly Mock<IPlayer> _player1Mock;
    private readonly Round _round;

    public RoundTests()
    {
        _player1Mock = new Mock<IPlayer>();
        _player2Mock = new Mock<IPlayer>();
        _round = new Round(new List<IPlayer> { _player1Mock.Object, _player2Mock.Object });
    }

    [Fact]
    public void GivenARound_WhenPlayRoundIsCalled_ThenPromptsPlayersForTheirScores()
    {

        //Arrange
        _player1Mock.SetupGet(player => player.RoundScore).Returns(50);
        _player2Mock.SetupGet(player => player.RoundScore).Returns(30);

        //Act
        _round.PlayRound();

        //Assert
        _player1Mock.VerifyGet(player => player.RoundScore);
        _player2Mock.VerifyGet(player => player.RoundScore);
    }
}