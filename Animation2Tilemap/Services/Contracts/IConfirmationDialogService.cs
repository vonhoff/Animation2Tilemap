namespace Animation2Tilemap.Services.Contracts;

public interface IConfirmationDialogService
{
    bool Confirm(string message, bool defaultOption);
}