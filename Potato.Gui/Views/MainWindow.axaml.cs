using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Threading;
using Potato.Gui.ViewModels;
using System;
using System.Linq;

namespace Potato.Gui.Views;

public sealed partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void VarietiesList_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.Key == Key.Delete && sender is DataGrid && DataContext is MainWindowViewModel viewModel)
        {
            if (viewModel.RemoveCommand.CanExecute(null))
            {
                var indexBefore = VarietiesList.SelectedIndex;
                viewModel.RemoveCommand.Execute(null);
                TriggerSelectionChange(indexBefore);
                e.Handled = true;
            }
        }
    }

    private void RemoveButton_Click(object? sender, RoutedEventArgs e)
    {
        TriggerSelectionChange(VarietiesList.SelectedIndex);
    }

    private void VarietiesList_SelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (sender is DataGrid dataGrid && DataContext is MainWindowViewModel viewModel)
        {
            viewModel.SelectedVarieties.Clear();

            foreach (var selected in dataGrid.SelectedItems.OfType<VarietyItem>())
            {
                viewModel.SelectedVarieties.Add(selected);
            }
        }
    }

    private void TriggerSelectionChange(int indexBefore)
    {
        Dispatcher.UIThread.Post(() =>
        {
            if (DataContext is not MainWindowViewModel viewModel || viewModel.Varieties.Count <= 0)
            {
                return;
            }

            var newIndex = Math.Max(0, Math.Min(indexBefore, viewModel.Varieties.Count - 1));
            var itemToSelect = viewModel.Varieties[newIndex];

            // Trigger a selection change, even if the element stays the same
            VarietiesList.SelectedItem = null;
            VarietiesList.SelectedItem = itemToSelect;
        }, DispatcherPriority.Input);
    }
}