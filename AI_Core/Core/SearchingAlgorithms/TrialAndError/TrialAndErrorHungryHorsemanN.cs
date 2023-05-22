using AI_Core.Core.Node;
using AI_Core.StateRepresentations.HungryHorsemanNState;

namespace AI_Core.Core.SearchingAlgorithms.TrialAndError;

public class TrialAndErrorHungryHorsemanN : GraphSearchHungryHorsemanN
{
    private static Random rnd;
    private int maxDepth;
    static TrialAndErrorHungryHorsemanN()
    {
        rnd = new Random();
    }

    public TrialAndErrorHungryHorsemanN(int size = 3, int maxDepth = Int32.MaxValue) : base(size)
    {
        this.maxDepth = maxDepth;
    }

    public override HungryHorsemanNNode Search() 
        => this.Search(StartNode);
    
    private HungryHorsemanNNode Search(HungryHorsemanNNode actualNode)
    {
        int actualDepth = actualNode.Depth;

        if (maxDepth != 0 && actualDepth >= maxDepth)
            return null!;

        if (actualNode.IsTerminalNode)
            return actualNode;

        do
        {
            int i = rnd.Next(-2, 3);
            int j = rnd.Next(-2, 3);
            
            HungryHorsemanNNode newNode = new HungryHorsemanNNode(actualNode);
            if (newNode.State.ApplyOperator(i, j))
            {
                HungryHorsemanNNode terminalNode = Search(newNode);
                if (terminalNode != null)
                    return terminalNode;
            }
        } while (!actualNode.IsTerminalNode);
        
        return null!;
    }
}