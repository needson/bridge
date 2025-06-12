namespace Bridge.Client;

public abstract class BridgeNode
{
}

public abstract class BridgeNode<TInputs, TOutputs> : BridgeNode
    where TInputs : BridgeNodeInputs
{
    public void RegisterInputsSource(Func<TInputs> inputsSource)
    {
    }

    public void RegisterOutputsListener(Action<TOutputs> outputsListener)
    {
    }
}

public class JsonInputNode :
    BridgeNode<JsonInputNodeInputs, JsonInputNodeOutputs>
{
}

public abstract class BridgeNodeInputs
{
}

public abstract class BridgeNodeOutputs
{
}

public class JsonInputNodeInputs : BridgeNodeInputs
{
    public string Json { get; set; }
}

public class JsonInputNodeOutputs : BridgeNodeOutputs
{
}

public class JsonOutputNode :
    BridgeNode<JsonOutputNodeInputs, JsonOutputNodeOutputs>
{
}

public class JsonOutputNodeInputs : BridgeNodeInputs
{
    public string Json { get; set; }
}

public class JsonOutputNodeOutputs : BridgeNodeOutputs
{
    public string Json { get; set; }
}