using Yatzy_Kata;
using Yatzy_Kata.Data;

namespace Yatzy_Test;

public class ScorerTest
{
    private readonly Scorer _scorer;
    private readonly List<Category> _remainingCategories;

    public ScorerTest()
    {
        _scorer = new Scorer();
        _remainingCategories = new List<Category>() { new Aces(), new Twos(), new Threes(), new Fours(), new Fives(), new Sixes(), new Chance(), new ThreeOfAKind(), new FourOfAKind(), new FullHouse(), new SmallStraight(), new LargeStraight(), new Yahtzee()};
    }

    [Fact]
    public void GivenCalculateScoreIsCalled_WhenAFullHouseIsRolled_ThenReturnFullHouses()
    {
        //Arrange
        var diceRoll = new List<int> { 4, 4, 4, 6, 6 };
        var expectedScore = new DiceHandAndCategoryAtTurnEnd(diceRoll, new FullHouse());

        //Act
        var actualScore = _scorer.CalculateScore(diceRoll, _remainingCategories);

        //Assert
        Assert.Equal(expectedScore, actualScore);
    }
    
    [Fact]
    public void GivenCalculateScoreIsCalled_WhenDicesAreRolledButMatchNoCategoriesLeftOver_ThenReturnAScoreOfZeroAndTakeTheLowestScoringCategoryPossible()
    {
        //Arrange
        var diceRoll = new List<int> { 5, 5, 5, 5, 5 };
        var expectedCategoryChosenAndDiceRoll = new DiceHandAndCategoryAtTurnEnd(diceRoll, new Aces());

        //Act
        _scorer.CalculateScore(diceRoll, _remainingCategories);
        _scorer.CalculateScore(diceRoll, _remainingCategories);
        _scorer.CalculateScore(diceRoll, _remainingCategories);
        _scorer.CalculateScore(diceRoll, _remainingCategories);
        _scorer.CalculateScore(diceRoll, _remainingCategories);
        var categoryChosenAfterAllViableCategoriesAreGone = _scorer.CalculateScore(diceRoll, _remainingCategories);

        //Assert
        Assert.Equal(expectedCategoryChosenAndDiceRoll, categoryChosenAfterAllViableCategoriesAreGone );
    }
    
    [Fact]
    public void GivenCalculateScoreIsCalled_WhenDicesAreRolledAllFivesTwoConsecutiveTimes_ThenReturnATotalScoreOf75()
    {
        //Arrange
        var diceRoll = new List<int> { 5, 5, 5, 5, 5 };

        //Act
        _scorer.CalculateScore(diceRoll,_remainingCategories);
        _scorer.CalculateScore(diceRoll, _remainingCategories);
        var totalScore = _scorer.GetTotalScore();

        //Assert
        Assert.Equal(75, totalScore );
    }
    
}