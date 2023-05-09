namespace AI_Core.Core.State;

public abstract class AbstractState : ICloneable
{
    public abstract bool IsState();
    public abstract bool IsGoalState();

    public virtual object Clone() => this.MemberwiseClone();
    public override bool Equals(object? obj) => false;
    public override int GetHashCode() => base.GetHashCode();
}