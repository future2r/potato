using Potato.Gui.ViewModels;
using Xunit;

namespace Potato.Gui.Tests.ViewModels;

public class MainWindowViewModelTests
{
    [Fact]
    public void MainWindowViewModel_CanBeCreated_WithoutException()
    {
        // Arrange & Act
        var exception = Record.Exception(() => new MainWindowViewModel());

        // Assert
        Assert.Null(exception);
    }
}
