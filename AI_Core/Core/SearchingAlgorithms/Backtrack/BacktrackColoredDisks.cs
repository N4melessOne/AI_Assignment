using AI_Core.Core.Node;
using AI_Core.StateRepresentations.ColoredDisksState;

namespace AI_Core.Core.SearchingAlgorithms.Backtrack;

public class BacktrackColoredDisks : GraphSearchColoredDisks
{
    private int maxDepth;
    private bool isMemorable;

    public BacktrackColoredDisks(int maxDepth, bool isMemorable, int size = 4) : base(size)
    {
        this.maxDepth = maxDepth;
        this.isMemorable = isMemorable;
    }

    public BacktrackColoredDisks() : this(0, false)
    {
    }

    public BacktrackColoredDisks(int maxDepth) : this(maxDepth, false)
    {
    }

    public BacktrackColoredDisks(bool isMemorable) : this(0, isMemorable)
    {
    }

    public override ColoredDisksNode Search() =>
        Search(StartNode);

    private ColoredDisksNode Search(ColoredDisksNode actualNode)
    {
        int actualDepth = actualNode.Depth;

        if (maxDepth != 0 && actualDepth >= maxDepth)
            return null!;

        ColoredDisksNode actualParent = null!;
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
        
        foreach (Directions dir in Enum.GetValues(typeof(Directions)))
        {
            for (int i = 0; i < actualNode.State.Size; i++)
            {
                ColoredDisksNode newNode = new ColoredDisksNode(actualNode);
                if (newNode.State.ApplyOperator(dir, i))
                {
                    ColoredDisksNode terminalNode = Search(newNode);
                    if (terminalNode != null)
                        return terminalNode;
                }
            }
        }

        return null!;
    }
}