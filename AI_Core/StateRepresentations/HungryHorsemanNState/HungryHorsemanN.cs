using AI_Core.Core.Operator;
using AI_Core.Core.State;

namespace AI_Core.StateRepresentations.HungryHorsemanNState;

public class HungryHorsemanN : AbstractState, IOperatorHandler<int, int>
{
    private int x, y, size;

    public int X
    {
        get => x;
    }

    public int Y
    {
        get => y;
    }

    public int Size
    {
        get => size;
    }

    public HungryHorsemanN(int size)
    {
        x = 0;
        y = 0;
        this.size = size;
    }

    public override bool IsGoalState() => x == size - 1 && y == size - 1;

    public override bool IsState() => 0 <= x && 0 <= y &&
                                      size > x && size > y; //can't be equal to the size!!

    public bool IsOperator(int diffX, int diffY)
    {
        return Math.Abs(diffX) + Math.Abs(diffY) == 3;
    }

    
    public bool ApplyOperator(int diffX, int diffY)
    {
        if (!IsOperator(diffX, diffY))
            return false;
        x += diffX;
        y += diffY;
        if (IsState())
            return true;
        x -= diffX;
        y -= diffY;
        return false;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null)
        {
            return false;
        }

        if (obj is HungryHorsemanN)
        {
            HungryHorsemanN other = (obj as HungryHorsemanN)!;
            return other.X == this.X && other.Y == this.Y
                                     && other.size == this.size;
        }
        else
            return false;
    }

    public override string ToString()
    {
        return $"Current position: ({this.X}, {this.Y})";
    }
}