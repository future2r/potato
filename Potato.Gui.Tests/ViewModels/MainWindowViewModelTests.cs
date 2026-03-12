using FluentAssertions;
using Potato.Gui.ViewModels;
using System.Linq;
using Xunit;

namespace Potato.Gui.Tests.ViewModels;

public class MainWindowViewModelTests
{
    private readonly MainWindowViewModel _sut = new();

    [Fact]
    public void AddVariety_WithEmptyInput_DoesNotAddToList()
    {
        // Arrange
        var initialCount = _sut.Varieties.Count;

        // Act
        _sut.VarietyNameToAdd = string.Empty;
        _sut.AddVarietyCommand.Execute(null);

        // Assert
        _sut.Varieties.Count.Should().Be(initialCount);
        _sut.AddVarietyCommand.CanExecute(null).Should().BeFalse();
    }

    [Fact]
    public void AddVariety_WithBlankInput_DoesNotAddToList()
    {
        // Arrange
        var initialCount = _sut.Varieties.Count;

        // Act
        _sut.VarietyNameToAdd = "   ";
        _sut.AddVarietyCommand.Execute(null);

        // Assert
        _sut.Varieties.Count.Should().Be(initialCount);
        _sut.AddVarietyCommand.CanExecute(null).Should().BeFalse();
    }

    [Fact]
    public void AddVariety_WithExistingVariety_DoesNotAddToList()
    {
        // Arrange
        _sut.Varieties.Should().NotBeEmpty();
        var initialCount = _sut.Varieties.Count;
        var existingName = _sut.Varieties[0].Name;

        // Act
        _sut.VarietyNameToAdd = existingName;
        _sut.AddVarietyCommand.Execute(null);

        // Assert
        _sut.Varieties.Count.Should().Be(initialCount);
        _sut.AddVarietyCommand.CanExecute(null).Should().BeFalse();
    }

    [Fact]
    public void AddVariety_WithNewVariety_AddsToListAndClearsInput()
    {
        // Arrange
        var initialCount = _sut.Varieties.Count;
        var name = "Test Potato";
        _sut.Varieties.Select(variety => variety.Name).Should().NotContain(name);

        // Act
        _sut.VarietyNameToAdd = name;
        _sut.AddVarietyCommand.CanExecute(null).Should().BeTrue();
        _sut.AddVarietyCommand.Execute(null);

        // Assert
        _sut.Varieties.Select(variety => variety.Name).Should().Contain(name);
        _sut.Varieties.Count.Should().Be(initialCount + 1);
        _sut.VarietyNameToAdd.Should().BeEmpty();
    }

    [Fact]
    public void Remove_WithNoSelection_CannotExecute()
    {
        // Arrange

        // Act & Assert
        _sut.SelectedVarieties.Should().BeEmpty();
        _sut.RemoveCommand.CanExecute(null).Should().BeFalse();
    }

    [Fact]
    public void Remove_WithSelectedVarieties_RemovesThemFromList()
    {
        // Arrange
        _sut.Varieties.Should().HaveCountGreaterThanOrEqualTo(2, "need at least 2 varieties for this test");
        var initialCount = _sut.Varieties.Count;

        // Select first and last variety for removal
        var firstVariety = _sut.Varieties[0];
        var lastVariety = _sut.Varieties[^1];
        _sut.SelectedVarieties.Add(firstVariety);
        _sut.SelectedVarieties.Add(lastVariety);

        // Act
        _sut.RemoveCommand.CanExecute(null).Should().BeTrue();
        _sut.RemoveCommand.Execute(null);

        // Assert
        _sut.Varieties.Count.Should().Be(initialCount - 2);
        _sut.Varieties.Should().NotContain(firstVariety);
        _sut.Varieties.Should().NotContain(lastVariety);
        _sut.SelectedVarieties.Should().BeEmpty();
        _sut.RemoveCommand.CanExecute(null).Should().BeFalse();
    }

    [Fact]
    public void RemoveAll_WithNonEmptyList_ClearsAllVarieties()
    {
        // Arrange
        _sut.Varieties.Should().NotBeEmpty();
        _sut.RemoveAllCommand.CanExecute(null).Should().BeTrue();

        // Act
        _sut.RemoveAllCommand.Execute(null);

        // Assert
        _sut.Varieties.Should().BeEmpty();
        _sut.RemoveAllCommand.CanExecute(null).Should().BeFalse();
    }

    [Fact]
    public void RemoveAll_WithEmptyList_CannotExecute()
    {
        // Arrange
        _sut.RemoveAllCommand.Execute(null);

        // Act & Assert
        _sut.Varieties.Should().BeEmpty();
        _sut.RemoveAllCommand.CanExecute(null).Should().BeFalse();
    }
}
