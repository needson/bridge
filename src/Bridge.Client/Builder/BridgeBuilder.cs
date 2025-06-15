namespace Bridge.Client.Builder;

public class BridgeBuilder
{
    private BridgeConfiguration? _configuration;
    private readonly BridgeNodeConfigurationProvider _bridgeNodeConfigurationProvider;

    public BridgeBuilder()
    {
        _bridgeNodeConfigurationProvider = new BridgeNodeConfigurationProvider();
    }

    public BridgeConfigurationBuilder ReadConfiguration => new(this);

    public BridgeNodeConfigurationBuilder Configure => new(this);

    internal void SetConfiguration(BridgeConfiguration configuration)
    {
        _configuration = configuration;
    }

    internal BridgeBuilder AddNodeConfiguration<TNode>(
        string nodeName, Action<TNode> nodeConfigurationCallback)
        where TNode : BridgeNode
    {
        _bridgeNodeConfigurationProvider
            .StoreNodeConfigurationCallback(nodeName, nodeConfigurationCallback);
        return this;
    }

    public Bridge Build()
    {
        return new Bridge(_configuration);
    }
}

public class BridgeNodeConfigurationProvider
{
    private readonly Dictionary<string, IList<ConfigurationCallback>> _configurationCallbacks;

    public BridgeNodeConfigurationProvider()
    {
        _configurationCallbacks = new Dictionary<string, IList<ConfigurationCallback>>();
    }

    public void StoreNodeConfigurationCallback<TNode>(
        string nodeName, Action<TNode> nodeConfigurationCallback)
        where TNode : BridgeNode
    {
        if (!_configurationCallbacks.ContainsKey(nodeName))
            _configurationCallbacks.Add(nodeName, []);

        _configurationCallbacks[nodeName]
            .Add(new ConfigurationCallback<TNode>(nodeConfigurationCallback));
    }

    public IEnumerable<Action<TNode>> GetNodeConfigurationCallbacks<TNode>(string nodeName)
        where TNode : BridgeNode
    {
        var callback = _configurationCallbacks[nodeName]
            .Cast<ConfigurationCallback<TNode>>()
            .Select(x => x.GetCallback());
        return callback;
    }

    private abstract class ConfigurationCallback;

    private sealed class ConfigurationCallback<TNode> : ConfigurationCallback
        where TNode : BridgeNode
    {
        private readonly Action<TNode> _nodeConfigurationCallback;

        public ConfigurationCallback(Action<TNode> nodeConfigurationCallback)
        {
            _nodeConfigurationCallback = nodeConfigurationCallback;
        }

        public Action<TNode> GetCallback()
        {
            return _nodeConfigurationCallback;
        }
    }
}

public class BridgeConfigurationBuilder
{
    private readonly BridgeBuilder _bridgeBuilder;

    public BridgeConfigurationBuilder(BridgeBuilder bridgeBuilder)
    {
        _bridgeBuilder = bridgeBuilder;
    }

    internal BridgeBuilder SetConfiguration(BridgeConfiguration configuration)
    {
        _bridgeBuilder.SetConfiguration(configuration);
        return _bridgeBuilder;
    }
}

public static class BridgeConfigurationSettingsExtensions
{
    public static BridgeBuilder FromJsonFile(
        this BridgeConfigurationBuilder builder, string filePath)
    {
        return builder.SetConfiguration(new BridgeConfiguration());
    }
}

public class BridgeNodeConfigurationBuilder
{
    private readonly BridgeBuilder _bridgeBuilder;

    public BridgeNodeConfigurationBuilder(BridgeBuilder bridgeBuilder)
    {
        _bridgeBuilder = bridgeBuilder;
    }

    internal BridgeBuilder AddNodeConfiguration<TNode>(
        string nodeName, Action<TNode> nodeConfigurationCallback)
        where TNode : BridgeNode
    {
        _bridgeBuilder.AddNodeConfiguration(nodeName, nodeConfigurationCallback);
        return _bridgeBuilder;
    }
}

public static class BridgeNodeConfigurationExtensions
{
    public static BridgeBuilder Node<TNode>(
        this BridgeNodeConfigurationBuilder nodeConfigurationBuilder, string nodeName,
        Action<TNode> nodeConfigurationCallback)
        where TNode : BridgeNode
    {
        return nodeConfigurationBuilder
            .AddNodeConfiguration(nodeName, nodeConfigurationCallback);
    }
}
