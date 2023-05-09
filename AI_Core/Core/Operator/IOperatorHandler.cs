namespace AI_Core.Core.Operator;

public interface IOperatorHandler<T>
{
    bool IsOperator(T t);
    bool ApplyOperator(T t);
}

public interface IOperatorHandler<T, U>
{
    bool IsOperator(T t, U u);
    bool ApplyOperator(T t, U u);
}

public interface IOperatorHandler<T, U, V>
{
    bool IsOperator(T t, U u, V v);
    bool ApplyOperator(T t, U u, V v);
}

public interface IOperatorHandler<T, U, V, W>
{
    bool IsOperator(T t, U u, V v, W w);
    bool ApplyOperator(T t, U u, V v, W w);
}

public interface IOperatorHandler<T, U, V, W, Y>
{
    bool IsOperator(T t, U u, V v, W w, Y y);
    bool ApplyOperator(T t, U u, V v, W w, Y y);
}

public interface IOperatorHandler<T, U, V, W, Y, Z>
{
    bool IsOperator(T t, U u, V v, W w, Y y, Z z);
    bool ApplyOperator(T t, U u, V v, W w, Y y, Z z);
}