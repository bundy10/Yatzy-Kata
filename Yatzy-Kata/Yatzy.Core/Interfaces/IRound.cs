using Yatzy_Kata.Outcomes;

namespace Yatzy_Kata.Interfaces;

public interface IRound
{
    RoundOutcomes GetRoundResult();
    void PlayTurns();
}