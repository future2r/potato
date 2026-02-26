using Potato.Core;

namespace Potato.Gui.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private readonly Database _database;

    public MainWindowViewModel()
    {
        _database = new Database();
    }

    public string Greeting { get; } = "Welcome to Avalonia!";
}
