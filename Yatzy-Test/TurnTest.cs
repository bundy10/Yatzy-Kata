using Moq;
using Yatzy_Kata;
using Yatzy_Kata.Data;
using Yatzy_Kata.Interfaces;
using Yatzy_Kata.Strategies;

namespace Yatzy_Test;

public class TurnTest
{
    private readonly Mock<IRandom> _mockPlayer1RandomDieRoll;
    private readonly Player _player1;
    private readonly Turn _turn;

    public TurnTest()
    {
        _mockPlayer1RandomDieRoll = new Mock<IRandom>();
        _player1 = new Player("bundy", new ComputerStrategy(_mockPlayer1RandomDieRoll.Object));
        _turn = new Turn(_player1);
    }
    
    [Fact]
    public void GivenATurn_WhenPlayerTurnIsCalled_ThenUpdatePlayerRecordsAfterTurn()
    {
        // Arrange
        _mockPlayer1RandomDieRoll.Setup(diceHand => diceHand.GetDiceHand()).Returns(new List<int> { 5, 5, 5, 5, 5 });

        // Act
        _turn.PlayerTurn();
        var expectedPlayerUpdatedRecords = _player1.PlayerRoundScore() == 50 && _player1.PlayerTotalPoints() == 50 &&
                                   _player1.GetRemainingCategories().Count == 12;

        // Assert
        Assert.True(expectedPlayerUpdatedRecords);
    }
}