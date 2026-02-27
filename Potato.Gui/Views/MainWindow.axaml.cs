using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Threading;
using System;

namespace Potato.Gui.Views;

public sealed partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void VarietiesList_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.Key == Key.Delete && sender is ListBox listBox && DataContext is Potato.Gui.ViewModels.MainWindowViewModel viewModel)
        {
            if (viewModel.RemoveCommand.CanExecute(null))
            {
                int indexBefore = listBox.SelectedIndex;
                viewModel.RemoveCommand.Execute(null);
                e.Handled = true;

                Dispatcher.UIThread.Post(() =>
                {
                    if (listBox.ItemCount > 0)
                    {
                        // Try to select the item at the same index, or the last item if out of range
                        int newIndex = Math.Min(indexBefore, listBox.ItemCount - 1);
                        listBox.SelectedIndex = newIndex;


                        var container = listBox.ContainerFromIndex(listBox.SelectedIndex);
                        container?.Focus();
                    }
                    listBox.Focus();
                }, DispatcherPriority.Input);
            }
        }
    }
}