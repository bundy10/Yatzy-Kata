using Moq;
using Yatzy_Kata;
using Yatzy_Kata.Data;
using Yatzy_Kata.Interfaces;
using Yatzy_Kata.Strategies;

namespace Yatzy_Test;

public class ConsoleUserStrategyTest
{
    private readonly Mock<IReader> _reader;
    private readonly Mock<IRandom> _mockRandom;
    private readonly ConsoleUserStrategy _consoleUserStrategy;
    private readonly List<Category> _remainingCategories;

    public ConsoleUserStrategyTest()
    {
        var writer = new ConsoleWriter();
        _reader = new Mock<IReader>();
        _mockRandom = new Mock<IRandom>();
        _consoleUserStrategy = new ConsoleUserStrategy(_reader.Object, writer, _mockRandom.Object);
        _remainingCategories = new List<Category> { new Fives(), new Aces(), new Yahtzee() };
    }

    [Fact]
    public void GivenGetAbandonChoiceIsCalled_WhenThePlayerHasDecidedNotToAbandon_ThenReturnFalse()
    {
        //Arrange
        _reader.Setup(input => input.ReadLine()).Returns("n");

        //Act
        _consoleUserStrategy.DoesPlayerWantToAbandonGame();
        var actualPlayerAbandonedStatus = _consoleUserStrategy.GetAbandonChoice();

        //Assert
        Assert.False(actualPlayerAbandonedStatus);
    }
    
    [Fact]
    public void GivenGetAbandonChoiceIsCalled_WhenThePlayerHasAbandoned_ThenReturnTrue()
    {
        //Arrange
        _reader.Setup(input => input.ReadLine()).Returns("y");

        //Act
        _consoleUserStrategy.DoesPlayerWantToAbandonGame();
        var actualPlayerAbandonedStatus = _consoleUserStrategy.GetAbandonChoice();

        //Assert
        Assert.True(actualPlayerAbandonedStatus);
    }
    
    [Fact]
    public void GivenDoesPlayerWantToAbandonGameIsCalled_ThenPromptTheUserToAbandonOrNot()
    {
        //Arrange
        _reader.Setup(input => input.ReadLine()).Returns("y");
        var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);

        //Act
        _consoleUserStrategy.DoesPlayerWantToAbandonGame();

        //Assert
        Assert.Contains(ConsoleMessages.AbandonOrNot, stringWriter.ToString());
    }
    
    [Fact]
    public void GivenCalculateTurnResultsIsCalled_WhenAListOfRemainingCategoriesAreAvailable_ThenPromptTheUserToSelectACategoryAndDisplayTheRemainingCategories()
    {
        //Arrange
        _reader.SetupSequence(input => input.ReadLine()).Returns("n").Returns("n").Returns("InvalidInput").Returns("3");
        _mockRandom.Setup(diceHand => diceHand.GetDiceHand()).Returns(new List<int> { 5, 5, 5, 5, 5 });
        var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);

        //Act
        _consoleUserStrategy.CalculateTurnResults(_remainingCategories);

        //Assert
        Assert.Contains(ConsoleMessages.SelectCategory, stringWriter.ToString());
        Assert.Contains(ConsoleMessages.RemainingCategoriesToString(_remainingCategories), stringWriter.ToString());
    }
    
    [Fact]
    public void GivenCalculateTurnResultsIsCalled_WhenAnInvalidInputToSelectACategoryIsPresented_ThenPromptTheUserToEnterAValidResponse()
    {
        //Arrange
        _reader.SetupSequence(input => input.ReadLine()).Returns("n").Returns("n").Returns("InvalidInput").Returns("3");
        _mockRandom.Setup(diceHand => diceHand.GetDiceHand()).Returns(new List<int> { 5, 5, 5, 5, 5 });
        var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);

        //Act
        _consoleUserStrategy.CalculateTurnResults(_remainingCategories);

        //Assert
        Assert.Contains(ConsoleMessages.InvalidCategorySelectionInput, stringWriter.ToString());
    }
    
    private static IEnumerable<object[]> ScoreTestObjects()
    {
        yield return new object[] { 50, "3", new List<int>{5,5,5,5,5}};
        yield return new object[] { 5, "2", new List<int>{1,1,1,1,1}};
        yield return new object[] { 25, "1", new List<int>{5,5,5,5,5}};
    }
    
    
    [Theory]
    [MemberData(nameof(ScoreTestObjects))]
    public void GivenCalculateTurnResultsIsCalled_WhenThePlayerReRollsThreeTimes_ThenReturnThePlayersSelections(int expectedScore, string userCategoryChoice, List<int> diceHand)
    {
        //Arrange
        _reader.SetupSequence(input => input.ReadLine()).Returns("n").Returns("n").Returns(userCategoryChoice);
        _mockRandom.Setup(dice => dice.GetDiceHand()).Returns(diceHand);

        //Act
        _consoleUserStrategy.CalculateTurnResults(_remainingCategories);
        var actualCategoryChosen = _consoleUserStrategy.GetTurnResults()?.Score;

        //Assert
        Assert.Equal(expectedScore, actualCategoryChosen);
    }
    
    
    private static IEnumerable<object[]> CategorySelectionTestObjects()
    {
        yield return new object[] { "3", new Yahtzee()};
        yield return new object[] { "2", new Aces()};
        yield return new object[] { "1", new Fives()};
    }
    
    [Theory]
    [MemberData(nameof(CategorySelectionTestObjects))]
    public void GivenCalculateTurnResultsIsCalled_WhenThereIsAListOfCategoriesAvailable_ThenReturnThePlayersCategorySelection(string userCategoryChoice, Category expectedCategoryChosen)
    {
        //Arrange
        _reader.SetupSequence(input => input.ReadLine()).Returns("n").Returns("n").Returns(userCategoryChoice);
        _mockRandom.Setup(diceHand => diceHand.GetDiceHand()).Returns(new List<int> { 5, 5, 5, 5, 5 });

        //Act
        _consoleUserStrategy.CalculateTurnResults(_remainingCategories);
        var actualCategoryChosen = _consoleUserStrategy.GetTurnResults()?.Category;

        //Assert
        Assert.Equal(expectedCategoryChosen, actualCategoryChosen);
    }

    [Fact]
    public void GivenCalculateTurnResultsIsCalled_WhenThereIsAListOfCategoriesAvailable_ThenReturnThePlayersTurnResult()
    {
        //Arrange
        _reader.SetupSequence(input => input.ReadLine()).Returns("n").Returns("n").Returns("3");
        _mockRandom.Setup(diceHand => diceHand.GetDiceHand()).Returns(new List<int> { 5, 5, 5, 5, 5 });

        //Act
        _consoleUserStrategy.CalculateTurnResults(_remainingCategories);
        var actualTurnResults = _consoleUserStrategy.GetTurnResults();
        var expectedTurnResults = new TurnResults(50, new Yahtzee());

        //Assert
        Assert.Equal(expectedTurnResults, actualTurnResults);
    }
}