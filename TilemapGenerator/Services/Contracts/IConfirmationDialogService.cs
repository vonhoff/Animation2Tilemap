namespace TilemapGenerator.Services.Contracts;

public interface IConfirmationDialogService
{
    bool Confirm(string message, bool defaultOption);
}