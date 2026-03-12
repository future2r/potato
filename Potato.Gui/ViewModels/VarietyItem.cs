using System;
using Potato.Core;

namespace Potato.Gui.ViewModels;

/// <summary>
/// View model that wraps a <see cref="Variety"/> for display in the UI.
/// </summary>
public sealed class VarietyItem(Variety variety)
{
    private readonly Variety _variety = variety ?? throw new ArgumentNullException(nameof(variety));

    public string Name => _variety.Name;
    public DateTime CreatedAt => _variety.CreatedAt;
}
