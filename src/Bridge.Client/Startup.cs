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
        return Task.Factory.StartNew(() =>
        {
            var bridge = new BridgeBuilder()
                .ReadConfiguration.FromJsonFile(_options.BridgeConfigurationFile)
                .Start.FromNode<JsonInputNode>(_options.BridgeStartNodeName, ConfigureInputNode)
                .Listen.ToNode<JsonOutputNode>(_options.BridgeResultNodeName, ConfigureOutputNode)
                .Build();

            bridge.Start();
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
