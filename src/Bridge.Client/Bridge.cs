namespace Bridge.Client;

public class Bridge
{
    public Bridge(BridgeConfiguration configuration)
    {
    }

    public T GetNode<T>(string name) where T : BridgeNode
    {
        return new JsonInputNode();
    }
}
