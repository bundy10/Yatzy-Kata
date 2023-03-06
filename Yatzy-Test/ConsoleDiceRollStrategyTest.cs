using Moq;
using Yatzy_Kata.Interfaces;
using Yatzy_Kata.Strategies;

namespace Yatzy_Test;

public class ConsoleDiceRollStrategyTest
{
    private readonly Mock<IReader> _reader;
    private readonly Mock<IRandom> _mockRandomDiceRoll;
    private readonly Mock<ConsoleDiceRollStrategy> _consoleDiceRollStrategy;

    public ConsoleDiceRollStrategyTest()
    {
        _reader = new Mock<IReader>();
        _mockRandomDiceRoll = new Mock<IRandom>();
        _consoleDiceRollStrategy = new Mock<ConsoleDiceRollStrategy>();
    }
    
    [Fact]
    public void GivenCalculateTurnResultsIsCalled_WhenNoCategoryIsSuitedToBePicked_ThenTakeTheLowestScoringCategory()
    {
        //Arrange
        _mockDiceRoll.Setup(dieHand => dieHand.GetDiceHand()).Returns(new List<int>{1,1,2,3,1});
        var remainingCategories = new List<Category> { new Fives(), new Sixes(), new FullHouse() };
        var expectedTurnResult = new TurnResults(0, new Fives());

        //Act
        _computerStrategy.CalculateTurnResults(remainingCategories);
        var actualScore = _computerStrategy.GetTurnResults();

        //Assert
        actualScore.Should().BeEquivalentTo(expectedTurnResult);

    }
}