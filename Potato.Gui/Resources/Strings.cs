using System.Collections.Generic;
using System.Globalization;

namespace Potato.Gui.Resources;

/// <summary>
/// Provides localized string resources with simple dictionary-based lookup.
/// Supports English and German via inline dictionaries.
/// </summary>
public static class Strings
{
    private static readonly Dictionary<string, Dictionary<string, string>> Resources = new()
    {
        {
            "en-US", new()
            {
                { "window_title", "Potato" },
                { "operating_system_label", "Operating System:" },
                { "runtime_label", "Runtime:" },
                { "new_variety_label", "New variety:" },
                { "add_button", "Add" },
                { "varieties_label", "Varieties:" },
                { "remove_button", "Remove" },
                { "remove_all_button", "Remove All" }
            }
        },
        {
            "de-DE", new()
            {
                { "window_title", "Potato" },
                { "operating_system_label", "Betriebssystem:" },
                { "runtime_label", "Laufzeit:" },
                { "new_variety_label", "Neue Sorte:" },
                { "add_button", "Hinzufügen" },
                { "varieties_label", "Sorten:" },
                { "remove_button", "Entfernen" },
                { "remove_all_button", "Alles entfernen" }
            }
        }
    };

    private static string _currentCulture = CultureInfo.CurrentCulture.Name.StartsWith("de") ? "de-DE" : "en-US";

    /// <summary>
    /// Gets or sets the current culture for localization.
    /// </summary>
    public static string CurrentCulture
    {
        get => _currentCulture;
        set => _currentCulture = value;
    }

    /// <summary>
    /// Gets a localized string by key, returns key as fallback if not found.
    /// </summary>
    private static string GetString(string key)
    {
        if (Resources.TryGetValue(_currentCulture, out var culture) && culture.TryGetValue(key, out var value))
        {
            return value;
        }
        return $"[{key}]"; // Fallback for missing keys
    }

    public static string WindowTitle => GetString("window_title");
    public static string OperatingSystemLabel => GetString("operating_system_label");
    public static string RuntimeLabel => GetString("runtime_label");
    public static string NewVarietyLabel => GetString("new_variety_label");
    public static string AddButton => GetString("add_button");
    public static string VarietiesLabel => GetString("varieties_label");
    public static string RemoveButton => GetString("remove_button");
    public static string RemoveAllButton => GetString("remove_all_button");
}
