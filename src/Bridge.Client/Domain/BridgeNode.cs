using Bridge.Client.Domain.Connectors;
using System.Reactive.Linq;

namespace Bridge.Client.Domain;

public class BridgeConnectorValue
{
    public string Value { get; set; }
}

public class InputNode
{
    public Connector Connector => InputConnector.Create(Handler);

    private static void Handler(BridgeConnectorValue input)
    {
        Console.WriteLine(input.Value);
    }
}

public class OutputNode
{
    public Connector Connector => OutputConnector.Create(Output);

    private static IObservable<BridgeConnectorValue> Output
        => Observable
            .Interval(TimeSpan.FromSeconds(1))
            .Select(_ => Produce());

    private static BridgeConnectorValue Produce()
    {
        return new BridgeConnectorValue { Value = $"Current time is {DateTime.Now}" };
    }
}
