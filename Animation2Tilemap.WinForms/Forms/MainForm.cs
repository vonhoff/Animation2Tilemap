using System.ComponentModel;
using System.Diagnostics;
using Animation2Tilemap.Core.Enums;
using Animation2Tilemap.Core.Factories.Contracts;
using Animation2Tilemap.Core.Factories;
using Animation2Tilemap.Core.Services.Contracts;
using Animation2Tilemap.Core.Services;
using Animation2Tilemap.Core.Workflows;
using Animation2Tilemap.WinForms.Dialogs;
using Animation2Tilemap.WinForms.Extensions;
using Animation2Tilemap.WinForms.Services;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;
using SixLabors.ImageSharp.PixelFormats;
using Color = System.Drawing.Color;
using Size = SixLabors.ImageSharp.Size;

namespace Animation2Tilemap.WinForms.Forms;

public partial class MainForm : Form
{
    private const string License = "MIT License";
    private const string ProjectName = "Animation2Tilemap";
    private const string ProjectSite = "https://github.com/vonhoff/Animation2Tilemap";
    private readonly ConsoleForm _consoleForm;
    private Color _backgroundColor = Color.White;
    private string _selectedInputPath = string.Empty;
    private string _selectedOutputPath = ".\\output";

    public MainForm()
    {
        InitializeComponent();

        _consoleForm = new ConsoleForm();
        _consoleForm.FormClosing += (_, e) =>
        {
            e.Cancel = true;
            ToggleConsole();
        };
        _consoleForm.Show();
        _consoleForm.Hide();

        InitializeLogger();
    }

    private static ServiceProvider ConfigureServices(MainWorkflowOptions mainWorkflowOptions)
    {
        var services = new ServiceCollection();
        services.AddSingleton(Log.Logger);
        services.AddSingleton(mainWorkflowOptions);
        services.AddSingleton<IConfirmationDialogService, ConfirmationDialogService>();
        services.AddSingleton<INamePatternService, NamePatternService>();
        services.AddSingleton<IImageAlignmentService, ImageAlignmentService>();
        services.AddSingleton<IImageLoaderService, ImageLoaderService>();
        services.AddSingleton<IImageHashService, ImageHashService>();
        services.AddSingleton<IXmlSerializerService, XmlSerializerService>();
        services.AddSingleton<ITilemapDataService, TilemapDataService>();
        services.AddSingleton<ITilesetFactory, TilesetFactory>();
        services.AddSingleton<ITilemapFactory, TilemapFactory>();
        services.AddSingleton<ITilesetImageFactory, TilesetImageFactory>();
        services.AddSingleton<MainWorkflow>();

        return services.BuildServiceProvider();
    }

    private static void HandleFailedOperation()
    {
        System.Media.SystemSounds.Hand.Play();
        MessageBox.Show(
            "The operation failed. Check the console output for more information.",
            "Operation failed",
            MessageBoxButtons.OK,
            MessageBoxIcon.Error
        );
    }

    private static async Task HandleSuccessfulOperation(string outputPath)
    {
        System.Media.SystemSounds.Beep.Play();
        var result = MessageBox.Show(
            "The operation has been completed successfully. Do you want to open the output folder?",
            "Operation successful",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Information
        );

        if (result == DialogResult.Yes)
        {
            await Task.Run(() => Process.Start("explorer.exe", outputPath));
        }
    }

    private static void OpenProjectSite()
    {
        try
        {
            Process.Start(new ProcessStartInfo(ProjectSite) { UseShellExecute = true });
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Failed to open the project site: {ProjectSite}", ProjectSite);
            MessageBox.Show(
                $"The project site couldn't be opened automatically. Please visit {ProjectSite} in your web browser for information about this program.",
                "Project Site Unavailable", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void cfgBackgroundColor_DoubleClick(object sender, EventArgs e) => ChangeBackgroundColor();

    private void cfgBackgroundColor_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            UpdateColor();
            e.Handled = true;
            e.SuppressKeyPress = true;
        }
    }

    private void cfgBackgroundColor_Leave(object sender, EventArgs e) => UpdateColor();

    private void cfgButtonInput_Click(object sender, EventArgs e) => SelectInputPath();

    private void cfgButtonOutput_Click(object sender, EventArgs e)
    {
        using var folderDialog = new FolderBrowserDialog();
        folderDialog.ShowNewFolderButton = true;

        if (folderDialog.ShowDialog() == DialogResult.OK)
        {
            cfgOutputPath.Text = folderDialog.SelectedPath;
            UpdateOutputPath();
        }
    }

    private void cfgInputPath_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            UpdateInputPath();
            e.Handled = true;
            e.SuppressKeyPress = true;
        }
    }

    private void cfgInputPath_Leave(object sender, EventArgs e) => UpdateInputPath();

    private void cfgOutputPath_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            UpdateOutputPath();
            e.Handled = true;
            e.SuppressKeyPress = true;
        }
    }

    private void ChangeBackgroundColor()
    {
        using var colorDialog = new ColorDialog();
        colorDialog.Color = _backgroundColor;

        if (colorDialog.ShowDialog() == DialogResult.OK)
        {
            cfgBackgroundColor.Text = ColorTranslator.ToHtml(colorDialog.Color);
            UpdateColor();
        }
    }

    private void InitializeLogger()
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Verbose()
            .WriteTo.RichTextBox(_consoleForm.OutputBox, LogEventLevel.Debug)
            .CreateLogger();

        Log.Information("{Name} — For more information, click the Help button.", ProjectName);
        Log.Information("This software is released under the {License}. © 2024 Simon Vonhoff", License);
    }

    private async void mainButtonStart_Click(object sender, EventArgs e)
    {
        Log.Information("A new operation has been started.");

        var mainWorkflowOptions = new MainWorkflowOptions
        {
            FrameDuration = (int)Math.Round(cfgFrameTime.Value),
            Input = cfgInputPath.Text,
            Output = cfgOutputPath.Text,
            TileSize = new Size((int)Math.Round(cfgTileWidth.Value), (int)Math.Round(cfgTileHeight.Value)),
            TileMargin = (int)Math.Round(cfgTileMargin.Value),
            TileSpacing = (int)Math.Round(cfgTileSpacing.Value),
            TransparentColor = Rgba32.ParseHex(cfgBackgroundColor.Text),
            TileLayerFormat = (TileLayerFormat)cfgLayerFormat.SelectedIndex,
            Verbose = true
        };

        var services = ConfigureServices(mainWorkflowOptions);

        Cursor = Cursors.WaitCursor;

        try
        {
            var workflow = services.GetRequiredService<MainWorkflow>();
            var result = await Task.Run(() => workflow.Run());

            if (result)
            {
                await HandleSuccessfulOperation(mainWorkflowOptions.Output);
            }
            else
            {
                HandleFailedOperation();
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex, "An unexpected error occurred during the operation.");
            HandleFailedOperation();
        }
        finally
        {
            Cursor = Cursors.Default;
        }
    }

    private void mainButtonToggleConsole_Click(object sender, EventArgs e) => ToggleConsole();

    private void MainForm_HelpButtonClicked(object sender, CancelEventArgs e) => OpenProjectSite();

    private void MainForm_Load(object sender, EventArgs e)
    {
        cfgLayerFormat.SelectedIndex = 2;
        cfgInputPath.Text = _selectedInputPath;
        cfgOutputPath.Text = _selectedOutputPath;
    }
    private void SelectInputPath()
    {
        using var fileFolderDialog = new FileFolderDialog();

        if (fileFolderDialog.ShowDialog() == DialogResult.OK)
        {
            cfgInputPath.Text = fileFolderDialog.SelectedPath;
            UpdateInputPath();
        }
    }

    private void ToggleConsole()
    {
        if (_consoleForm.Visible)
        {
            mainButtonToggleConsole.Text = "Show Console";
            _consoleForm.Hide();
        }
        else
        {
            mainButtonToggleConsole.Text = "Hide Console";
            _consoleForm.Show();
        }
    }
    private void UpdateColor()
    {
        try
        {
            var color = ColorTranslator.FromHtml(cfgBackgroundColor.Text);
            cfgBackgroundColor.BackColor = color;
            _backgroundColor = color;
            Log.Information("Background color updated to: {Color}", _backgroundColor.ToHex());
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Invalid background color specified: {Color}. Restoring previous color.",
                cfgBackgroundColor.Text);
            MessageBox.Show(
                "The specified color is invalid. The previous color has been restored to ensure proper functionality.",
                "Color Format Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        cfgBackgroundColor.Text = _backgroundColor.ToHex();
        cfgBackgroundColor.ForeColor = _backgroundColor.ContrastColor();
    }

    private void UpdateInputPath()
    {
        if (_selectedInputPath == cfgInputPath.Text)
        {
            return;
        }

        if (Directory.Exists(cfgInputPath.Text))
        {
            Log.Information("Input folder set to: {Path}", cfgInputPath.Text);
        }
        else if (File.Exists(cfgInputPath.Text))
        {
            Log.Information("Input file set to: {Path}", cfgInputPath.Text);
        }
        else
        {
            Log.Warning("Invalid input path specified: {Path}", cfgInputPath.Text);
            MessageBox.Show(
                "The specified path doesn't exist or is inaccessible. Please select a valid file or folder to proceed.",
                "Invalid Input Path", MessageBoxButtons.OK, MessageBoxIcon.Error);
            cfgInputPath.Text = _selectedInputPath;
            return;
        }

        _selectedInputPath = cfgInputPath.Text;
    }
    private void UpdateOutputPath()
    {
        if (string.IsNullOrWhiteSpace(cfgOutputPath.Text))
        {
            cfgOutputPath.Text = ".";
            Log.Information("Empty output path specified. Set to folder: {Path}", Path.GetFullPath("."));
        }

        if (_selectedOutputPath == cfgOutputPath.Text) return;

        var fullPath = Path.GetFullPath(cfgOutputPath.Text);
        Log.Information("Output folder set to: {Path}", fullPath);
        _selectedOutputPath = cfgOutputPath.Text;

        if (!Directory.Exists(fullPath))
        {
            Log.Information("Output folder does not exist. It will be created at runtime: {Path}", fullPath);
            MessageBox.Show(
                "The specified output folder doesn't exist yet. It will be created automatically when the process runs.",
                "New Output Folder", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        else if (Directory.GetFiles(fullPath).Length != 0)
        {
            Log.Warning("Output folder is not empty. Existing files may be overwritten: {Path}", fullPath);
            MessageBox.Show(
                "The specified output folder contains existing files. These files may be overwritten during the process.",
                "Existing Files in Output Folder", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}