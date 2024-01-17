namespace Animation2Tilemap.WinForms.Dialogs;

public class FileFolderDialog : CommonDialog
{
    private readonly OpenFileDialog _dialog = new();

    /// <summary>
    ///     Parses FilePath into either folder path (if Folder Selection is set) or returns file path.
    /// </summary>
    public string SelectedPath
    {
        get
        {
            try
            {
                if (string.IsNullOrWhiteSpace(_dialog.FileName) == false &&
                    (_dialog.FileName.EndsWith("Folder Selection") || File.Exists(_dialog.FileName) == false) &&
                    Directory.Exists(_dialog.FileName) == false)
                {
                    return Path.GetDirectoryName(_dialog.FileName) ?? throw new InvalidOperationException();
                }
            }
            catch
            {
                // Ignore
            }

            return _dialog.FileName;
        }
        set
        {
            if (string.IsNullOrWhiteSpace(value) == false)
            {
                _dialog.FileName = value;
            }
        }
    }

    public override void Reset()
    {
        _dialog.Reset();
    }

    public new DialogResult ShowDialog()
    {
        return ShowDialog(null);
    }

    private new DialogResult ShowDialog(IWin32Window? owner)
    {
        // Set validate names to false, otherwise Windows will not let you select "Folder Selection".
        _dialog.ValidateNames = false;
        _dialog.CheckFileExists = false;
        _dialog.CheckPathExists = true;

        try
        {
            // Set the initial directory (used when dialog.FileName is set from the outside).
            if (string.IsNullOrWhiteSpace(_dialog.FileName) == false)
            {
                _dialog.InitialDirectory = Directory.Exists(_dialog.FileName) ? _dialog.FileName : Path.GetDirectoryName(_dialog.FileName);
            }
        }
        catch
        {
            // Ignore
        }

        // Always default to Folder Selection.
        _dialog.FileName = "Folder Selection";
        return _dialog.ShowDialog(owner);
    }

    protected override bool RunDialog(IntPtr hwndOwner)
    {
        return true;
    }
}