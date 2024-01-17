using Animation2Tilemap.Core.Services.Contracts;

namespace Animation2Tilemap.WinForms.Services;

public class ConfirmationDialogService : IConfirmationDialogService
{
    public bool Confirm(string message, bool defaultOption)
    {
        var defaultButton = defaultOption ? MessageBoxDefaultButton.Button1 : MessageBoxDefaultButton.Button2;
        var result = MessageBox.Show(message, "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, defaultButton);

        return result == DialogResult.Yes;
    }
}