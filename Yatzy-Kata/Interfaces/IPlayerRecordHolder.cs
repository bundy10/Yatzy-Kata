using Yatzy_Kata.Data;

namespace Yatzy_Kata.Interfaces;

public interface IPlayerRecordHolder
{
    public void RemoveUsedCategory(Category categoryToRemove);
    public void SetRoundScore(int roundScore);
    public int GetRoundScore();
    public void AddToTotalPoints(int roundScore);
    public int GetTotalPoints();
    public List<Category> GetRemainingCategories();
}