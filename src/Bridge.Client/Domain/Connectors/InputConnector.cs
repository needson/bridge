using LanguageExt;

namespace Bridge.Client.Domain.Connectors;

public class InputConnector : Connector
{
    private readonly Action<BridgeConnectorValue> _handler;

    public InputConnector(Action<BridgeConnectorValue> handler)
    {
        _handler = handler;
    }

    public static InputConnector Create(Action<BridgeConnectorValue> handler)
    {
        return new InputConnector(handler);
    }

    public override Task<Fin<Unit>> Connect(Connection connection)
    {
        connection.SetHandler(_handler);
        return Task.FromResult(Fin<Unit>.Succ(Unit.Default));
    }
}
