using LanguageExt;
using System.Reactive.Concurrency;
using System.Reactive.Linq;

namespace Bridge.Client.Domain;

public class Connection
{
    private IObservable<BridgeConnectorValue> _trigger;
    private Action<BridgeConnectorValue> _handler;

    public Connection()
    {
        _trigger = Observable.Empty<BridgeConnectorValue>();
        _handler = _ => { };
    }

    public Task<Fin<Unit>> SetTrigger(IObservable<BridgeConnectorValue> trigger)
    {
        _trigger = trigger;

        return _trigger
            .ObserveOn(new NewThreadScheduler())
            .Consume(Handle).Run()
            .AsTask();
    }

    public void SetHandler(Action<BridgeConnectorValue> handler)
    {
        _handler = handler;
    }

    private Aff<Unit> Handle(BridgeConnectorValue input)
    {
        return Prelude.Aff(() =>
        {
            _handler.Invoke(input);
            return new ValueTask<Unit>(Unit.Default);
        });
    }
}
