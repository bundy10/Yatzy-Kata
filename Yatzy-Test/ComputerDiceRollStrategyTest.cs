using Moq;
using Yatzy_Kata.Data;
using Yatzy_Kata.Interfaces;
using Yatzy_Kata.Strategies;

namespace Yatzy_Test;

public class ComputerDiceRollStrategyTests
{
    [Fact]
    public void GivenGetDiceHandIsCalled_WhenAFixedDiceHandIsAvailable_ThenReturnTheDiceHand()
    {
        // Arrange
        var mockRandom = new Mock<IRandom>();
        mockRandom.Setup(diceRoll  => diceRoll.GetDiceNumbersBetweenRange())
            .Returns(new List<int> { 1, 2, 3, 4, 5 });

        var strategy = new ComputerDiceRollStrategy(mockRandom.Object);

        // Act
        strategy.RollDice();
        var diceHand = strategy.GetDiceHand();

        // Assert
        Assert.Equal(new List<int> { 1, 2, 3, 4, 5 }, diceHand);
    }
    
    [Fact]
    public void GivenGetDiceHandIsCalled_WhenRollDiceIsCalled_ThenReturnAValidDiceHand()
    {
        // Arrange
        var strategy = new ComputerDiceRollStrategy(new RandomDiceRoll());
        
        // Act
        strategy.RollDice();
        var diceHand = strategy.GetDiceHand();
        var validDiceHand = diceHand.Count == 5 && diceHand.All(i => i is >= 1 and <= 5);
        
        // Assert
        Assert.True(validDiceHand);
    }
}