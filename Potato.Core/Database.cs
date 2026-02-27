namespace Potato.Core;

public sealed class Database
{
    private readonly List<string> _varieties;

    public Database()
    {
        _varieties =
        [
            "Russet Burbank",
            "Yukon Gold",
            "Red Bliss",
            "Kennebec",
            "Maris Piper"
        ];
    }

    public IReadOnlyList<string> GetPotatoVarieties()
    {
        return _varieties.AsReadOnly();
    }
}
