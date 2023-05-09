namespace AI_Core.Core.Operator;

public interface IProxyOperator<T>
{
    int GetNrOfOperators();
    bool ApplyOperator(T t);
}