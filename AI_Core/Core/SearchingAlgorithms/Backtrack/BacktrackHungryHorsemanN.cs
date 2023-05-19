using AI_Core.Core.Node;
using AI_Core.StateRepresentations.HungryHorsemanNState;

namespace AI_Core.Core.SearchingAlgorithms.Backtrack;

public class BacktrackHungryHorsemanN : GraphSearchHungryHorsemanN
{
    private int maxDepth;
    private bool isMemorable;

    public BacktrackHungryHorsemanN(int maxDepth, bool isMemorable, int size = 3) : base(size)
    {
        this.maxDepth = maxDepth;
        this.isMemorable = isMemorable;
    }

    public BacktrackHungryHorsemanN() : this(0, false)
    {
    }

    public BacktrackHungryHorsemanN(int maxDepth) : this(maxDepth, false)
    {
    }

    public BacktrackHungryHorsemanN(bool isMemorable) : this(0, isMemorable)
    {
    }

    public override HungryHorsemanNNode Search() =>
        Search(StartNode);

    private HungryHorsemanNNode Search(HungryHorsemanNNode actualNode)
    {
        int actualDepth = actualNode.Depth;

        //check if we reached the maximum depth
        if (maxDepth != 0 && actualDepth >= maxDepth)
            return null!;

        HungryHorsemanNNode actualParent = null!;
        if (isMemorable)
            actualParent = actualNode.Parent;

        while (actualParent != null)
        {
            if (actualNode.Equals(actualParent))
                return null!;
            actualParent = actualParent.Parent;
        }

        if (actualNode.IsTerminalNode)
            return actualNode;
        for (int i = -2; i < 3; i++)
        {
            for (int j = -2; j < 3; j++)
            {
                HungryHorsemanNNode newNode = new HungryHorsemanNNode(actualNode);
                if (newNode.State.ApplyOperator(i, j))
                {
                    HungryHorsemanNNode terminalNode = Search(newNode);
                    if (terminalNode != null)
                        return terminalNode;
                }
            }
        }

        return null!;
    }
}