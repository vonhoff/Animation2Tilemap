namespace Animation2Tilemap.Core.Services.Contracts;

public interface IConfirmationDialogService
{
    bool Confirm(string message, bool defaultOption);
}