using AI_Core.Core.Node;

namespace AI_Core.Core.SearchingAlgorithms.DepthFirst;

public class DepthFirstHungryHorsemanN : GraphSearchHungryHorsemanN
{
    private Stack<HungryHorsemanNNode> openNodes;
    private List<HungryHorsemanNNode> closedNodes;
    private bool circleDetection;

    public DepthFirstHungryHorsemanN(bool circleDetection, int size = 3)
    {
        openNodes = new Stack<HungryHorsemanNNode>();
        openNodes.Push(new HungryHorsemanNNode(size));
        closedNodes = new List<HungryHorsemanNNode>();
        this.circleDetection = circleDetection;
    }

    public DepthFirstHungryHorsemanN(int size = 3) : this(true, size)
    {
    }

    public override HungryHorsemanNNode Search()
    {
        return this.circleDetection
            ? SearchTerminalNodeWithCircleDetection()
            : SearchTerminalNodeWithoutCircleDetection();
    }

    private HungryHorsemanNNode SearchTerminalNodeWithoutCircleDetection()
    {
        while (openNodes.Count != 0)
        {
            HungryHorsemanNNode actual = openNodes.Pop();
            List<HungryHorsemanNNode> children = actual.GetAllChildNodes();
            foreach (HungryHorsemanNNode child in children)
            {
                if (child.IsTerminalNode)
                    return child;
                openNodes.Push(child);
            }
        }

        return null!;
    }

    private HungryHorsemanNNode SearchTerminalNodeWithCircleDetection()
    {
        while (openNodes.Count != 0)
        {
            HungryHorsemanNNode actual = openNodes.Pop();
            List<HungryHorsemanNNode> children = actual.GetAllChildNodes();
            foreach (HungryHorsemanNNode child in children)
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