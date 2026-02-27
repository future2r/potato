using Potato.Gui.ViewModels;
using Xunit;

namespace Potato.Gui.Tests.ViewModels;

public class MainWindowViewModelTests
{
    [Fact]
    public void AddVariety_WithEmptyInput_DoesNotAddToList()
    {
        // Arrange
        var model = new MainWindowViewModel();
        var initialCount = model.Varieties.Count;

        // Act
        model.VarietyToAdd = string.Empty;
        model.AddVarietyCommand.Execute(null);

        // Assert
        Assert.Equal(initialCount, model.Varieties.Count);
        Assert.False(model.AddVarietyCommand.CanExecute(null));
    }

    [Fact]
    public void AddVariety_WithBlankInput_DoesNotAddToList()
    {
        // Arrange
        var model = new MainWindowViewModel();
        var initialCount = model.Varieties.Count;

        // Act
        model.VarietyToAdd = "   ";
        model.AddVarietyCommand.Execute(null);

        // Assert
        Assert.Equal(initialCount, model.Varieties.Count);
        Assert.False(model.AddVarietyCommand.CanExecute(null));
    }

    [Fact]
    public void AddVariety_WithExistingVariety_DoesNotAddToList()
    {
        // Arrange
        var model = new MainWindowViewModel();
        Assert.NotEmpty(model.Varieties);
        var initialCount = model.Varieties.Count;
        var existingVariety = model.Varieties[0];

        // Act
        model.VarietyToAdd = existingVariety;
        model.AddVarietyCommand.Execute(null);

        // Assert
        Assert.Equal(initialCount, model.Varieties.Count);
        Assert.False(model.AddVarietyCommand.CanExecute(null));
    }

    [Fact]
    public void AddVariety_WithNewVariety_AddsToListAndClearsInput()
    {
        // Arrange
        var model = new MainWindowViewModel();
        var initialCount = model.Varieties.Count;
        var varietyToAdd = "Test Potato";
        Assert.DoesNotContain(varietyToAdd, model.Varieties);

        // Act
        model.VarietyToAdd = varietyToAdd;
        Assert.True(model.AddVarietyCommand.CanExecute(null));
        model.AddVarietyCommand.Execute(null);

        // Assert
        Assert.Contains(varietyToAdd, model.Varieties);
        Assert.Equal(initialCount + 1, model.Varieties.Count);
        Assert.Equal(string.Empty, model.VarietyToAdd);
    }

    [Fact]
    public void Remove_WithNoSelection_CannotExecute()
    {
        // Arrange
        var model = new MainWindowViewModel();

        // Act & Assert
        Assert.Empty(model.SelectedVarieties);
        Assert.False(model.RemoveCommand.CanExecute(null));
    }

    [Fact]
    public void Remove_WithSelectedVarieties_RemovesThemFromList()
    {
        // Arrange
        var model = new MainWindowViewModel();
        Assert.True(model.Varieties.Count >= 2, "Need at least 2 varieties for this test");
        var initialCount = model.Varieties.Count;

        // Select first and last variety for removal
        var firstVariety = model.Varieties[0];
        var lastVariety = model.Varieties[^1];
        model.SelectedVarieties.Add(firstVariety);
        model.SelectedVarieties.Add(lastVariety);

        // Act
        Assert.True(model.RemoveCommand.CanExecute(null));
        model.RemoveCommand.Execute(null);

        // Assert
        Assert.Equal(initialCount - 2, model.Varieties.Count);
        Assert.DoesNotContain(firstVariety, model.Varieties);
        Assert.DoesNotContain(lastVariety, model.Varieties);
        Assert.Empty(model.SelectedVarieties);
        Assert.False(model.RemoveCommand.CanExecute(null));
    }

    [Fact]
    public void RemoveAll_WithNonEmptyList_ClearsAllVarieties()
    {
        // Arrange
        var model = new MainWindowViewModel();
        Assert.NotEmpty(model.Varieties);
        Assert.True(model.RemoveAllCommand.CanExecute(null));

        // Act
        model.RemoveAllCommand.Execute(null);

        // Assert
        Assert.Empty(model.Varieties);
        Assert.False(model.RemoveAllCommand.CanExecute(null));
    }

    [Fact]
    public void RemoveAll_WithEmptyList_CannotExecute()
    {
        // Arrange
        var model = new MainWindowViewModel();
        model.Varieties.Clear();

        // Act & Assert
        Assert.Empty(model.Varieties);
        Assert.False(model.RemoveAllCommand.CanExecute(null));
    }
}
