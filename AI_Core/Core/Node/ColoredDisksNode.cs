using AI_Core.StateRepresentations.ColoredDisksState;

namespace AI_Core.Core.Node;

public class ColoredDisksNode
{
    ColoredDisksState state;
    int depth;
    ColoredDisksNode parent;

    public ColoredDisksNode(int size = 4)
    {
        this.state = new ColoredDisksState(size);
        this.depth = 0;
        this.parent = null!;
    }

    public ColoredDisksNode(ColoredDisksNode parent) 
    {
        this.state = (ColoredDisksState)parent.state.Clone();
        this.depth = parent.depth + 1;
        this.parent = parent;
    }

    public ColoredDisksNode Parent
    {
        get => this.parent;
    }

    public ColoredDisksState State
    {
        get => this.state;
    }

    public int Depth
    {
        get => this.depth;
    }

    public bool IsTerminalNode
    {
        get => this.state.IsGoalState();
    }

    public List<ColoredDisksNode> Extend()
    {
        List<ColoredDisksNode> children = new List<ColoredDisksNode>();
        //Get all the possible steps from current position
        foreach (Directions dir in Enum.GetValues(typeof(Directions)))
        {
            for (int i = 0; i < this.state.Size; i++)
            {
                ColoredDisksNode childNode = new ColoredDisksNode(this);
                if (childNode.state.ApplyOperator(dir, i))
                    children.Add(childNode);
            }
        }
        return children;
    }

    public override bool Equals(object? obj)
    {
        ColoredDisksNode other = (ColoredDisksNode)obj!;
        return this.state.Equals(other.state);
    }

    public override string ToString()
    {
        return state.ToString()!;
    }
}