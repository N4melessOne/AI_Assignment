using AI_Core.Core.Node;
using AI_Core.StateRepresentations.ColoredDisksState;
using AI_Core.StateRepresentations.HungryHorsemanNState;

namespace AI_Core.Core.SearchingAlgorithms.TrialAndError;

public class TrialAndErrorColoredDisks : GraphSearchColoredDisks
{
    private static Random rnd;
    private int maxDepth;
    static TrialAndErrorColoredDisks()
    {
        rnd = new Random();
    }

    public TrialAndErrorColoredDisks(int size = 4, int maxDepth = Int32.MaxValue) : base(size)
    {
        this.maxDepth = maxDepth;
    }

    public override ColoredDisksNode Search() 
        => this.Search(StartNode);
    
    private ColoredDisksNode Search(ColoredDisksNode actualNode)
    {
        int actualDepth = actualNode.Depth;

        if (maxDepth != 0 && actualDepth >= maxDepth)
            return null!;

        if (actualNode.IsTerminalNode)
            return actualNode;

        do
        {
            Directions rowOrColumn = (Directions)rnd.Next(0, Enum.GetNames(typeof(Directions)).Length);
            int index = rnd.Next(0, actualNode.State.Size);
            
            ColoredDisksNode newNode = new ColoredDisksNode(actualNode);
            if (newNode.State.ApplyOperator(rowOrColumn, index))
            {
                ColoredDisksNode terminalNode = Search(newNode);
                if (terminalNode != null)
                    return terminalNode;
            }
        } while (!actualNode.IsTerminalNode);
        
        return null!;
    }
}