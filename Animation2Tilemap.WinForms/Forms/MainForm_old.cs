using Animation2Tilemap.Core;
using Animation2Tilemap.Core.Enums;
using Animation2Tilemap.Core.Factories;
using Animation2Tilemap.Core.Factories.Contracts;
using Animation2Tilemap.Core.Services;
using Animation2Tilemap.Core.Services.Contracts;
using Animation2Tilemap.WinForms.Dialogs;
using Animation2Tilemap.WinForms.Extensions;
using Animation2Tilemap.WinForms.Services;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.RichTextBoxForms;
using Serilog.Sinks.RichTextBoxForms.Themes;
using SixLabors.ImageSharp.PixelFormats;
using System.ComponentModel;
using System.Diagnostics;
using System.Media;
using Animation2Tilemap.Core.Workflows;
using Color = System.Drawing.Color;
using Directory = System.IO.Directory;
using Size = SixLabors.ImageSharp.Size;

namespace Animation2Tilemap.WinForms.Forms;

public partial class MainForm_old : Form
{
    private const string OutputDefault = "output";
    private const string ProjectName = "Animation2Tilemap v1.2.0";
    private const string ProjectSite = "https://github.com/vonhoff/Animation2Tilemap";
    private Color _selectedColor = Color.White;
    private string _selectedInputPath = string.Empty;
    private string _selectedOutputPath = OutputDefault;

    public MainForm_old()
    {
        InitializeComponent();
    }

    private void configButtonColor_Click(object sender, EventArgs e)
    {
        var colorDialog = new ColorDialog
        {
            Color = _selectedColor
        };

        if (colorDialog.ShowDialog() == DialogResult.OK)
        {
            configTextColor.Text = ColorTranslator.ToHtml(colorDialog.Color);
            UpdateColor();
        }
    }

    private void configButtonInput_Click(object sender, EventArgs e)
    {
        var fileFolderDialog = new FileFolderDialog();

        if (fileFolderDialog.ShowDialog() == DialogResult.OK)
        {
            configTextInput.Text = fileFolderDialog.SelectedPath;
            UpdateInputPath();
        }
    }

    private void configButtonOutput_Click(object sender, EventArgs e)
    {
        var folderDialog = new FolderBrowserDialog
        {
            ShowNewFolderButton = true
        };

        if (folderDialog.ShowDialog() == DialogResult.OK)
        {
            configTextOutput.Text = folderDialog.SelectedPath;
            UpdateOutputPath();
        }
    }

    private void configButtonStart_Click(object sender, EventArgs e)
    {
        Log.Information("{Text}", "A new operation has been started.");

        var mainWorkflowOptions = new MainWorkflowOptions
        {
            FrameDuration = (int)Math.Round(configNumberFrameTime.Value),
            Input = configTextInput.Text,
            Output = configTextOutput.Text,
            TileSize = new Size((int)Math.Round(configNumberWidth.Value), (int)Math.Round(configNumberHeight.Value)),
            TileMargin = (int)Math.Round(configNumberMargin.Value),
            TileSpacing = (int)Math.Round(configNumberSpacing.Value),
            TransparentColor = Rgba32.ParseHex(configTextColor.Text),
            TileLayerFormat = (TileLayerFormat)configLayerFormat.SelectedIndex,
            Verbose = true
        };

        var services = new ServiceCollection();
        services.AddScoped<MainForm_old>();
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

        ToggleStartButton(false);
        tabControl2.Enabled = false;
        Cursor = Cursors.WaitCursor;
        outputBox.Cursor = Cursors.WaitCursor;

        var serviceProvider = services.BuildServiceProvider();
        var coreApplication = serviceProvider.GetRequiredService<MainWorkflow>();
        var resultTask = Task.Run(() => coreApplication.Run());

        resultTask.ContinueWith(task =>
        {
            ToggleStartButton(true);
            tabControl2.Enabled = true;
            Cursor = Cursors.Default;
            outputBox.Cursor = Cursors.Default;

            if (task.Result)
            {
                SystemSounds.Beep.Play();
                var openFolderResult = MessageBox.Show(
                    "The operation has been completed successfully. Would you like to open the output folder?",
                    "Operation successful",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Information
                );

                if (openFolderResult == DialogResult.Yes)
                {
                    Process.Start("explorer.exe", mainWorkflowOptions.Output);
                }
            }
            else
            {
                SystemSounds.Hand.Play();
                MessageBox.Show(
                    "The operation failed. Check the console output for more information.",
                    "Operation failed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }, TaskScheduler.FromCurrentSynchronizationContext());
    }

    private void configTextColor_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            UpdateColor();
            configButtonColor.Focus();
        }
    }

    private void configTextColor_Leave(object sender, EventArgs e)
    {
        UpdateColor();
    }

    private void configTextInput_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            configButtonInput.Focus();
        }
    }

    private void configTextInput_Leave(object sender, EventArgs e)
    {
        UpdateInputPath();
    }

    private void configTextOutput_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            configButtonOutput.Focus();
        }
    }

    private void configTextOutput_Leave(object sender, EventArgs e)
    {
        UpdateOutputPath();
    }

    private void MainForm_HelpButtonClicked(object sender, CancelEventArgs e)
    {
        try
        {
            Process.Start(
                new ProcessStartInfo
                {
                    FileName = ProjectSite,
                    UseShellExecute = true
                }
            );
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error opening web page.");
            Log.Information("Please visit {Url} in your web browser for information about this program.", ProjectSite);
        }
    }

    private void MainForm_Load(object sender, EventArgs e)
    {
        configLayerFormat.SelectedIndex = 2;
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Verbose()
            .WriteTo.RichTextBox(outputBox)
            .CreateLogger();

        Log.Information("{Name} — For more information, click the Help button.", ProjectName);
        Log.Information("Select an image or folder that contains animated frames.");
    }

    private void ToggleStartButton(bool toggle)
    {
        if (toggle)
        {
            configButtonStart.BackColor = Color.FromArgb(0, 123, 255);
            configButtonStart.ForeColor = Color.White;
            configButtonStart.Enabled = true;
        }
        else
        {
            configButtonStart.BackColor = Color.Transparent;
            configButtonStart.ForeColor = Color.Black;
            configButtonStart.Enabled = false;
        }
    }

    private void ToggleInputHighlight(bool toggle)
    {
        splitContainer3.BackColor = toggle ? Color.Gold : Color.Transparent;
    }

    private void UpdateColor()
    {
        var code = _selectedColor.ToHex();

        if (configTextColor.Text != code)
        {
            try
            {
                var color = ColorTranslator.FromHtml(configTextColor.Text);
                _selectedColor = color;
            }
            catch
            {
                Log.Error("The specified color is invalid, the previous color is restored.");
            }
        }

        configTextColor.Text = _selectedColor.ToHex();
    }

    private void UpdateInputPath()
    {
        if (_selectedInputPath == configTextInput.Text)
        {
            return;
        }

        if (Directory.Exists(configTextInput.Text))
        {
            Log.Information("Input Folder: {Path}", configTextInput.Text);
            ToggleStartButton(true);
            ToggleInputHighlight(false);
        }
        else if (File.Exists(configTextInput.Text))
        {
            Log.Information("Input File: {Path}", configTextInput.Text);
            ToggleStartButton(true);
            ToggleInputHighlight(false);
        }
        else
        {
            Log.Error("The specified input path does not point to an existing file or folder.");
            configTextInput.Text = string.Empty;
            ToggleStartButton(false);
            ToggleInputHighlight(true);
        }

        _selectedInputPath = configTextInput.Text;
    }

    private void UpdateOutputPath()
    {
        if (string.IsNullOrWhiteSpace(configTextOutput.Text))
        {
            Log.Warning("An empty output path is not allowed, the default path is restored.");
            configTextOutput.Text = OutputDefault;
        }

        if (_selectedOutputPath != configTextOutput.Text)
        {
            Log.Information("Output Folder: {Path}", Path.GetFullPath(configTextOutput.Text));
            _selectedOutputPath = configTextOutput.Text;

            if (Directory.Exists(configTextOutput.Text) == false)
            {
                Log.Information("The output folder does not exist, it will be created at run time.");
            }
            else
            {
                if (Directory.GetFiles(configTextOutput.Text).Length != 0)
                {
                    Log.Warning("The output folder is not empty, existing files will be overwritten.");
                }
            }
        }
    }
}