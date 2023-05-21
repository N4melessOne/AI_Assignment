using AI_Core.Core.Node;

namespace AI_Core.Core.SearchingAlgorithms.DepthFirst;

public class DepthFirstColoredDisks : GraphSearchColoredDisks
{
    private Stack<ColoredDisksNode> openNodes;
    private List<ColoredDisksNode> closedNodes;
    private bool circleDetection;

    public DepthFirstColoredDisks(bool circleDetection, int size = 4)
    {
        openNodes = new Stack<ColoredDisksNode>();
        openNodes.Push(new ColoredDisksNode(size));
        closedNodes = new List<ColoredDisksNode>();
        this.circleDetection = circleDetection;
    }

    public DepthFirstColoredDisks(int size = 4) : this(true, size)
    {
    }

    public override ColoredDisksNode Search()
    {
        return this.circleDetection
            ? SearchTerminalNodeWithCircleDetection()
            : SearchTerminalNodeWithoutCircleDetection();
    }

    private ColoredDisksNode SearchTerminalNodeWithoutCircleDetection()
    {
        while (openNodes.Count != 0)
        {
            ColoredDisksNode actual = openNodes.Pop();
            List<ColoredDisksNode> children = actual.Extend();
            foreach (ColoredDisksNode child in children)
            {
                if (child.IsTerminalNode)
                    return child;
                openNodes.Push(child);
            }
        }

        return null!;
    }

    private ColoredDisksNode SearchTerminalNodeWithCircleDetection()
    {
        while (openNodes.Count != 0)
        {
            ColoredDisksNode actual = openNodes.Pop();
            List<ColoredDisksNode> children = actual.Extend();
            foreach (ColoredDisksNode child in children)
            {
                if (child.IsTerminalNode)
                    return child;
                if (!closedNodes.Contains(child) && !openNodes.Contains(child))
                    openNodes.Push(child);
            }

            closedNodes.Add(actual);
        }

        return null!;
    }
}