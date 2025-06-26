using LanguageExt;

namespace Bridge.Client.Domain;

public class Connector
{
    private readonly IObservable<BridgeConnectorValue>? _trigger;
    private readonly Action<BridgeConnectorValue>? _handler;

    public Connector(IObservable<BridgeConnectorValue> trigger)
    {
        _trigger = trigger;
    }

    public Connector(Action<BridgeConnectorValue> handler)
    {
        _handler = handler;
    }

    public static Connector Create(Action<BridgeConnectorValue> handler)
    {
        return new Connector(handler);
    }

    public static Connector Create(IObservable<BridgeConnectorValue> trigger)
    {
        return new Connector(trigger);
    }

    public void Connect(Connector connector)
    {
        _ = connector._trigger.Consume(Handle).Run();
    }

    private Aff<Unit> Handle(BridgeConnectorValue input)
    {
        return Prelude.Aff(() =>
        {
            _handler?.Invoke(input);
            return new ValueTask<Unit>(Unit.Default);
        });
    }
}
