using Bridge.Client.Builder;
using Bridge.Client.Domain;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Bridge.Client;

internal class Startup
{
    private readonly ILogger _logger;
    private readonly StartupOptions _options;

    public Startup(IOptions<StartupOptions> options, ILogger<Startup> logger)
    {
        _logger = logger;
        _options = options.Value;
    }

    public Task Run()
    {
        return Task.Factory.StartNew(async () =>
        {
            //var bridge = new BridgeBuilder()
            //    .ReadConfiguration.FromJsonFile(_options.BridgeConfigurationFile)
            //    .Configure.Node<JsonInputNode>(_options.BridgeStartNodeName, ConfigureInputNode)
            //    .Configure.Node<JsonOutputNode>(_options.BridgeResultNodeName, ConfigureOutputNode)
            //    .Build();

            //bridge.Start();

            var outputNode = new OutputNode();
            var inputNode = new InputNode();

            var connection = new Connection();

            await inputNode.Connector.Connect(connection);
            await outputNode.Connector.Connect(connection);
        });
    }

    private static void ConfigureInputNode(JsonInputNode node)
    {
        node.RegisterInputsSource(() => new JsonInputNodeInputs
        {
            Json = """
               {
                "Name": "Alex",
                "Initials": "AN",
                "DateOfBirth": "13-05-1988"
               }
               """
        });
    }

    private void ConfigureOutputNode(JsonOutputNode node)
    {
        node.RegisterOutputsListener(outputs =>
        {
            _logger.LogInformation(outputs.Json);
        });
    }
}
