using Moq;
using Yatzy_Kata;
using Yatzy_Kata.Data;
using Yatzy_Kata.Interfaces;

namespace Yatzy_Test;

public class TurnTest
{
    private readonly Mock<IRandom> _randomMock;
    private readonly Turn _turn;

    public TurnTest()
    {
        _randomMock = new Mock<IRandom>();
        _turn = new Turn(_randomMock.Object);
    }

    [Fact]
    public void GivenATurnIsCreated_WhenPlayerTurnIsCalled_ThenReturnDiceRolls()
    {
        //Arrange
        _randomMock.Setup(diceRolls => diceRolls.GetDiceNumbersBetweenRange())
            .Returns(new List<int>() { 1, 2, 3, 4, 5, 6 });
        const string categoryDummy = "Dummy";
        List<int> expectedDiceRolls = new List<int>() {1,2,3,4,5,6};
        DiceHandAndCategoryAtTurn expectedOutcomeOfPlayerTurn = new DiceHandAndCategoryAtTurn(expectedDiceRolls, categoryDummy);

        //act 
        _turn.PlayerTurn();
        
        //Assert
        Assert.Equal(expectedOutcomeOfPlayerTurn.Dice, _turn.PlayerTurn().Dice);

    }
}