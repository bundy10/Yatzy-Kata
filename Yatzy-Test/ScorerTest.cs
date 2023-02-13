using Yatzy_Kata;
using Yatzy_Kata.Data;

namespace Yatzy_Test;

public class ScorerTest
{
    private readonly Scorer _scorer;

    public ScorerTest()
    {
        _scorer = new Scorer();
    }

    [Fact]
    public void GivenCalculateScoreIsCalled_WhenAFullHouseIsRolled_ThenReturnFullHouses()
    {
        //Arrange
        var diceRoll = new List<int> { 4, 4, 4, 6, 6 };
        var expectedScore = new DiceHandAndCategoryAtTurnEnd(diceRoll, new FullHouse());

        //Act
        var actualScore = _scorer.CalculateScore(diceRoll);

        //Assert
        Assert.Equal(expectedScore, actualScore);
    }
    
}