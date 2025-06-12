namespace Bridge.Client;

public class BridgeBuilder
{
    private BridgeConfiguration? _configuration;

    public BridgeConfigurationSettings ReadConfiguration => new(this);
    public BridgeStartConfiguration Start => new(this);
    public BridgeListenConfiguration Listen => new(this);

    internal void SetConfiguration(BridgeConfiguration configuration)
    {
        _configuration = configuration;
    }

    public Bridge Build()
    {
        return new Bridge(_configuration);
    }
}

public class BridgeConfigurationSettings
{
    private readonly BridgeBuilder _bridgeBuilder;

    public BridgeConfigurationSettings(BridgeBuilder bridgeBuilder)
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
        this BridgeConfigurationSettings settings, string filePath)
    {
        return settings.SetConfiguration(new BridgeConfiguration());
    }
}

public class BridgeStartConfiguration
{
    public BridgeStartConfiguration(BridgeBuilder bridgeBuilder)
    {
    }
}

public static class BridgeStartConfigurationExtensions
{
    public static BridgeBuilder FromNode<TNode>(
        this BridgeStartConfiguration startConfiguration, string nodeName,
        Action<TNode> nodeConfigurationCallback)
        where TNode : BridgeNode
    {
    }
}

public class BridgeListenConfiguration
{
    public BridgeListenConfiguration(BridgeBuilder bridgeBuilder)
    {
    }
}

public static class BridgeListenConfigurationExtensions
{
    public static BridgeBuilder ToNode<TNode>(
        this BridgeListenConfiguration listenConfiguration, string nodeName,
        Action<TNode> nodeConfigurationCallback)
        where TNode : BridgeNode
    {
    }
}