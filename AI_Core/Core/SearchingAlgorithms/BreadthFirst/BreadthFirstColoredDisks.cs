using AI_Core.Core.Node;

namespace AI_Core.Core.SearchingAlgorithms.BreadthFirst;

public class BreadthFirstColoredDisks : GraphSearchColoredDisks
{
    private Queue<ColoredDisksNode> openNodes; //the difference between the two algorithms
    //is the data structure for the database of nodes.
    private List<ColoredDisksNode> closedNodes;
    private bool circleDetection;

    public BreadthFirstColoredDisks(bool circleDetection, int size = 4)
    {
        openNodes = new Queue<ColoredDisksNode>();
        openNodes.Enqueue(new ColoredDisksNode(size)); //starting node
        closedNodes = new List<ColoredDisksNode>();
        this.circleDetection = circleDetection;
    }

    public BreadthFirstColoredDisks(int size = 4) : this(true, size)
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
            ColoredDisksNode actual = openNodes.Dequeue();
            List<ColoredDisksNode> children = actual.Extend();
            foreach (ColoredDisksNode child in children)
            {
                if (child.IsTerminalNode)
                    return child;
                openNodes.Enqueue(child);
            }
        }

        return null!;
    }

    private ColoredDisksNode SearchTerminalNodeWithCircleDetection()
    {
        //TODO: iterate through the current items in the list, then extend the nodes
        while (openNodes.Count != 0)
        {
            ColoredDisksNode actual = openNodes.Dequeue();
            List<ColoredDisksNode> children = actual.Extend();
            foreach (ColoredDisksNode child in children)
            {
                if (child.IsTerminalNode)
                    return child;
                if (!closedNodes.Contains(child) && !openNodes.Contains(child))
                    openNodes.Enqueue(child);
            }

            closedNodes.Add(actual);
        }

        return null!;
    }
}