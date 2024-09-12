namespace Animation2Tilemap.WinForms.Forms
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            mainButtonStart = new Button();
            mainButtonCancel = new Button();
            mainButtonToggleConsole = new Button();
            cfgBoxTileSettings = new GroupBox();
            cfgBackgroundColor = new TextBox();
            cfgLabelLayerFormat = new Label();
            cfgLabelFrameTime = new Label();
            cfgLayerFormat = new ComboBox();
            cfgLabelBackgroundColor = new Label();
            cfgFrameTime = new NumericUpDown();
            cfgLabelTileSpacing = new Label();
            cfgLabelTileMargin = new Label();
            cfgLabelTileHeight = new Label();
            cfgLabelTileWidth = new Label();
            cfgTileSpacing = new NumericUpDown();
            cfgTileMargin = new NumericUpDown();
            cfgTileHeight = new NumericUpDown();
            cfgTileWidth = new NumericUpDown();
            cfgBoxPrimarySettings = new GroupBox();
            cfgButtonOutput = new Button();
            cfgOutputPath = new TextBox();
            cfgButtonInput = new Button();
            cfgInputPath = new TextBox();
            cfgLabelOutput = new Label();
            cfgLabelInput = new Label();
            mainProgressFile = new Label();
            mainBoxProgress = new GroupBox();
            mainProgressBar = new ProgressBar();
            cfgBoxTileSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)cfgFrameTime).BeginInit();
            ((System.ComponentModel.ISupportInitialize)cfgTileSpacing).BeginInit();
            ((System.ComponentModel.ISupportInitialize)cfgTileMargin).BeginInit();
            ((System.ComponentModel.ISupportInitialize)cfgTileHeight).BeginInit();
            ((System.ComponentModel.ISupportInitialize)cfgTileWidth).BeginInit();
            cfgBoxPrimarySettings.SuspendLayout();
            mainBoxProgress.SuspendLayout();
            SuspendLayout();
            // 
            // mainButtonStart
            // 
            mainButtonStart.Location = new Point(6, 342);
            mainButtonStart.Name = "mainButtonStart";
            mainButtonStart.Size = new Size(113, 35);
            mainButtonStart.TabIndex = 0;
            mainButtonStart.Text = "Start";
            mainButtonStart.UseVisualStyleBackColor = true;
            mainButtonStart.Click += mainButtonStart_Click;
            // 
            // mainButtonCancel
            // 
            mainButtonCancel.Enabled = false;
            mainButtonCancel.Location = new Point(125, 342);
            mainButtonCancel.Name = "mainButtonCancel";
            mainButtonCancel.Size = new Size(113, 35);
            mainButtonCancel.TabIndex = 1;
            mainButtonCancel.Text = "Cancel";
            mainButtonCancel.UseVisualStyleBackColor = true;
            mainButtonCancel.Click += mainButtonCancel_Click;
            // 
            // mainButtonToggleConsole
            // 
            mainButtonToggleConsole.Location = new Point(320, 342);
            mainButtonToggleConsole.Name = "mainButtonToggleConsole";
            mainButtonToggleConsole.Size = new Size(113, 35);
            mainButtonToggleConsole.TabIndex = 5;
            mainButtonToggleConsole.Text = "Show Console";
            mainButtonToggleConsole.UseVisualStyleBackColor = true;
            mainButtonToggleConsole.Click += mainButtonToggleConsole_Click;
            // 
            // cfgBoxTileSettings
            // 
            cfgBoxTileSettings.Controls.Add(cfgBackgroundColor);
            cfgBoxTileSettings.Controls.Add(cfgLabelLayerFormat);
            cfgBoxTileSettings.Controls.Add(cfgLabelFrameTime);
            cfgBoxTileSettings.Controls.Add(cfgLayerFormat);
            cfgBoxTileSettings.Controls.Add(cfgLabelBackgroundColor);
            cfgBoxTileSettings.Controls.Add(cfgFrameTime);
            cfgBoxTileSettings.Controls.Add(cfgLabelTileSpacing);
            cfgBoxTileSettings.Controls.Add(cfgLabelTileMargin);
            cfgBoxTileSettings.Controls.Add(cfgLabelTileHeight);
            cfgBoxTileSettings.Controls.Add(cfgLabelTileWidth);
            cfgBoxTileSettings.Controls.Add(cfgTileSpacing);
            cfgBoxTileSettings.Controls.Add(cfgTileMargin);
            cfgBoxTileSettings.Controls.Add(cfgTileHeight);
            cfgBoxTileSettings.Controls.Add(cfgTileWidth);
            cfgBoxTileSettings.Dock = DockStyle.Top;
            cfgBoxTileSettings.Location = new Point(6, 136);
            cfgBoxTileSettings.Name = "cfgBoxTileSettings";
            cfgBoxTileSettings.Size = new Size(427, 126);
            cfgBoxTileSettings.TabIndex = 21;
            cfgBoxTileSettings.TabStop = false;
            cfgBoxTileSettings.Text = "Generation Settings";
            // 
            // cfgBackgroundColor
            // 
            cfgBackgroundColor.Location = new Point(111, 93);
            cfgBackgroundColor.Name = "cfgBackgroundColor";
            cfgBackgroundColor.Size = new Size(149, 23);
            cfgBackgroundColor.TabIndex = 14;
            cfgBackgroundColor.Text = "#FFFFFF";
            cfgBackgroundColor.DoubleClick += cfgBackgroundColor_DoubleClick;
            cfgBackgroundColor.KeyDown += cfgBackgroundColor_KeyDown;
            cfgBackgroundColor.Leave += cfgBackgroundColor_Leave;
            // 
            // cfgLabelLayerFormat
            // 
            cfgLabelLayerFormat.AutoSize = true;
            cfgLabelLayerFormat.Location = new Point(266, 69);
            cfgLabelLayerFormat.Name = "cfgLabelLayerFormat";
            cfgLabelLayerFormat.Padding = new Padding(0, 5, 0, 1);
            cfgLabelLayerFormat.Size = new Size(76, 21);
            cfgLabelLayerFormat.TabIndex = 12;
            cfgLabelLayerFormat.Text = "Layer Format";
            // 
            // cfgLabelFrameTime
            // 
            cfgLabelFrameTime.AutoSize = true;
            cfgLabelFrameTime.Location = new Point(6, 69);
            cfgLabelFrameTime.Name = "cfgLabelFrameTime";
            cfgLabelFrameTime.Padding = new Padding(0, 5, 0, 1);
            cfgLabelFrameTime.Size = new Size(96, 21);
            cfgLabelFrameTime.TabIndex = 13;
            cfgLabelFrameTime.Text = "Frame Time (ms)";
            // 
            // cfgLayerFormat
            // 
            cfgLayerFormat.DropDownStyle = ComboBoxStyle.DropDownList;
            cfgLayerFormat.FormattingEnabled = true;
            cfgLayerFormat.Items.AddRange(new object[] { "base64", "gzip + base64", "zlib + base64", "csv" });
            cfgLayerFormat.Location = new Point(266, 93);
            cfgLayerFormat.Margin = new Padding(3, 2, 3, 2);
            cfgLayerFormat.Name = "cfgLayerFormat";
            cfgLayerFormat.Size = new Size(154, 23);
            cfgLayerFormat.TabIndex = 13;
            // 
            // cfgLabelBackgroundColor
            // 
            cfgLabelBackgroundColor.AutoSize = true;
            cfgLabelBackgroundColor.Location = new Point(111, 69);
            cfgLabelBackgroundColor.Name = "cfgLabelBackgroundColor";
            cfgLabelBackgroundColor.Padding = new Padding(0, 5, 0, 1);
            cfgLabelBackgroundColor.Size = new Size(103, 21);
            cfgLabelBackgroundColor.TabIndex = 6;
            cfgLabelBackgroundColor.Text = "Background Color";
            // 
            // cfgFrameTime
            // 
            cfgFrameTime.Location = new Point(6, 93);
            cfgFrameTime.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            cfgFrameTime.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            cfgFrameTime.Name = "cfgFrameTime";
            cfgFrameTime.Size = new Size(99, 23);
            cfgFrameTime.TabIndex = 12;
            cfgFrameTime.Value = new decimal(new int[] { 125, 0, 0, 0 });
            // 
            // cfgLabelTileSpacing
            // 
            cfgLabelTileSpacing.AutoSize = true;
            cfgLabelTileSpacing.Location = new Point(321, 19);
            cfgLabelTileSpacing.Name = "cfgLabelTileSpacing";
            cfgLabelTileSpacing.Padding = new Padding(0, 5, 0, 1);
            cfgLabelTileSpacing.Size = new Size(94, 21);
            cfgLabelTileSpacing.TabIndex = 8;
            cfgLabelTileSpacing.Text = "Tile Spacing (px)";
            // 
            // cfgLabelTileMargin
            // 
            cfgLabelTileMargin.AutoSize = true;
            cfgLabelTileMargin.Location = new Point(216, 19);
            cfgLabelTileMargin.Name = "cfgLabelTileMargin";
            cfgLabelTileMargin.Padding = new Padding(0, 5, 0, 1);
            cfgLabelTileMargin.Size = new Size(90, 21);
            cfgLabelTileMargin.TabIndex = 7;
            cfgLabelTileMargin.Text = "Tile Margin (px)";
            // 
            // cfgLabelTileHeight
            // 
            cfgLabelTileHeight.AutoSize = true;
            cfgLabelTileHeight.Location = new Point(111, 19);
            cfgLabelTileHeight.Name = "cfgLabelTileHeight";
            cfgLabelTileHeight.Padding = new Padding(0, 5, 0, 1);
            cfgLabelTileHeight.Size = new Size(88, 21);
            cfgLabelTileHeight.TabIndex = 6;
            cfgLabelTileHeight.Text = "Tile Height (px)";
            // 
            // cfgLabelTileWidth
            // 
            cfgLabelTileWidth.AutoSize = true;
            cfgLabelTileWidth.Location = new Point(6, 19);
            cfgLabelTileWidth.Name = "cfgLabelTileWidth";
            cfgLabelTileWidth.Padding = new Padding(0, 5, 0, 1);
            cfgLabelTileWidth.Size = new Size(84, 21);
            cfgLabelTileWidth.TabIndex = 5;
            cfgLabelTileWidth.Text = "Tile Width (px)";
            // 
            // cfgTileSpacing
            // 
            cfgTileSpacing.Location = new Point(321, 43);
            cfgTileSpacing.Maximum = new decimal(new int[] { 128, 0, 0, 0 });
            cfgTileSpacing.Name = "cfgTileSpacing";
            cfgTileSpacing.Size = new Size(99, 23);
            cfgTileSpacing.TabIndex = 4;
            // 
            // cfgTileMargin
            // 
            cfgTileMargin.Location = new Point(216, 43);
            cfgTileMargin.Maximum = new decimal(new int[] { 128, 0, 0, 0 });
            cfgTileMargin.Name = "cfgTileMargin";
            cfgTileMargin.Size = new Size(99, 23);
            cfgTileMargin.TabIndex = 3;
            // 
            // cfgTileHeight
            // 
            cfgTileHeight.Location = new Point(111, 43);
            cfgTileHeight.Maximum = new decimal(new int[] { 128, 0, 0, 0 });
            cfgTileHeight.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            cfgTileHeight.Name = "cfgTileHeight";
            cfgTileHeight.Size = new Size(99, 23);
            cfgTileHeight.TabIndex = 2;
            cfgTileHeight.Value = new decimal(new int[] { 8, 0, 0, 0 });
            // 
            // cfgTileWidth
            // 
            cfgTileWidth.Location = new Point(6, 43);
            cfgTileWidth.Maximum = new decimal(new int[] { 128, 0, 0, 0 });
            cfgTileWidth.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            cfgTileWidth.Name = "cfgTileWidth";
            cfgTileWidth.Size = new Size(99, 23);
            cfgTileWidth.TabIndex = 1;
            cfgTileWidth.Value = new decimal(new int[] { 8, 0, 0, 0 });
            // 
            // cfgBoxPrimarySettings
            // 
            cfgBoxPrimarySettings.Controls.Add(cfgButtonOutput);
            cfgBoxPrimarySettings.Controls.Add(cfgOutputPath);
            cfgBoxPrimarySettings.Controls.Add(cfgButtonInput);
            cfgBoxPrimarySettings.Controls.Add(cfgInputPath);
            cfgBoxPrimarySettings.Controls.Add(cfgLabelOutput);
            cfgBoxPrimarySettings.Controls.Add(cfgLabelInput);
            cfgBoxPrimarySettings.Dock = DockStyle.Top;
            cfgBoxPrimarySettings.Location = new Point(6, 6);
            cfgBoxPrimarySettings.Name = "cfgBoxPrimarySettings";
            cfgBoxPrimarySettings.Size = new Size(427, 130);
            cfgBoxPrimarySettings.TabIndex = 20;
            cfgBoxPrimarySettings.TabStop = false;
            cfgBoxPrimarySettings.Text = "Primary Settings";
            // 
            // cfgButtonOutput
            // 
            cfgButtonOutput.Location = new Point(384, 90);
            cfgButtonOutput.Margin = new Padding(3, 2, 3, 2);
            cfgButtonOutput.Name = "cfgButtonOutput";
            cfgButtonOutput.Size = new Size(37, 23);
            cfgButtonOutput.TabIndex = 8;
            cfgButtonOutput.Text = "...";
            cfgButtonOutput.UseVisualStyleBackColor = true;
            cfgButtonOutput.Click += cfgButtonOutput_Click;
            // 
            // cfgOutputPath
            // 
            cfgOutputPath.Location = new Point(6, 90);
            cfgOutputPath.Margin = new Padding(3, 2, 3, 2);
            cfgOutputPath.Name = "cfgOutputPath";
            cfgOutputPath.Size = new Size(372, 23);
            cfgOutputPath.TabIndex = 7;
            cfgOutputPath.KeyDown += cfgOutputPath_KeyDown;
            // 
            // cfgButtonInput
            // 
            cfgButtonInput.Location = new Point(384, 42);
            cfgButtonInput.Margin = new Padding(3, 2, 3, 2);
            cfgButtonInput.Name = "cfgButtonInput";
            cfgButtonInput.Size = new Size(37, 23);
            cfgButtonInput.TabIndex = 6;
            cfgButtonInput.Text = "...";
            cfgButtonInput.UseVisualStyleBackColor = true;
            cfgButtonInput.Click += cfgButtonInput_Click;
            // 
            // cfgInputPath
            // 
            cfgInputPath.Location = new Point(6, 42);
            cfgInputPath.Margin = new Padding(3, 2, 3, 2);
            cfgInputPath.Name = "cfgInputPath";
            cfgInputPath.Size = new Size(372, 23);
            cfgInputPath.TabIndex = 5;
            cfgInputPath.KeyDown += cfgInputPath_KeyDown;
            cfgInputPath.Leave += cfgInputPath_Leave;
            // 
            // cfgLabelOutput
            // 
            cfgLabelOutput.AutoSize = true;
            cfgLabelOutput.Location = new Point(6, 67);
            cfgLabelOutput.Name = "cfgLabelOutput";
            cfgLabelOutput.Padding = new Padding(0, 5, 0, 1);
            cfgLabelOutput.Size = new Size(81, 21);
            cfgLabelOutput.TabIndex = 4;
            cfgLabelOutput.Text = "Output Folder";
            // 
            // cfgLabelInput
            // 
            cfgLabelInput.AutoSize = true;
            cfgLabelInput.Location = new Point(3, 19);
            cfgLabelInput.Name = "cfgLabelInput";
            cfgLabelInput.Padding = new Padding(0, 5, 0, 1);
            cfgLabelInput.Size = new Size(94, 21);
            cfgLabelInput.TabIndex = 0;
            cfgLabelInput.Text = "Input File/Folder";
            // 
            // mainProgressFile
            // 
            mainProgressFile.AutoSize = true;
            mainProgressFile.Dock = DockStyle.Bottom;
            mainProgressFile.Location = new Point(6, 47);
            mainProgressFile.Name = "mainProgressFile";
            mainProgressFile.Padding = new Padding(0, 1, 0, 5);
            mainProgressFile.Size = new Size(39, 21);
            mainProgressFile.TabIndex = 22;
            mainProgressFile.Text = "Ready";
            // 
            // mainBoxProgress
            // 
            mainBoxProgress.Controls.Add(mainProgressBar);
            mainBoxProgress.Controls.Add(mainProgressFile);
            mainBoxProgress.Dock = DockStyle.Top;
            mainBoxProgress.Location = new Point(6, 262);
            mainBoxProgress.Name = "mainBoxProgress";
            mainBoxProgress.Padding = new Padding(6);
            mainBoxProgress.Size = new Size(427, 74);
            mainBoxProgress.TabIndex = 24;
            mainBoxProgress.TabStop = false;
            mainBoxProgress.Text = "Progress";
            // 
            // mainProgressBar
            // 
            mainProgressBar.Dock = DockStyle.Top;
            mainProgressBar.Location = new Point(6, 22);
            mainProgressBar.Name = "mainProgressBar";
            mainProgressBar.Size = new Size(415, 23);
            mainProgressBar.TabIndex = 23;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(439, 384);
            Controls.Add(mainBoxProgress);
            Controls.Add(cfgBoxTileSettings);
            Controls.Add(cfgBoxPrimarySettings);
            Controls.Add(mainButtonStart);
            Controls.Add(mainButtonToggleConsole);
            Controls.Add(mainButtonCancel);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            HelpButton = true;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "MainForm";
            Padding = new Padding(6);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Animation2Tilemap";
            HelpButtonClicked += MainForm_HelpButtonClicked;
            Load += MainForm_Load;
            cfgBoxTileSettings.ResumeLayout(false);
            cfgBoxTileSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)cfgFrameTime).EndInit();
            ((System.ComponentModel.ISupportInitialize)cfgTileSpacing).EndInit();
            ((System.ComponentModel.ISupportInitialize)cfgTileMargin).EndInit();
            ((System.ComponentModel.ISupportInitialize)cfgTileHeight).EndInit();
            ((System.ComponentModel.ISupportInitialize)cfgTileWidth).EndInit();
            cfgBoxPrimarySettings.ResumeLayout(false);
            cfgBoxPrimarySettings.PerformLayout();
            mainBoxProgress.ResumeLayout(false);
            mainBoxProgress.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private Button mainButtonCancel;
        private Button mainButtonStart;
        private Button mainButtonToggleConsole;
        private GroupBox cfgBoxTileSettings;
        private TextBox cfgBackgroundColor;
        private Label cfgLabelLayerFormat;
        private Label cfgLabelFrameTime;
        private ComboBox cfgLayerFormat;
        private Label cfgLabelBackgroundColor;
        private NumericUpDown cfgFrameTime;
        private Label cfgLabelTileSpacing;
        private Label cfgLabelTileMargin;
        private Label cfgLabelTileHeight;
        private Label cfgLabelTileWidth;
        private NumericUpDown cfgTileSpacing;
        private NumericUpDown cfgTileMargin;
        private NumericUpDown cfgTileHeight;
        private NumericUpDown cfgTileWidth;
        private GroupBox cfgBoxPrimarySettings;
        private Button cfgButtonOutput;
        private TextBox cfgOutputPath;
        private Button cfgButtonInput;
        private TextBox cfgInputPath;
        private Label cfgLabelOutput;
        private Label cfgLabelInput;
        private Label mainProgressFile;
        private GroupBox mainBoxProgress;
        private ProgressBar mainProgressBar;
    }
}