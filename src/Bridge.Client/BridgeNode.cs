using System.Linq.Expressions;

namespace Bridge.Client;

public abstract class BridgeNode
{
}

public abstract class BridgeNode<TInputs> : BridgeNode
    where TInputs : BridgeNodeInputs
{
    public void SetInput<T>(Expression<Func<TInputs, T>> inputSelector, T inputValue)
    {
    }

    public T GetOutput<T>(Expression<Func<TInputs, T>> outputSelector)
    {
    }
}

public class JsonInputNode : BridgeNode<JsonInputNodeInputs>
{
}

public abstract class BridgeNodeInputs
{
}

public class JsonInputNodeInputs : BridgeNodeInputs
{
    public string Json { get; set; }
}

public class JsonOutputNode : BridgeNode<JsonOutputNodeOutputs>
{
}

public class JsonOutputNodeOutputs : BridgeNodeInputs
{
    public string Json { get; set; }
}