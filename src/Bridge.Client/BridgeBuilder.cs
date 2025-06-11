namespace Bridge.Client;

public class BridgeBuilder
{
    private readonly BridgeConfiguration _configuration;

    public BridgeBuilder()
    {
        _configuration = new BridgeConfiguration();
    }

    public BridgeBuilder Configure(Action<BridgeConfiguration> configure)
    {
        configure(_configuration);
        return this;
    }

    public Bridge Build()
    {
        return new Bridge(_configuration);
    }
}
