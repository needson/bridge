using LanguageExt;

namespace Bridge.Client.Domain.Connectors;

public class OutputConnector : Connector
{
    private readonly IObservable<BridgeConnectorValue> _trigger;

    public OutputConnector(IObservable<BridgeConnectorValue> trigger)
    {
        _trigger = trigger;
    }

    public static OutputConnector Create(IObservable<BridgeConnectorValue> trigger)
    {
        return new OutputConnector(trigger);
    }

    public override Task<Fin<Unit>> Connect(Connection connection)
    {
        return connection.SetTrigger(_trigger);
    }
}
