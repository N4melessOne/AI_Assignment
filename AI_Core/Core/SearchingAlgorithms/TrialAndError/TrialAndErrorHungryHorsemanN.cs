using AI_Core.StateRepresentations.HungryHorsemanNState;

namespace AI_Core.Core.SearchingAlgorithms.TrialAndError;

public class TrialAndErrorHungryHorsemanN
{
    private static Random rnd;
    static TrialAndErrorHungryHorsemanN()
    {
        rnd = new Random();
    }
    
    public bool RandomStep(HungryHorsemanN currentState)
    {
        if (currentState is null)
            return false;
        
        HungryHorsemanN temp = (currentState.Clone() as HungryHorsemanN)!;
        int x = rnd.Next(-2, 3);
        int y = rnd.Next(-2, 3);
        if (temp.IsOperator(x, y) && temp.ApplyOperator(x, y))
        {
            return currentState.ApplyOperator(x, y);
        }

        return false;
    }
}