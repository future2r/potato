namespace Potato.Core;

/// <summary>
/// Provides access to the potato variety collection.
/// </summary>
public sealed class Database
{
    private readonly List<Variety> _varieties;

    public Database()
    {
        _varieties =
        [
            new Variety("Russet Burbank"),
            new Variety("Yukon Gold"),
            new Variety("Red Bliss"),
            new Variety("Kennebec"),
            new Variety("Maris Piper")
        ];
    }

    /// <summary>
    /// Returns all potato varieties.
    /// </summary>
    public IReadOnlyList<Variety> GetAll()
    {
        return _varieties.AsReadOnly();
    }
}
