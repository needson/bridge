namespace Bridge.Client;

internal static class Program
{
    public static async Task Main(string[] args)
    {
        const string json = """
           {
            "Name": "Alex",
            "Initials": "AN",
            "DateOfBirth": "13-05-1988"
           }
           """;

        var bridgeBuilder = new BridgeBuilder();
        
        var bridge = bridgeBuilder
            .Configure(configuration =>
            {
                configuration.NodeConfigurationPath = "bridge.json";
            })
            .Build();
        
        var jsonInputNode = bridge.GetNode<JsonInputNode>("JsonInput01");
        var jsonOutputNode = bridge.GetNode<JsonOutputNode>("JsonOutputNode01");

        jsonInputNode.SetInput(x => x.Json, json);

        await bridge.Run();
        
        var jsonOutput = jsonOutputNode.GetOutput(x => x.Json);

        Console.WriteLine(jsonOutput);
    }
}
