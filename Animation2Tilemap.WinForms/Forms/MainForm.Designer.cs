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
            mainProgressBar = new ProgressBar();
            mainSplitContainer = new SplitContainer();
            mainTabConfiguration = new TabControl();
            tabPage2 = new TabPage();
            cfgBoxTilemapSettings = new GroupBox();
            cfgLayerFormat = new ComboBox();
            cfgLabelLayerFormat = new Label();
            cfgLabelFrameTime = new Label();
            cfgFrameTime = new NumericUpDown();
            cfgSplitterBackgroundColor = new SplitContainer();
            cfgBackgroundColor = new TextBox();
            cfgButtonBackgroundColor = new Button();
            cfgLabelBackgroundColor = new Label();
            cfgBoxTileSettings = new GroupBox();
            cfgLabelTileSpacing = new Label();
            cfgLabelTileMargin = new Label();
            cfgLabelTileHeight = new Label();
            cfgLabelTileWidth = new Label();
            cfgTileSpacing = new NumericUpDown();
            cfgTileMargin = new NumericUpDown();
            cfgTileHeight = new NumericUpDown();
            cfgTileWidth = new NumericUpDown();
            cfgBoxPrimarySettings = new GroupBox();
            cfgSplitterOutput = new SplitContainer();
            cfgTextOutput = new TextBox();
            cfgButtonOutput = new Button();
            cfgLabelOutput = new Label();
            cfgSplitterInput = new SplitContainer();
            cfgTextInput = new TextBox();
            cfgButtonInput = new Button();
            cfgLabelInput = new Label();
            mainTabConsole = new TabControl();
            tabPage1 = new TabPage();
            mainTextBoxConsole = new RichTextBox();
            ((System.ComponentModel.ISupportInitialize)mainSplitContainer).BeginInit();
            mainSplitContainer.Panel1.SuspendLayout();
            mainSplitContainer.Panel2.SuspendLayout();
            mainSplitContainer.SuspendLayout();
            mainTabConfiguration.SuspendLayout();
            tabPage2.SuspendLayout();
            cfgBoxTilemapSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)cfgFrameTime).BeginInit();
            ((System.ComponentModel.ISupportInitialize)cfgSplitterBackgroundColor).BeginInit();
            cfgSplitterBackgroundColor.Panel1.SuspendLayout();
            cfgSplitterBackgroundColor.Panel2.SuspendLayout();
            cfgSplitterBackgroundColor.SuspendLayout();
            cfgBoxTileSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)cfgTileSpacing).BeginInit();
            ((System.ComponentModel.ISupportInitialize)cfgTileMargin).BeginInit();
            ((System.ComponentModel.ISupportInitialize)cfgTileHeight).BeginInit();
            ((System.ComponentModel.ISupportInitialize)cfgTileWidth).BeginInit();
            cfgBoxPrimarySettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)cfgSplitterOutput).BeginInit();
            cfgSplitterOutput.Panel1.SuspendLayout();
            cfgSplitterOutput.Panel2.SuspendLayout();
            cfgSplitterOutput.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)cfgSplitterInput).BeginInit();
            cfgSplitterInput.Panel1.SuspendLayout();
            cfgSplitterInput.Panel2.SuspendLayout();
            cfgSplitterInput.SuspendLayout();
            mainTabConsole.SuspendLayout();
            tabPage1.SuspendLayout();
            SuspendLayout();
            // 
            // mainButtonStart
            // 
            mainButtonStart.Location = new Point(9, 542);
            mainButtonStart.Name = "mainButtonStart";
            mainButtonStart.Size = new Size(113, 35);
            mainButtonStart.TabIndex = 0;
            mainButtonStart.Text = "Start";
            mainButtonStart.UseVisualStyleBackColor = true;
            // 
            // mainButtonCancel
            // 
            mainButtonCancel.Location = new Point(128, 542);
            mainButtonCancel.Name = "mainButtonCancel";
            mainButtonCancel.Size = new Size(113, 35);
            mainButtonCancel.TabIndex = 1;
            mainButtonCancel.Text = "Cancel";
            mainButtonCancel.UseVisualStyleBackColor = true;
            // 
            // mainButtonToggleConsole
            // 
            mainButtonToggleConsole.Location = new Point(312, 542);
            mainButtonToggleConsole.Name = "mainButtonToggleConsole";
            mainButtonToggleConsole.Size = new Size(113, 35);
            mainButtonToggleConsole.TabIndex = 2;
            mainButtonToggleConsole.Text = "Show Console";
            mainButtonToggleConsole.UseVisualStyleBackColor = true;
            // 
            // mainProgressBar
            // 
            mainProgressBar.Location = new Point(9, 513);
            mainProgressBar.Name = "mainProgressBar";
            mainProgressBar.Size = new Size(416, 23);
            mainProgressBar.TabIndex = 3;
            // 
            // mainSplitContainer
            // 
            mainSplitContainer.Location = new Point(9, 9);
            mainSplitContainer.Name = "mainSplitContainer";
            mainSplitContainer.Orientation = Orientation.Horizontal;
            // 
            // mainSplitContainer.Panel1
            // 
            mainSplitContainer.Panel1.Controls.Add(mainTabConfiguration);
            // 
            // mainSplitContainer.Panel2
            // 
            mainSplitContainer.Panel2.Controls.Add(mainTabConsole);
            mainSplitContainer.Size = new Size(416, 498);
            mainSplitContainer.SplitterDistance = 325;
            mainSplitContainer.TabIndex = 4;
            // 
            // mainTabConfiguration
            // 
            mainTabConfiguration.Controls.Add(tabPage2);
            mainTabConfiguration.Dock = DockStyle.Fill;
            mainTabConfiguration.Location = new Point(0, 0);
            mainTabConfiguration.Name = "mainTabConfiguration";
            mainTabConfiguration.SelectedIndex = 0;
            mainTabConfiguration.Size = new Size(416, 325);
            mainTabConfiguration.TabIndex = 0;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(cfgBoxTilemapSettings);
            tabPage2.Controls.Add(cfgBoxTileSettings);
            tabPage2.Controls.Add(cfgBoxPrimarySettings);
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(408, 297);
            tabPage2.TabIndex = 0;
            tabPage2.Text = "Configuration";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // cfgBoxTilemapSettings
            // 
            cfgBoxTilemapSettings.Controls.Add(cfgLayerFormat);
            cfgBoxTilemapSettings.Controls.Add(cfgLabelLayerFormat);
            cfgBoxTilemapSettings.Controls.Add(cfgLabelFrameTime);
            cfgBoxTilemapSettings.Controls.Add(cfgFrameTime);
            cfgBoxTilemapSettings.Controls.Add(cfgSplitterBackgroundColor);
            cfgBoxTilemapSettings.Controls.Add(cfgLabelBackgroundColor);
            cfgBoxTilemapSettings.Dock = DockStyle.Top;
            cfgBoxTilemapSettings.Location = new Point(3, 211);
            cfgBoxTilemapSettings.Name = "cfgBoxTilemapSettings";
            cfgBoxTilemapSettings.Size = new Size(402, 78);
            cfgBoxTilemapSettings.TabIndex = 2;
            cfgBoxTilemapSettings.TabStop = false;
            cfgBoxTilemapSettings.Text = "Tilemap Settings";
            // 
            // cfgLayerFormat
            // 
            cfgLayerFormat.DropDownStyle = ComboBoxStyle.DropDownList;
            cfgLayerFormat.FormattingEnabled = true;
            cfgLayerFormat.Items.AddRange(new object[] { "base64", "gzip + base64", "zlib + base64", "csv" });
            cfgLayerFormat.Location = new Point(303, 44);
            cfgLayerFormat.Margin = new Padding(3, 2, 3, 2);
            cfgLayerFormat.Name = "cfgLayerFormat";
            cfgLayerFormat.Size = new Size(93, 23);
            cfgLayerFormat.TabIndex = 13;
            // 
            // cfgLabelLayerFormat
            // 
            cfgLabelLayerFormat.AutoSize = true;
            cfgLabelLayerFormat.Location = new Point(303, 19);
            cfgLabelLayerFormat.Name = "cfgLabelLayerFormat";
            cfgLabelLayerFormat.Padding = new Padding(0, 5, 0, 1);
            cfgLabelLayerFormat.Size = new Size(76, 21);
            cfgLabelLayerFormat.TabIndex = 12;
            cfgLabelLayerFormat.Text = "Layer Format";
            // 
            // cfgLabelFrameTime
            // 
            cfgLabelFrameTime.AutoSize = true;
            cfgLabelFrameTime.Location = new Point(204, 19);
            cfgLabelFrameTime.Name = "cfgLabelFrameTime";
            cfgLabelFrameTime.Padding = new Padding(0, 5, 0, 1);
            cfgLabelFrameTime.Size = new Size(69, 21);
            cfgLabelFrameTime.TabIndex = 11;
            cfgLabelFrameTime.Text = "Frame Time";
            // 
            // cfgFrameTime
            // 
            cfgFrameTime.Location = new Point(204, 43);
            cfgFrameTime.Name = "cfgFrameTime";
            cfgFrameTime.Size = new Size(93, 23);
            cfgFrameTime.TabIndex = 9;
            // 
            // cfgSplitterBackgroundColor
            // 
            cfgSplitterBackgroundColor.FixedPanel = FixedPanel.Panel2;
            cfgSplitterBackgroundColor.IsSplitterFixed = true;
            cfgSplitterBackgroundColor.Location = new Point(3, 40);
            cfgSplitterBackgroundColor.Margin = new Padding(3, 2, 3, 2);
            cfgSplitterBackgroundColor.Name = "cfgSplitterBackgroundColor";
            // 
            // cfgSplitterBackgroundColor.Panel1
            // 
            cfgSplitterBackgroundColor.Panel1.Controls.Add(cfgBackgroundColor);
            cfgSplitterBackgroundColor.Panel1.Padding = new Padding(3, 2, 1, 2);
            // 
            // cfgSplitterBackgroundColor.Panel2
            // 
            cfgSplitterBackgroundColor.Panel2.Controls.Add(cfgButtonBackgroundColor);
            cfgSplitterBackgroundColor.Panel2.Padding = new Padding(0, 2, 3, 2);
            cfgSplitterBackgroundColor.Size = new Size(195, 28);
            cfgSplitterBackgroundColor.SplitterDistance = 150;
            cfgSplitterBackgroundColor.TabIndex = 7;
            // 
            // cfgBackgroundColor
            // 
            cfgBackgroundColor.Dock = DockStyle.Fill;
            cfgBackgroundColor.Location = new Point(3, 2);
            cfgBackgroundColor.Margin = new Padding(3, 2, 3, 2);
            cfgBackgroundColor.Name = "cfgBackgroundColor";
            cfgBackgroundColor.Size = new Size(146, 23);
            cfgBackgroundColor.TabIndex = 1;
            // 
            // cfgButtonBackgroundColor
            // 
            cfgButtonBackgroundColor.Dock = DockStyle.Fill;
            cfgButtonBackgroundColor.Location = new Point(0, 2);
            cfgButtonBackgroundColor.Margin = new Padding(3, 2, 3, 2);
            cfgButtonBackgroundColor.Name = "cfgButtonBackgroundColor";
            cfgButtonBackgroundColor.Size = new Size(38, 24);
            cfgButtonBackgroundColor.TabIndex = 2;
            cfgButtonBackgroundColor.Text = "...";
            cfgButtonBackgroundColor.UseVisualStyleBackColor = true;
            // 
            // cfgLabelBackgroundColor
            // 
            cfgLabelBackgroundColor.AutoSize = true;
            cfgLabelBackgroundColor.Dock = DockStyle.Top;
            cfgLabelBackgroundColor.Location = new Point(3, 19);
            cfgLabelBackgroundColor.Name = "cfgLabelBackgroundColor";
            cfgLabelBackgroundColor.Padding = new Padding(0, 5, 0, 1);
            cfgLabelBackgroundColor.Size = new Size(103, 21);
            cfgLabelBackgroundColor.TabIndex = 6;
            cfgLabelBackgroundColor.Text = "Background Color";
            // 
            // cfgBoxTileSettings
            // 
            cfgBoxTileSettings.Controls.Add(cfgLabelTileSpacing);
            cfgBoxTileSettings.Controls.Add(cfgLabelTileMargin);
            cfgBoxTileSettings.Controls.Add(cfgLabelTileHeight);
            cfgBoxTileSettings.Controls.Add(cfgLabelTileWidth);
            cfgBoxTileSettings.Controls.Add(cfgTileSpacing);
            cfgBoxTileSettings.Controls.Add(cfgTileMargin);
            cfgBoxTileSettings.Controls.Add(cfgTileHeight);
            cfgBoxTileSettings.Controls.Add(cfgTileWidth);
            cfgBoxTileSettings.Dock = DockStyle.Top;
            cfgBoxTileSettings.Location = new Point(3, 133);
            cfgBoxTileSettings.Name = "cfgBoxTileSettings";
            cfgBoxTileSettings.Size = new Size(402, 78);
            cfgBoxTileSettings.TabIndex = 1;
            cfgBoxTileSettings.TabStop = false;
            cfgBoxTileSettings.Text = "Tile Settings";
            // 
            // cfgLabelTileSpacing
            // 
            cfgLabelTileSpacing.AutoSize = true;
            cfgLabelTileSpacing.Location = new Point(303, 19);
            cfgLabelTileSpacing.Name = "cfgLabelTileSpacing";
            cfgLabelTileSpacing.Padding = new Padding(0, 5, 0, 1);
            cfgLabelTileSpacing.Size = new Size(70, 21);
            cfgLabelTileSpacing.TabIndex = 8;
            cfgLabelTileSpacing.Text = "Tile Spacing";
            // 
            // cfgLabelTileMargin
            // 
            cfgLabelTileMargin.AutoSize = true;
            cfgLabelTileMargin.Location = new Point(204, 19);
            cfgLabelTileMargin.Name = "cfgLabelTileMargin";
            cfgLabelTileMargin.Padding = new Padding(0, 5, 0, 1);
            cfgLabelTileMargin.Size = new Size(66, 21);
            cfgLabelTileMargin.TabIndex = 7;
            cfgLabelTileMargin.Text = "Tile Margin";
            // 
            // cfgLabelTileHeight
            // 
            cfgLabelTileHeight.AutoSize = true;
            cfgLabelTileHeight.Location = new Point(105, 19);
            cfgLabelTileHeight.Name = "cfgLabelTileHeight";
            cfgLabelTileHeight.Padding = new Padding(0, 5, 0, 1);
            cfgLabelTileHeight.Size = new Size(64, 21);
            cfgLabelTileHeight.TabIndex = 6;
            cfgLabelTileHeight.Text = "Tile Height";
            // 
            // cfgLabelTileWidth
            // 
            cfgLabelTileWidth.AutoSize = true;
            cfgLabelTileWidth.Location = new Point(6, 19);
            cfgLabelTileWidth.Name = "cfgLabelTileWidth";
            cfgLabelTileWidth.Padding = new Padding(0, 5, 0, 1);
            cfgLabelTileWidth.Size = new Size(60, 21);
            cfgLabelTileWidth.TabIndex = 5;
            cfgLabelTileWidth.Text = "Tile Width";
            // 
            // cfgTileSpacing
            // 
            cfgTileSpacing.Location = new Point(303, 43);
            cfgTileSpacing.Name = "cfgTileSpacing";
            cfgTileSpacing.Size = new Size(93, 23);
            cfgTileSpacing.TabIndex = 4;
            // 
            // cfgTileMargin
            // 
            cfgTileMargin.Location = new Point(204, 43);
            cfgTileMargin.Name = "cfgTileMargin";
            cfgTileMargin.Size = new Size(93, 23);
            cfgTileMargin.TabIndex = 3;
            // 
            // cfgTileHeight
            // 
            cfgTileHeight.Location = new Point(105, 43);
            cfgTileHeight.Name = "cfgTileHeight";
            cfgTileHeight.Size = new Size(93, 23);
            cfgTileHeight.TabIndex = 2;
            // 
            // cfgTileWidth
            // 
            cfgTileWidth.Location = new Point(6, 43);
            cfgTileWidth.Name = "cfgTileWidth";
            cfgTileWidth.Size = new Size(93, 23);
            cfgTileWidth.TabIndex = 1;
            // 
            // cfgBoxPrimarySettings
            // 
            cfgBoxPrimarySettings.Controls.Add(cfgSplitterOutput);
            cfgBoxPrimarySettings.Controls.Add(cfgLabelOutput);
            cfgBoxPrimarySettings.Controls.Add(cfgSplitterInput);
            cfgBoxPrimarySettings.Controls.Add(cfgLabelInput);
            cfgBoxPrimarySettings.Dock = DockStyle.Top;
            cfgBoxPrimarySettings.Location = new Point(3, 3);
            cfgBoxPrimarySettings.Name = "cfgBoxPrimarySettings";
            cfgBoxPrimarySettings.Size = new Size(402, 130);
            cfgBoxPrimarySettings.TabIndex = 0;
            cfgBoxPrimarySettings.TabStop = false;
            cfgBoxPrimarySettings.Text = "Primary Settings";
            // 
            // cfgSplitterOutput
            // 
            cfgSplitterOutput.Dock = DockStyle.Top;
            cfgSplitterOutput.FixedPanel = FixedPanel.Panel2;
            cfgSplitterOutput.IsSplitterFixed = true;
            cfgSplitterOutput.Location = new Point(3, 89);
            cfgSplitterOutput.Margin = new Padding(3, 2, 3, 2);
            cfgSplitterOutput.Name = "cfgSplitterOutput";
            // 
            // cfgSplitterOutput.Panel1
            // 
            cfgSplitterOutput.Panel1.Controls.Add(cfgTextOutput);
            cfgSplitterOutput.Panel1.Padding = new Padding(3, 2, 1, 2);
            // 
            // cfgSplitterOutput.Panel2
            // 
            cfgSplitterOutput.Panel2.Controls.Add(cfgButtonOutput);
            cfgSplitterOutput.Panel2.Padding = new Padding(0, 2, 3, 2);
            cfgSplitterOutput.Size = new Size(396, 28);
            cfgSplitterOutput.SplitterDistance = 351;
            cfgSplitterOutput.TabIndex = 5;
            // 
            // cfgTextOutput
            // 
            cfgTextOutput.Dock = DockStyle.Fill;
            cfgTextOutput.Location = new Point(3, 2);
            cfgTextOutput.Margin = new Padding(3, 2, 3, 2);
            cfgTextOutput.Name = "cfgTextOutput";
            cfgTextOutput.Size = new Size(347, 23);
            cfgTextOutput.TabIndex = 1;
            // 
            // cfgButtonOutput
            // 
            cfgButtonOutput.Dock = DockStyle.Fill;
            cfgButtonOutput.Location = new Point(0, 2);
            cfgButtonOutput.Margin = new Padding(3, 2, 3, 2);
            cfgButtonOutput.Name = "cfgButtonOutput";
            cfgButtonOutput.Size = new Size(38, 24);
            cfgButtonOutput.TabIndex = 2;
            cfgButtonOutput.Text = "...";
            cfgButtonOutput.UseVisualStyleBackColor = true;
            // 
            // cfgLabelOutput
            // 
            cfgLabelOutput.AutoSize = true;
            cfgLabelOutput.Dock = DockStyle.Top;
            cfgLabelOutput.Location = new Point(3, 68);
            cfgLabelOutput.Name = "cfgLabelOutput";
            cfgLabelOutput.Padding = new Padding(0, 5, 0, 1);
            cfgLabelOutput.Size = new Size(81, 21);
            cfgLabelOutput.TabIndex = 4;
            cfgLabelOutput.Text = "Output Folder";
            // 
            // cfgSplitterInput
            // 
            cfgSplitterInput.Dock = DockStyle.Top;
            cfgSplitterInput.FixedPanel = FixedPanel.Panel2;
            cfgSplitterInput.IsSplitterFixed = true;
            cfgSplitterInput.Location = new Point(3, 40);
            cfgSplitterInput.Margin = new Padding(3, 2, 3, 2);
            cfgSplitterInput.Name = "cfgSplitterInput";
            // 
            // cfgSplitterInput.Panel1
            // 
            cfgSplitterInput.Panel1.Controls.Add(cfgTextInput);
            cfgSplitterInput.Panel1.Padding = new Padding(3, 2, 1, 2);
            // 
            // cfgSplitterInput.Panel2
            // 
            cfgSplitterInput.Panel2.Controls.Add(cfgButtonInput);
            cfgSplitterInput.Panel2.Padding = new Padding(0, 2, 3, 2);
            cfgSplitterInput.Size = new Size(396, 28);
            cfgSplitterInput.SplitterDistance = 351;
            cfgSplitterInput.TabIndex = 3;
            // 
            // cfgTextInput
            // 
            cfgTextInput.Dock = DockStyle.Fill;
            cfgTextInput.Location = new Point(3, 2);
            cfgTextInput.Margin = new Padding(3, 2, 3, 2);
            cfgTextInput.Name = "cfgTextInput";
            cfgTextInput.Size = new Size(347, 23);
            cfgTextInput.TabIndex = 1;
            // 
            // cfgButtonInput
            // 
            cfgButtonInput.Dock = DockStyle.Fill;
            cfgButtonInput.Location = new Point(0, 2);
            cfgButtonInput.Margin = new Padding(3, 2, 3, 2);
            cfgButtonInput.Name = "cfgButtonInput";
            cfgButtonInput.Size = new Size(38, 24);
            cfgButtonInput.TabIndex = 2;
            cfgButtonInput.Text = "...";
            cfgButtonInput.UseVisualStyleBackColor = true;
            // 
            // cfgLabelInput
            // 
            cfgLabelInput.AutoSize = true;
            cfgLabelInput.Dock = DockStyle.Top;
            cfgLabelInput.Location = new Point(3, 19);
            cfgLabelInput.Name = "cfgLabelInput";
            cfgLabelInput.Padding = new Padding(0, 5, 0, 1);
            cfgLabelInput.Size = new Size(94, 21);
            cfgLabelInput.TabIndex = 0;
            cfgLabelInput.Text = "Input File/Folder";
            // 
            // mainTabConsole
            // 
            mainTabConsole.Controls.Add(tabPage1);
            mainTabConsole.Dock = DockStyle.Fill;
            mainTabConsole.Location = new Point(0, 0);
            mainTabConsole.Name = "mainTabConsole";
            mainTabConsole.SelectedIndex = 0;
            mainTabConsole.Size = new Size(416, 169);
            mainTabConsole.TabIndex = 0;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(mainTextBoxConsole);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(408, 141);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Console";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // mainTextBoxConsole
            // 
            mainTextBoxConsole.Dock = DockStyle.Fill;
            mainTextBoxConsole.Location = new Point(3, 3);
            mainTextBoxConsole.Name = "mainTextBoxConsole";
            mainTextBoxConsole.Size = new Size(402, 135);
            mainTextBoxConsole.TabIndex = 0;
            mainTextBoxConsole.Text = "";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(434, 586);
            Controls.Add(mainSplitContainer);
            Controls.Add(mainProgressBar);
            Controls.Add(mainButtonToggleConsole);
            Controls.Add(mainButtonCancel);
            Controls.Add(mainButtonStart);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            HelpButton = true;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "MainForm";
            Padding = new Padding(6);
            Text = "Animation2Tilemap";
            mainSplitContainer.Panel1.ResumeLayout(false);
            mainSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)mainSplitContainer).EndInit();
            mainSplitContainer.ResumeLayout(false);
            mainTabConfiguration.ResumeLayout(false);
            tabPage2.ResumeLayout(false);
            cfgBoxTilemapSettings.ResumeLayout(false);
            cfgBoxTilemapSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)cfgFrameTime).EndInit();
            cfgSplitterBackgroundColor.Panel1.ResumeLayout(false);
            cfgSplitterBackgroundColor.Panel1.PerformLayout();
            cfgSplitterBackgroundColor.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)cfgSplitterBackgroundColor).EndInit();
            cfgSplitterBackgroundColor.ResumeLayout(false);
            cfgBoxTileSettings.ResumeLayout(false);
            cfgBoxTileSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)cfgTileSpacing).EndInit();
            ((System.ComponentModel.ISupportInitialize)cfgTileMargin).EndInit();
            ((System.ComponentModel.ISupportInitialize)cfgTileHeight).EndInit();
            ((System.ComponentModel.ISupportInitialize)cfgTileWidth).EndInit();
            cfgBoxPrimarySettings.ResumeLayout(false);
            cfgBoxPrimarySettings.PerformLayout();
            cfgSplitterOutput.Panel1.ResumeLayout(false);
            cfgSplitterOutput.Panel1.PerformLayout();
            cfgSplitterOutput.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)cfgSplitterOutput).EndInit();
            cfgSplitterOutput.ResumeLayout(false);
            cfgSplitterInput.Panel1.ResumeLayout(false);
            cfgSplitterInput.Panel1.PerformLayout();
            cfgSplitterInput.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)cfgSplitterInput).EndInit();
            cfgSplitterInput.ResumeLayout(false);
            mainTabConsole.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Button cfgButtonBackgroundColor;
        private Button cfgButtonInput;
        private Button cfgButtonOutput;
        private Button mainButtonCancel;
        private Button mainButtonStart;
        private Button mainButtonToggleConsole;
        private ComboBox cfgLayerFormat;
        private GroupBox cfgBoxPrimarySettings;
        private GroupBox cfgBoxTilemapSettings;
        private GroupBox cfgBoxTileSettings;
        private Label cfgLabelBackgroundColor;
        private Label cfgLabelFrameTime;
        private Label cfgLabelInput;
        private Label cfgLabelLayerFormat;
        private Label cfgLabelOutput;
        private Label cfgLabelTileHeight;
        private Label cfgLabelTileMargin;
        private Label cfgLabelTileSpacing;
        private Label cfgLabelTileWidth;
        private NumericUpDown cfgFrameTime;
        private NumericUpDown cfgTileHeight;
        private NumericUpDown cfgTileMargin;
        private NumericUpDown cfgTileSpacing;
        private NumericUpDown cfgTileWidth;
        private ProgressBar mainProgressBar;
        private RichTextBox mainTextBoxConsole;
        private SplitContainer cfgSplitterBackgroundColor;
        private SplitContainer cfgSplitterInput;
        private SplitContainer cfgSplitterOutput;
        private SplitContainer mainSplitContainer;
        private TabControl mainTabConfiguration;
        private TabControl mainTabConsole;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TextBox cfgBackgroundColor;
        private TextBox cfgTextInput;
        private TextBox cfgTextOutput;
    }
}