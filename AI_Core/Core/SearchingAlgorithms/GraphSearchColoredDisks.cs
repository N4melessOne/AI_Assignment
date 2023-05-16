using AI_Core.Core.Node;

namespace AI_Core.Core.SearchingAlgorithms;

public abstract class GraphSearchColoredDisks
{
    ColoredDisksNode startNode;

    public GraphSearchColoredDisks(int size = 4) =>
        this.startNode = new ColoredDisksNode(size);

    public ColoredDisksNode StartNode
    {
        get => this.startNode;
    }

    public abstract ColoredDisksNode Search();

    public void PrintSolution(ColoredDisksNode terminalNode)
    {
        if (terminalNode == null)
        {
            Console.WriteLine("There is no solution for this problem.");
            return;
        }

        Stack<ColoredDisksNode> solution = new Stack<ColoredDisksNode>();
        ColoredDisksNode node = terminalNode;
        while (node != null)
        {
            solution.Push(node);
            node = node.Parent;
        }

        foreach (ColoredDisksNode n in solution)
        {
            Console.WriteLine(n);
        }
    }

    public Stack<ColoredDisksNode> GetSolution(ColoredDisksNode terminalNode)
    {
        if (terminalNode == null)
            return null!;

        Stack<ColoredDisksNode> solution = new Stack<ColoredDisksNode>();
        ColoredDisksNode node = terminalNode!;
        while (node != null)
        {
            solution.Push(node);
            node = node.Parent;
        }

        return solution;
    }
}