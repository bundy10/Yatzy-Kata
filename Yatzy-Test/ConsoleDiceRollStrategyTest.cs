using Moq;
using Yatzy_Kata;
using Yatzy_Kata.Data;
using Yatzy_Kata.Interfaces;
using Yatzy_Kata.Strategies;

namespace Yatzy_Test;

public class ConsoleDiceRollStrategyTest
{
    private readonly Mock<IReader> _reader;
    private readonly Mock<IRandom> _mockRandomDiceRoll;
    private readonly ConsoleDiceRollStrategy _consoleDiceRollStrategy;

    public ConsoleDiceRollStrategyTest()
    {
        _reader = new Mock<IReader>();
        _mockRandomDiceRoll = new Mock<IRandom>();
        _consoleDiceRollStrategy = new ConsoleDiceRollStrategy(_reader.Object, new ConsoleWriter(), _mockRandomDiceRoll.Object);
    }
    
    [Fact]
    public void GivenRollDiceIsCalled_WhenPlayerDoesNotWantToReRoll_ThenReturnTheDiceHand()
    {
        //Arrange
        _mockRandomDiceRoll.Setup(diceHand => diceHand.GetDiceHand()).Returns(new List<int> { 5, 5, 5, 5, 5 });
        _reader.Setup(input => input.ReadLine()).Returns("n");

        //Act
        _consoleDiceRollStrategy.RollDice();
        var expectedDiceHand = new List<int> { 5, 5, 5, 5, 5 };

        //Assert
        Assert.Equal(expectedDiceHand, _consoleDiceRollStrategy.GetDiceHand());

    }

    public static IEnumerable<object[]> ReRolledDicesTestObjects()
    {
        yield return new object[] { 1, "1", new List<int> {1,6,6,6,6} };
        yield return new object[] { 2, "12", new List<int> {2,2,6,6,6} };
        yield return new object[] { 3, "123", new List<int> {3,3,3,6,6} };
        yield return new object[] { 4, "1234", new List<int> {4,4,4,4,6} };
        yield return new object[] { 5, "12345", new List<int> {5,5,5,5,5} };
    }
    
    [Theory]
    [MemberData(nameof(ReRolledDicesTestObjects))]
    public void GivenRollDiceIsCalled_WhenPlayerDoesWantsToReRollFirstAndSecondDiceOnce_ThenReturnTheNewAdjustedDiceHand(int reRollSingleDiceOutcome, string dieToReRoll, List<int> expectedDiceHand)
    {
        //Arrange
        _mockRandomDiceRoll.Setup(diceHand => diceHand.GetDiceHand()).Returns(new List<int> { 6, 6, 6, 6, 6 });
        _mockRandomDiceRoll.Setup(diceHand => diceHand.GetDiceRoll()).Returns(reRollSingleDiceOutcome);
        _reader.SetupSequence(input => input.ReadLine()).Returns("y").Returns(dieToReRoll).Returns("n");

        //Act
        _consoleDiceRollStrategy.RollDice();

        //Assert
        Assert.Equal(expectedDiceHand, _consoleDiceRollStrategy.GetDiceHand());

    }
    
    [Fact]
    public void GivenRollDiceIsCalled_WhenPlayerReRollsThreeTimes_ThenTheMaximumTimesAllowedToReRollIsReachedReturnTheDiceHand()
    {
        //Arrange
        _mockRandomDiceRoll.Setup(diceHand => diceHand.GetDiceHand()).Returns(new List<int> { 5, 5, 5, 5, 5 });
        _mockRandomDiceRoll.Setup(diceHand => diceHand.GetDiceRoll()).Returns(1);
        _reader.SetupSequence(input => input.ReadLine()).Returns("y").Returns("1").Returns("y").Returns("2")
            .Returns("y").Returns("3");

        //Act
        _consoleDiceRollStrategy.RollDice();
        var expectedDiceHand = new List<int> { 1, 1, 1, 5, 5 };

        //Assert
        Assert.Equal(expectedDiceHand, _consoleDiceRollStrategy.GetDiceHand());

    }
    
    [Fact]
    public void GivenRollDiceIsCalled_ThenDisplayTheCurrentDiceHandRolledAndPromptUserToReRollOrNot()
    {
        //Arrange
        _reader.SetupSequence(input => input.ReadLine()).Returns("n");
        _mockRandomDiceRoll.Setup(diceHand => diceHand.GetDiceHand()).Returns(new List<int> { 5, 5, 5, 5, 5 });
        var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);

        //Act
        _consoleDiceRollStrategy.RollDice();

        //Assert
        Assert.Contains(ConsoleMessages.DiceHandToString(new List<int> { 5, 5, 5, 5, 5 }), stringWriter.ToString());
        Assert.Contains(ConsoleMessages.ReRollDecision, stringWriter.ToString());
    }
    
    [Fact]
    public void GivenRollDiceIsCalled_WhenPlayerWantsToReRoll_ThenPromptUserToSelectWhichDieToReRoll()
    {
        //Arrange
        _reader.SetupSequence(input => input.ReadLine()).Returns("y").Returns("1").Returns("n");
        _mockRandomDiceRoll.Setup(diceHand => diceHand.GetDiceHand()).Returns(new List<int> { 5, 5, 5, 5, 5 });
        var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);

        //Act
        _consoleDiceRollStrategy.RollDice();

        //Assert
        Assert.Contains(ConsoleMessages.SelectWhichDieToReRoll, stringWriter.ToString());
    }
    
    [Fact]
    public void GivenRollDiceIsCalled_WhenUserInputsInvalidReRollDecisionAndInvalidDieToSelectForReRoll_ThenUserIsPromptedAnInvalidMessage()
    {
        //Arrange
        _reader.SetupSequence(input => input.ReadLine()).Returns("asd").Returns("y").Returns("asd").Returns("1")
            .Returns("n");
        _mockRandomDiceRoll.Setup(diceHand => diceHand.GetDiceHand()).Returns(new List<int> { 5, 5, 5, 5, 5 });
        var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);

        //Act
        _consoleDiceRollStrategy.RollDice();

        //Assert
        Assert.Contains(ConsoleMessages.InvalidReRollDecision, stringWriter.ToString());
        Assert.Contains(ConsoleMessages.InvalidDieInput, stringWriter.ToString());
    }
}