using Moq;
using Yatzy_Kata;
using Yatzy_Kata.Data;
using Yatzy_Kata.Interfaces;

namespace Yatzy_Test;

public class TurnTest
{
    private readonly Mock<IRandom> _randomMock;
    private readonly Turn _turn;
    private readonly Mock<IScorer> _scorer;

    public TurnTest()
    {
        _randomMock = new Mock<IRandom>();
        _scorer = new Mock<IScorer>();
        _turn = new Turn(_randomMock.Object, _scorer.Object);
    }

    [Fact]
    public void GivenATurnIsCreated_WhenPlayerTurnIsCalled_ThenReturnDiceRolls()
    {
        //Arrange
        var categoryDummy = new Yahtzee();
        List<int> expectedDiceRolls = new List<int>() {1,2,3,4,5,6};
        _randomMock.Setup(diceRolls => diceRolls.GetDiceNumbersBetweenRange())
            .Returns(new List<int>() { 1, 2, 3, 4, 5, 6 });
        _scorer.Setup(score => score.CalculateScore(It.IsAny<List<int>>()))
            .Returns(new DiceHandAndCategoryAtTurnEnd(expectedDiceRolls, categoryDummy));
        
        
        DiceHandAndCategoryAtTurnEnd expectedOutcomeOfPlayerTurnEnd = new DiceHandAndCategoryAtTurnEnd(expectedDiceRolls, categoryDummy);

        //act 
        _turn.PlayerTurn();
        
        //Assert
        Assert.Equal(expectedOutcomeOfPlayerTurnEnd.Dice, _turn.PlayerTurn().Dice);

    }
}