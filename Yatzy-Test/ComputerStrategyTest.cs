using Moq;
using Yatzy_Kata;
using Yatzy_Kata.Data;
using Yatzy_Kata.Interfaces;
using Yatzy_Kata.Strategies;

namespace Yatzy_Test;

public class ComputerStrategyTest
{
    private readonly ComputerStrategy _computerStrategy;
    private readonly List<Category> _remainingCategories;
    private readonly Mock<IRandom> _mockDiceRoll;

    public ComputerStrategyTest()
    {
        _mockDiceRoll = new Mock<IRandom>();
        _computerStrategy = new ComputerStrategy(_mockDiceRoll.Object);
        _remainingCategories = new List<Category>() { new Aces(), new Twos(), new Threes(), new Fours(), new Fives(), new Sixes(), new Chance(), new ThreeOfAKind(), new FourOfAKind(), new FullHouse(), new SmallStraight(), new LargeStraight(), new Yahtzee()};
    }

    public static IEnumerable<object[]> UpperCategoryTest()
    {
        yield return new object[] { 50, new Yahtzee(), new List<int> {5,5,5,5,5} };
        yield return new object[] { 40, new LargeStraight(), new List<int> {2,3,4,5,6} };
        yield return new object[] { 30, new SmallStraight(), new List<int> {1,2,3,4,6} };
        yield return new object[] { 25, new FullHouse(), new List<int> {2,2,2,4,4} };
        yield return new object[] { 21, new FourOfAKind(), new List<int> {4,5,4,4,4} };
        yield return new object[] { 12, new ThreeOfAKind(), new List<int> {3,3,3,2,1} };
    }

    [Theory]
    [MemberData(nameof(UpperCategoryTest))]
    public void GivenCalculateTurnResultsIsCalled_WhenADiceHandThatMatchesAnUpperLevelCategoryIsRolled_ThenTakeThatCategory(int expectedScore, Category expectedCategory, List<int> diceHand)
    {
        //Arrange
        _remainingCategories.Remove(new Chance());
        _mockDiceRoll.Setup(dieHand => dieHand.GetDiceHand()).Returns(diceHand);
        var expectedTurnResult = new TurnResults(expectedScore, expectedCategory);

        //Act
        _computerStrategy.CalculateTurnResults(_remainingCategories);
        var actualScore = _computerStrategy.GetTurnResults();

        //Assert
        Assert.Equal(expectedTurnResult, actualScore);
    }
    
    /*[Fact]
    public void GivenCalculateScoreIsCalled_WhenDicesAreRolledButMatchNoCategoriesLeftOver_ThenReturnAScoreOfZeroAndTakeTheLowestScoringCategoryPossible()
    {
        //Arrange
        var diceRoll = new List<int> { 5, 5, 5, 5, 5 };
        var expectedCategoryChosenAndDiceRoll = new TurnResults(32, new Aces());

        //Act
        _computerStrategy.CalculateTurnResults(diceRoll, _remainingCategories);
        _computerStrategy.CalculateTurnResults(diceRoll, _remainingCategories);
        _computerStrategy.CalculateTurnResults(diceRoll, _remainingCategories);
        _computerStrategy.CalculateTurnResults(diceRoll, _remainingCategories);
        _computerStrategy.CalculateTurnResults(diceRoll, _remainingCategories);
        var categoryChosenAfterAllViableCategoriesAreGone = _computerStrategy.CalculateTurnResults(diceRoll, _remainingCategories);

        //Assert
        Assert.Equal(expectedCategoryChosenAndDiceRoll, categoryChosenAfterAllViableCategoriesAreGone );
    }*/
    
    /*[Fact]
    public void GivenCalculateScoreIsCalled_WhenDicesAreRolledAllFivesTwoConsecutiveTimes_ThenReturnATotalScoreOf75()
    {
        //Arrange
        var diceRoll = new List<int> { 5, 5, 5, 5, 5 };

        //Act
        _computerStrategy.CalculateScore(diceRoll,_remainingCategories);
        _computerStrategy.CalculateScore(diceRoll, _remainingCategories);
        var totalScore = _computerStrategy.GetTurnResults().Score

        //Assert
        Assert.Equal(75, totalScore );
    }*/
    
}