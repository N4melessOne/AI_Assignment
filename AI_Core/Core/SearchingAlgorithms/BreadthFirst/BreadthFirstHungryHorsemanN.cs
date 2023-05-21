using AI_Core.Core.Node;

namespace AI_Core.Core.SearchingAlgorithms.BreadthFirst;

public class BreadthFirstHungryHorsemanN : GraphSearchHungryHorsemanN
{
    private Queue<HungryHorsemanNNode> openNodes; //the difference between the two algorithms
    //is the data structure for the database of nodes.
    private List<HungryHorsemanNNode> closedNodes;
    private bool circleDetection;

    public BreadthFirstHungryHorsemanN(bool circleDetection, int size = 3)
    {
        openNodes = new Queue<HungryHorsemanNNode>();
        openNodes.Enqueue(new HungryHorsemanNNode(size)); //starting node
        closedNodes = new List<HungryHorsemanNNode>();
        this.circleDetection = circleDetection;
    }

    public BreadthFirstHungryHorsemanN(int size = 3) : this(true, size)
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
            HungryHorsemanNNode actual = openNodes.Dequeue();
            List<HungryHorsemanNNode> children = actual.GetAllChildNodes();
            foreach (HungryHorsemanNNode child in children)
            {
                if (child.IsTerminalNode)
                    return child;
                openNodes.Enqueue(child);
            }
        }

        return null!;
    }

    private HungryHorsemanNNode SearchTerminalNodeWithCircleDetection()
    {
        //TODO: iterate through the current items in the list, then extend the nodes
        while (openNodes.Count != 0)
        {
            HungryHorsemanNNode actual = openNodes.Dequeue();
            List<HungryHorsemanNNode> children = actual.GetAllChildNodes();
            foreach (HungryHorsemanNNode child in children)
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