namespace Potato.Core;

/// <summary>
/// Represents a potato variety with its name and creation date.
/// </summary>
public sealed record Variety(string Name, DateTime CreatedAt)
{
    /// <summary>
    /// Creates a new variety with the current date.
    /// </summary>
    public Variety(string name) : this(name, DateTime.Now) { }

    public string Name { get; init; } = Name ?? throw new ArgumentNullException(nameof(Name));
}
