using AI_Core.Core.Node;
using AI_Core.StateRepresentations.ColoredDisksState;
using AI_Core.StateRepresentations.HungryHorsemanNState;

namespace AI_Core.Core.SearchingAlgorithms.TrialAndError;

public class TrialAndErrorColoredDisks : GraphSearchColoredDisks
{
    private static Random rnd;
    private ColoredDisksNode startNode;
    private Stack<ColoredDisksState> solution;

    static TrialAndErrorColoredDisks()
    {
        rnd = new Random();
    }

    public TrialAndErrorColoredDisks(int size = 3)
    {
        startNode = new ColoredDisksNode(size);
    }

    public bool RandomStep(ColoredDisksState currentState)
    {
        if (currentState is null)
            return false;

        ColoredDisksState temp = (currentState.Clone() as ColoredDisksState)!;
        int index = rnd.Next(0, currentState.Size);
        Directions rowOrColumn = (Directions)rnd.Next(2);
        if (temp.IsOperator(rowOrColumn, index) && temp.ApplyOperator(rowOrColumn, index))
        {
            return currentState.ApplyOperator(rowOrColumn, index);
        }
        return false;
    }

    //TODO:Fix this, to be the main method call and handle the whole process
    public override ColoredDisksNode Search()
    {
        do
        {
            ColoredDisksNode newNode = new ColoredDisksNode(this.startNode);
            if (RandomStep(this.startNode.State))
            {
                
            }
        } while (!startNode.IsTerminalNode);

        return this.startNode;
    }
}