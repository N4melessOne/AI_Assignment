using AI_Core.Core.Operator;
using AI_Core.Core.State;

namespace AI_Core.StateRepresentations.ColoredDisksState;

public class ColoredDisksState : AbstractState, IOperatorHandler<Directions, int>
{
    private int size;
    private bool[,] disks;
    private bool[,] goalDisks;

    private ColoredDisksState()
    {
        //for cloning
    }

    public ColoredDisksState(int size = 4)
    {
        this.size = size;
        InitializeStart();
        InitializeGoal();
    }

    public bool[,] Disks
    {
        get
        {
            bool[,] temp = new bool[this.disks.GetLength(0), this.disks.GetLength(1)];
            for (int i = 0; i < this.disks.GetLength(0); i++)
            {
                for (int j = 0; j < this.disks.GetLength(1); j++)
                {
                    temp[i, j] = this.disks[i, j];
                }
            }

            return temp;
        }
    }

    public bool[,] GoalDisks
    {
        get
        {
            bool[,] temp = new bool[this.goalDisks.GetLength(0), this.goalDisks.GetLength(1)];
            for (int i = 0; i < this.goalDisks.GetLength(0); i++)
            {
                for (int j = 0; j < this.goalDisks.GetLength(1); j++)
                {
                    temp[i, j] = this.goalDisks[i, j];
                }
            }

            return temp;
        }
    }

    public int Size
    {
        get => this.size;
    }

    private void InitializeStart()
    {
        this.disks = new bool[this.size, this.size];
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                disks[i, j] = false; //filling the disks with only one of the colors
            }
        }
    }

    private void InitializeGoal()
    {
        //Have to make some changes cause the first one is unsolvable. 
        //Now the solution will be the primary and secondary diagonals of the N*N squares.
        this.goalDisks = new bool[this.size, this.size];
        for (int i = 0; i < this.size; i++)
        {
            for (int j = 0; j < this.size; j++)
            {
                if (i == j || i + j == this.size - 1)
                    goalDisks[i, j] = true;
                else
                    goalDisks[i, j] = false;
                //if (i == 0 || i == this.size - 1 || j == 0 || j == this.size - 1)
                //goalDisks[i, j] = false;
            }
        }
    }

    public override object Clone()
    {
        ColoredDisksState clone = new ColoredDisksState();
        clone.disks = this.disks; //should be the property, but it needs sooooo much time for cloning every time...
        clone.goalDisks =
            this.goalDisks; //here as well, if the property method is used, you can never get a solution, or finish search
        clone.size = this.size;
        return clone;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null)
            return false;
        if (obj is ColoredDisksState)
        {
            ColoredDisksState temp = (obj as ColoredDisksState)!;
            for (int i = 0; i < this.disks.GetLength(0); i++)
            {
                for (int j = 0; j < this.disks.GetLength(1); j++)
                {
                    if (temp.disks[i, j] != this.disks[i, j])
                        return false;
                }
            }

            return true;
        }
        else
            return false;
    }

    public override string ToString()
    {
        int _true = 0, _false = 0;
        for (int i = 0; i < this.disks.GetLength(0); i++)
        {
            for (int j = 0; j < this.disks.GetLength(1); j++)
            {
                if (this.disks[i, j])
                    _true++;
                else
                    _false++;
            }
        }

        return $"There are {_true} red and {_false} blue disks";
    }

    public override bool IsState()
    {
        return true; //Don't know any kind of state that would not be true if the operators are handled correctly.
    }

    public override bool IsGoalState()
    {
        for (int i = 0; i < this.disks.GetLength(0); i++)
        {
            for (int j = 0; j < this.disks.GetLength(1); j++)
            {
                if (this.disks[i, j] != this.goalDisks[i, j])
                    return false;
            }
        }

        return true;
    }

    public bool IsOperator(Directions dir, int index)
    {
        switch (dir)
        {
            case Directions.ROW:
                if (index >= 0 && index < this.size) return true;
                break;
            case Directions.COLUMN:
                if (index >= 0 && index < this.size) return true;
                break;
            default:
                return false;
        }

        return false;
    }

    public bool ApplyOperator(Directions dir, int index)
    {
        switch (dir)
        {
            //swhitching the true and false values in the row or column
            case Directions.ROW:
                for (int i = 0; i < this.size; i++)
                    this.disks[index, i] = !this.disks[index, i];
                if (IsState())
                    return true;
                return false;

            case Directions.COLUMN:
                for (int i = 0; i < this.size; i++)
                    this.disks[i, index] = !this.disks[i, index];
                if (IsState())
                    return true;
                return false;

            default:
                return false;
        }
    }
}