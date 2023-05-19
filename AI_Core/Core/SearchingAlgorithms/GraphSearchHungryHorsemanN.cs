using AI_Core.Core.Node;
using AI_Core.StateRepresentations.HungryHorsemanNState;

namespace AI_Core.Core.SearchingAlgorithms;

public  abstract class GraphSearchHungryHorsemanN
{
    HungryHorsemanNNode startNode;

    public GraphSearchHungryHorsemanN(int size = 3) =>
        this.startNode = new HungryHorsemanNNode(size);

    public HungryHorsemanNNode StartNode
    {
        get => this.startNode;
    }

    public abstract HungryHorsemanNNode Search();

    public void PrintSolution(HungryHorsemanNNode terminalNode)
    {
        if (terminalNode == null)
        {
            Console.WriteLine("There is no solution for this problem.");
            return;
        }

        Stack<HungryHorsemanNNode> solution = new Stack<HungryHorsemanNNode>();
        HungryHorsemanNNode node = terminalNode;
        while (node != null)
        {
            solution.Push(node);
            node = node.Parent;
        }

        foreach (HungryHorsemanNNode n in solution)
        {
            Console.WriteLine(n);
        }
    }

    public Stack<HungryHorsemanNNode> GetSolution(HungryHorsemanNNode terminalNode)
    {
        if (terminalNode == null)
            return null!;

        Stack<HungryHorsemanNNode> solution = new Stack<HungryHorsemanNNode>();
        HungryHorsemanNNode node = terminalNode!;
        while (node != null)
        {
            solution.Push(node);
            node = node.Parent;
        }

        return solution;

    }
}