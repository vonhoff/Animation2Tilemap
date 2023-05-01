using Animation2Tilemap.Services;
using Animation2Tilemap.Services.Contracts;

namespace Animation2Tilemap.Test.Services;

public class ConfirmationDialogServiceTests
{
    private readonly IConfirmationDialogService _confirmationDialogService;

    public ConfirmationDialogServiceTests()
    {
        _confirmationDialogService = new ConfirmationDialogService();
    }

    [Theory]
    [InlineData("Y", true, true)]
    [InlineData("N", true, false)]
    [InlineData("", true, true)]
    [InlineData("Y", false, true)]
    [InlineData("N", false, false)]
    [InlineData("", false, false)]
    public void Confirm_ShouldReturnCorrectResponse_GivenValidInput(string input, bool defaultOption, bool expected)
    {
        // Arrange
        const string message = "Are you sure?";
        var inputReader = new StringReader(input);
        Console.SetIn(inputReader);

        // Act
        var actual = _confirmationDialogService.Confirm(message, defaultOption);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Confirm_ShouldReturnDefaultResponse_GivenInvalidInput()
    {
        // Arrange
        const string message = "Are you sure?";
        const bool defaultOption = true;
        const string input = "X\nY";
        var inputReader = new StringReader(input);
        Console.SetIn(inputReader);

        // Act
        var actual = _confirmationDialogService.Confirm(message, defaultOption);

        // Assert
        Assert.True(actual);
    }
}