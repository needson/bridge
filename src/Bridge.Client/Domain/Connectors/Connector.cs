using LanguageExt;

namespace Bridge.Client.Domain.Connectors;

public abstract class Connector
{
    public abstract Task<Fin<Unit>> Connect(Connection connection);
}
