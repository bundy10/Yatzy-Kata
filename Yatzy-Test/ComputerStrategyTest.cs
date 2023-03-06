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
    
    public static IEnumerable<object[]> LowerCategoryTest()
    {
        yield return new object[] { 5, new Aces(), new List<int> {1,1,1,1,1} };
        yield return new object[] { 10, new Twos(), new List<int> {2,2,2,2,2} };
        yield return new object[] { 15, new Threes(), new List<int> {3,3,3,3,3} };
        yield return new object[] { 20, new Fours(), new List<int> {4,4,4,4,4} };
        yield return new object[] { 25, new Fives(), new List<int> {5,5,5,5,5} };
        yield return new object[] { 30, new Sixes(), new List<int> {6,6,6,6,6} };
    }

    [Theory]
    [MemberData(nameof(LowerCategoryTest))]
    public void GivenCalculateTurnResultsIsCalled_WhenUpperCategoriesAreNotAvailableADiceHandThatMatchesALowerLevelCategoryIsRolled_ThenTakeThatCategory(int expectedScore, Category expectedCategory, List<int> diceHand)
    {
        //Arrange
        for (var i = _remainingCategories.Count - 1; i >= 6; i--)
        {
            _remainingCategories.RemoveAt(i);
        }
        
        _mockDiceRoll.Setup(dieHand => dieHand.GetDiceHand()).Returns(diceHand);
        var expectedTurnResult = new TurnResults(expectedScore, expectedCategory);

        //Act
        _computerStrategy.CalculateTurnResults(_remainingCategories);
        var actualScore = _computerStrategy.GetTurnResults();

        //Assert
        Assert.Equal(expectedTurnResult, actualScore);
    }
    
    public static IEnumerable<object[]> LowerCategoryIfUpperCategoryIsNotSuitableTest()
    {
        yield return new object[] { 6, new Sixes(), new List<int> {6,4,3,4,1} };
        yield return new object[] { 5, new Fives(), new List<int> {5,4,1,1,2} };
        yield return new object[] { 4, new Fours(), new List<int> {4,3,3,1,1} };
        yield return new object[] { 3, new Threes(), new List<int> {3,2,1,1,2} };
    }

    [Theory]
    [MemberData(nameof(LowerCategoryIfUpperCategoryIsNotSuitableTest))]
    public void GivenCalculateTurnResultsIsCalled_WhenUpperCategoriesAreAvailableButNotSuitedForTheDiceHand_ThenTakeTheHighestScoringLowerCategorySuited(int expectedScore, Category expectedCategory, List<int> diceHand)
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
    
    public static IEnumerable<object[]> IfThereIsNoSuitableUpperCategoryThenChanceWillBeTakenBeforeAnyLowerCategoriesTestObjects()
    {
        yield return new object[] { 18, new Chance(), new List<int> {6,4,3,4,1} };
        yield return new object[] { 13, new Chance(), new List<int> {5,4,1,1,2} };
        yield return new object[] { 12, new Chance(), new List<int> {4,3,3,1,1} };
        yield return new object[] { 9, new Chance(), new List<int> {3,2,1,1,2} };
    }

    [Theory]
    [MemberData(nameof(IfThereIsNoSuitableUpperCategoryThenChanceWillBeTakenBeforeAnyLowerCategoriesTestObjects))]
    public void GivenCalculateTurnResultsIsCalled_WhenUpperCategoriesAreAvailableButNotSuitedForTheDiceHand_ThenTakeChanceBeforeAnyLowerCategories(int expectedScore, Category expectedCategory, List<int> diceHand)
    {
        //Arrange
        _mockDiceRoll.Setup(dieHand => dieHand.GetDiceHand()).Returns(diceHand);
        var expectedTurnResult = new TurnResults(expectedScore, expectedCategory);

        //Act
        _computerStrategy.CalculateTurnResults(_remainingCategories);
        var actualScore = _computerStrategy.GetTurnResults();

        //Assert
        Assert.Equal(expectedTurnResult, actualScore);
    }
}