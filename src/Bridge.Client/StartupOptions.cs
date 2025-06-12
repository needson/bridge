namespace Bridge.Client;

internal class StartupOptions
{
    public required string BridgeConfigurationFile { get; set; }
    public required string BridgeStartNodeName { get; set; }
    public required string BridgeResultNodeName { get; set; }
}
