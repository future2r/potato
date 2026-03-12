using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Potato.Core;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;

namespace Potato.Gui.ViewModels;

/// <summary>
/// ViewModel for the main application window.
/// Manages the display and manipulation of potato varieties.
/// </summary>
public sealed partial class MainWindowViewModel : ViewModelBase
{
    private readonly Database _database;

    /// <summary>
    /// Gets or sets the potato variety input by the user.
    /// </summary>
    [ObservableProperty]
    private string _varietyNameToAdd;

    /// <summary>
    /// Gets the read-only collection of potato varieties that can be displayed in the UI.
    /// </summary>
    public ReadOnlyObservableCollection<VarietyItem> Varieties { get; }

    private readonly ObservableCollection<VarietyItem> _varieties;

    /// <summary>
    /// Gets the observable collection of selected potato varieties from the list.
    /// </summary>
    [ObservableProperty]
    private ObservableCollection<VarietyItem> _selectedVarieties;

    /// <summary>
    /// Gets the current operating system information.
    /// </summary>
    public string OperatingSystem { get; }

    /// <summary>
    /// Gets the current runtime information.
    /// </summary>
    public string Runtime { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="MainWindowViewModel"/> class.
    /// Loads potato varieties from the database and initializes system information.
    /// </summary>
    public MainWindowViewModel()
    {
        // Initialize the database
        _database = new Database();

        // Initialize user input
        _varietyNameToAdd = string.Empty;

        // Load varieties from database and subscribe to changes
        _varieties = new ObservableCollection<VarietyItem>(
            _database.GetAll().Select(static variety => new VarietyItem(variety)));
        Varieties = new ReadOnlyObservableCollection<VarietyItem>(_varieties);
        _varieties.CollectionChanged += (s, e) =>
        {
            AddVarietyCommand.NotifyCanExecuteChanged();
            RemoveAllCommand.NotifyCanExecuteChanged();
        };

        // Initialize selection tracking
        _selectedVarieties = new ObservableCollection<VarietyItem>();
        _selectedVarieties.CollectionChanged += (s, e) => RemoveCommand.NotifyCanExecuteChanged();

        // Capture system information
        OperatingSystem = GetOperatingSystemInfo();
        Runtime = GetRuntimeInfo();
    }

    /// <summary>
    /// Gets the current operating system description and architecture.
    /// </summary>
    /// <returns>A formatted string containing the OS description and architecture.</returns>
    private static string GetOperatingSystemInfo()
    {
        return $"{RuntimeInformation.OSDescription} ({RuntimeInformation.OSArchitecture})";
    }

    /// <summary>
    /// Gets the current runtime framework description and identifier.
    /// </summary>
    /// <returns>A formatted string containing the framework description and identifier.</returns>
    private static string GetRuntimeInfo()
    {
        return $"{RuntimeInformation.FrameworkDescription} ({RuntimeInformation.RuntimeIdentifier})";
    }

    /// <summary>
    /// Called when the <see cref="VarietyNameToAdd"/> property changes.
    /// Notifies the AddVarietyCommand to re-evaluate its can-execute state.
    /// </summary>
    /// <param name="value">The new value of the VarietyNameToAdd property.</param>
    partial void OnVarietyNameToAddChanged(string value)
    {
        AddVarietyCommand.NotifyCanExecuteChanged();
    }

    /// <summary>
    /// Determines whether a new variety can be added.
    /// Returns false if the input is empty, whitespace-only, or already exists in the list.
    /// </summary>
    /// <returns>True if the variety can be added; otherwise, false.</returns>
    private bool CanAddVariety()
    {
        var trimmed = VarietyNameToAdd?.Trim() ?? string.Empty;
        return !string.IsNullOrEmpty(trimmed) && !_varieties.Any(variety => variety.Name == trimmed);
    }

    /// <summary>
    /// Adds the trimmed input variety to the collection if it is valid and not already present.
    /// Clears the input field after a successful add.
    /// </summary>
    [RelayCommand(CanExecute = nameof(CanAddVariety))]
    private void AddVariety()
    {
        // Validate input
        var trimmed = VarietyNameToAdd?.Trim() ?? string.Empty;
        if (!string.IsNullOrEmpty(trimmed) && !_varieties.Any(variety => variety.Name == trimmed))
        {
            // Add to collection and clear the input field
            _varieties.Add(new VarietyItem(new Variety(trimmed)));
            VarietyNameToAdd = string.Empty;
        }
    }

    /// <summary>
    /// Determines whether selected varieties can be removed.
    /// Returns false if no items are selected.
    /// </summary>
    /// <returns>True if items can be removed; otherwise, false.</returns>
    private bool CanRemove()
    {
        return SelectedVarieties.Count > 0;
    }

    /// <summary>
    /// Removes the selected varieties from the collection.
    /// </summary>
    [RelayCommand(CanExecute = nameof(CanRemove))]
    private void Remove()
    {
        // Create a copy of selected items to avoid modifying collection during iteration
        var itemsToRemove = SelectedVarieties.ToList();

        // Remove each selected item from the main collection
        foreach (var item in itemsToRemove)
        {
            _varieties.Remove(item);
        }

        // Clear the selection
        SelectedVarieties.Clear();
    }

    /// <summary>
    /// Determines whether all varieties can be removed.
    /// Returns false if the variety collection is empty.
    /// </summary>
    /// <returns>True if all items can be removed; otherwise, false.</returns>
    private bool CanRemoveAll()
    {
        return _varieties.Count > 0;
    }

    /// <summary>
    /// Removes all varieties from the collection.
    /// </summary>
    [RelayCommand(CanExecute = nameof(CanRemoveAll))]
    private void RemoveAll()
    {
        // Clear both the varieties collection and the selection
        _varieties.Clear();
        SelectedVarieties.Clear();
    }
}
