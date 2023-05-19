using AI_Core.StateRepresentations.HungryHorsemanNState;

namespace AI_Core.Core.Node;

public class HungryHorsemanNNode
{
    HungryHorsemanN state;
    int depth;
    HungryHorsemanNNode parent;

    public HungryHorsemanNNode(int size = 3)
    {
        this.state = new HungryHorsemanN(size);
        this.depth = 0;
        this.parent = null!;
    }

    public HungryHorsemanNNode(HungryHorsemanNNode parent) //generate with operator
    {
        this.state = (HungryHorsemanN)parent.state.Clone();
        this.depth = parent.depth + 1;
        this.parent = parent;
    }

    public HungryHorsemanNNode Parent
    {
        get => this.parent;
    }

    public HungryHorsemanN State
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

    public List<HungryHorsemanNNode> GetAllChildNodes()
    {
        List<HungryHorsemanNNode> children = new List<HungryHorsemanNNode>();
        for (int i = -2; i < 3; i++)
        {
            for (int j = -2; j < 3; j++)
            {
                int diffX = i;
                int diffY = j;
                HungryHorsemanNNode childNode = new HungryHorsemanNNode(this);
                if (childNode.state.ApplyOperator(diffX, diffY))
                    children.Add(childNode);
            }
        }
        return children;
    }

    public override bool Equals(object? obj)
    {
        HungryHorsemanNNode other = (HungryHorsemanNNode)obj!;
        return this.state.Equals(other.state);
    }

    public override string ToString()
    {
        return state.ToString()!;
    }
}