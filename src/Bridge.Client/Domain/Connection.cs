namespace Bridge.Client.Domain;

public class Connection
{
    public Connection(Connector input, Connector output)
    {
        output.Connect(input);
    }
}
