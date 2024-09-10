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
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            progressBar1 = new ProgressBar();
            splitContainer1 = new SplitContainer();
            tabPage2 = new TabPage();
            groupBox1 = new GroupBox();
            label1 = new Label();
            splitContainer4 = new SplitContainer();
            configButtonOutput = new Button();
            configTextOutput = new TextBox();
            label2 = new Label();
            splitContainer2 = new SplitContainer();
            button4 = new Button();
            textBox1 = new TextBox();
            groupBox2 = new GroupBox();
            groupBox3 = new GroupBox();
            tabControl2 = new TabControl();
            tabPage1 = new TabPage();
            richTextBox1 = new RichTextBox();
            tabControl1 = new TabControl();
            numericUpDown1 = new NumericUpDown();
            numericUpDown2 = new NumericUpDown();
            numericUpDown3 = new NumericUpDown();
            numericUpDown4 = new NumericUpDown();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            splitContainer3 = new SplitContainer();
            textBox2 = new TextBox();
            button5 = new Button();
            label7 = new Label();
            label8 = new Label();
            label9 = new Label();
            numericUpDown6 = new NumericUpDown();
            configLayerFormat = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            tabPage2.SuspendLayout();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer4).BeginInit();
            splitContainer4.Panel1.SuspendLayout();
            splitContainer4.Panel2.SuspendLayout();
            splitContainer4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer2).BeginInit();
            splitContainer2.Panel1.SuspendLayout();
            splitContainer2.Panel2.SuspendLayout();
            splitContainer2.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            tabControl2.SuspendLayout();
            tabPage1.SuspendLayout();
            tabControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitContainer3).BeginInit();
            splitContainer3.Panel1.SuspendLayout();
            splitContainer3.Panel2.SuspendLayout();
            splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown6).BeginInit();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(9, 542);
            button1.Name = "button1";
            button1.Size = new Size(113, 35);
            button1.TabIndex = 0;
            button1.Text = "Start";
            button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Location = new Point(128, 542);
            button2.Name = "button2";
            button2.Size = new Size(113, 35);
            button2.TabIndex = 1;
            button2.Text = "Cancel";
            button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            button3.Location = new Point(312, 542);
            button3.Name = "button3";
            button3.Size = new Size(113, 35);
            button3.TabIndex = 2;
            button3.Text = "Show Console";
            button3.UseVisualStyleBackColor = true;
            // 
            // progressBar1
            // 
            progressBar1.Location = new Point(9, 513);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(416, 23);
            progressBar1.TabIndex = 3;
            // 
            // splitContainer1
            // 
            splitContainer1.Location = new Point(9, 9);
            splitContainer1.Name = "splitContainer1";
            splitContainer1.Orientation = Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(tabControl2);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(tabControl1);
            splitContainer1.Size = new Size(416, 498);
            splitContainer1.SplitterDistance = 325;
            splitContainer1.TabIndex = 4;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(groupBox3);
            tabPage2.Controls.Add(groupBox2);
            tabPage2.Controls.Add(groupBox1);
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(408, 297);
            tabPage2.TabIndex = 0;
            tabPage2.Text = "Configuration";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(splitContainer2);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(splitContainer4);
            groupBox1.Controls.Add(label1);
            groupBox1.Dock = DockStyle.Top;
            groupBox1.Location = new Point(3, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(402, 130);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Primary Settings";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Dock = DockStyle.Top;
            label1.Location = new Point(3, 19);
            label1.Name = "label1";
            label1.Padding = new Padding(0, 5, 0, 1);
            label1.Size = new Size(94, 21);
            label1.TabIndex = 0;
            label1.Text = "Input File/Folder";
            // 
            // splitContainer4
            // 
            splitContainer4.Dock = DockStyle.Top;
            splitContainer4.FixedPanel = FixedPanel.Panel2;
            splitContainer4.IsSplitterFixed = true;
            splitContainer4.Location = new Point(3, 40);
            splitContainer4.Margin = new Padding(3, 2, 3, 2);
            splitContainer4.Name = "splitContainer4";
            // 
            // splitContainer4.Panel1
            // 
            splitContainer4.Panel1.Controls.Add(configTextOutput);
            splitContainer4.Panel1.Padding = new Padding(3, 2, 1, 2);
            // 
            // splitContainer4.Panel2
            // 
            splitContainer4.Panel2.Controls.Add(configButtonOutput);
            splitContainer4.Panel2.Padding = new Padding(0, 2, 3, 2);
            splitContainer4.Size = new Size(396, 28);
            splitContainer4.SplitterDistance = 351;
            splitContainer4.TabIndex = 3;
            // 
            // configButtonOutput
            // 
            configButtonOutput.Dock = DockStyle.Fill;
            configButtonOutput.Location = new Point(0, 2);
            configButtonOutput.Margin = new Padding(3, 2, 3, 2);
            configButtonOutput.Name = "configButtonOutput";
            configButtonOutput.Size = new Size(38, 24);
            configButtonOutput.TabIndex = 2;
            configButtonOutput.Text = "...";
            configButtonOutput.UseVisualStyleBackColor = true;
            // 
            // configTextOutput
            // 
            configTextOutput.Dock = DockStyle.Fill;
            configTextOutput.Location = new Point(3, 2);
            configTextOutput.Margin = new Padding(3, 2, 3, 2);
            configTextOutput.Name = "configTextOutput";
            configTextOutput.Size = new Size(347, 23);
            configTextOutput.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Dock = DockStyle.Top;
            label2.Location = new Point(3, 68);
            label2.Name = "label2";
            label2.Padding = new Padding(0, 5, 0, 1);
            label2.Size = new Size(81, 21);
            label2.TabIndex = 4;
            label2.Text = "Output Folder";
            // 
            // splitContainer2
            // 
            splitContainer2.Dock = DockStyle.Top;
            splitContainer2.FixedPanel = FixedPanel.Panel2;
            splitContainer2.IsSplitterFixed = true;
            splitContainer2.Location = new Point(3, 89);
            splitContainer2.Margin = new Padding(3, 2, 3, 2);
            splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            splitContainer2.Panel1.Controls.Add(textBox1);
            splitContainer2.Panel1.Padding = new Padding(3, 2, 1, 2);
            // 
            // splitContainer2.Panel2
            // 
            splitContainer2.Panel2.Controls.Add(button4);
            splitContainer2.Panel2.Padding = new Padding(0, 2, 3, 2);
            splitContainer2.Size = new Size(396, 28);
            splitContainer2.SplitterDistance = 351;
            splitContainer2.TabIndex = 5;
            // 
            // button4
            // 
            button4.Dock = DockStyle.Fill;
            button4.Location = new Point(0, 2);
            button4.Margin = new Padding(3, 2, 3, 2);
            button4.Name = "button4";
            button4.Size = new Size(38, 24);
            button4.TabIndex = 2;
            button4.Text = "...";
            button4.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            textBox1.Dock = DockStyle.Fill;
            textBox1.Location = new Point(3, 2);
            textBox1.Margin = new Padding(3, 2, 3, 2);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(347, 23);
            textBox1.TabIndex = 1;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(label6);
            groupBox2.Controls.Add(label5);
            groupBox2.Controls.Add(label4);
            groupBox2.Controls.Add(label3);
            groupBox2.Controls.Add(numericUpDown4);
            groupBox2.Controls.Add(numericUpDown3);
            groupBox2.Controls.Add(numericUpDown2);
            groupBox2.Controls.Add(numericUpDown1);
            groupBox2.Dock = DockStyle.Top;
            groupBox2.Location = new Point(3, 133);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(402, 78);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "Tile Settings";
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(configLayerFormat);
            groupBox3.Controls.Add(label8);
            groupBox3.Controls.Add(label9);
            groupBox3.Controls.Add(numericUpDown6);
            groupBox3.Controls.Add(splitContainer3);
            groupBox3.Controls.Add(label7);
            groupBox3.Dock = DockStyle.Top;
            groupBox3.Location = new Point(3, 211);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(402, 78);
            groupBox3.TabIndex = 2;
            groupBox3.TabStop = false;
            groupBox3.Text = "Tilemap Settings";
            // 
            // tabControl2
            // 
            tabControl2.Controls.Add(tabPage2);
            tabControl2.Dock = DockStyle.Fill;
            tabControl2.Location = new Point(0, 0);
            tabControl2.Name = "tabControl2";
            tabControl2.SelectedIndex = 0;
            tabControl2.Size = new Size(416, 325);
            tabControl2.TabIndex = 0;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(richTextBox1);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(408, 141);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Console";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // richTextBox1
            // 
            richTextBox1.Dock = DockStyle.Fill;
            richTextBox1.Location = new Point(3, 3);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(402, 135);
            richTextBox1.TabIndex = 0;
            richTextBox1.Text = "";
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(416, 169);
            tabControl1.TabIndex = 0;
            // 
            // numericUpDown1
            // 
            numericUpDown1.Location = new Point(6, 43);
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(93, 23);
            numericUpDown1.TabIndex = 1;
            // 
            // numericUpDown2
            // 
            numericUpDown2.Location = new Point(105, 43);
            numericUpDown2.Name = "numericUpDown2";
            numericUpDown2.Size = new Size(93, 23);
            numericUpDown2.TabIndex = 2;
            // 
            // numericUpDown3
            // 
            numericUpDown3.Location = new Point(204, 43);
            numericUpDown3.Name = "numericUpDown3";
            numericUpDown3.Size = new Size(93, 23);
            numericUpDown3.TabIndex = 3;
            // 
            // numericUpDown4
            // 
            numericUpDown4.Location = new Point(303, 43);
            numericUpDown4.Name = "numericUpDown4";
            numericUpDown4.Size = new Size(93, 23);
            numericUpDown4.TabIndex = 4;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(6, 19);
            label3.Name = "label3";
            label3.Padding = new Padding(0, 5, 0, 1);
            label3.Size = new Size(60, 21);
            label3.TabIndex = 5;
            label3.Text = "Tile Width";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(105, 19);
            label4.Name = "label4";
            label4.Padding = new Padding(0, 5, 0, 1);
            label4.Size = new Size(64, 21);
            label4.TabIndex = 6;
            label4.Text = "Tile Height";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(204, 19);
            label5.Name = "label5";
            label5.Padding = new Padding(0, 5, 0, 1);
            label5.Size = new Size(66, 21);
            label5.TabIndex = 7;
            label5.Text = "Tile Margin";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(303, 19);
            label6.Name = "label6";
            label6.Padding = new Padding(0, 5, 0, 1);
            label6.Size = new Size(70, 21);
            label6.TabIndex = 8;
            label6.Text = "Tile Spacing";
            // 
            // splitContainer3
            // 
            splitContainer3.FixedPanel = FixedPanel.Panel2;
            splitContainer3.IsSplitterFixed = true;
            splitContainer3.Location = new Point(3, 40);
            splitContainer3.Margin = new Padding(3, 2, 3, 2);
            splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            splitContainer3.Panel1.Controls.Add(textBox2);
            splitContainer3.Panel1.Padding = new Padding(3, 2, 1, 2);
            // 
            // splitContainer3.Panel2
            // 
            splitContainer3.Panel2.Controls.Add(button5);
            splitContainer3.Panel2.Padding = new Padding(0, 2, 3, 2);
            splitContainer3.Size = new Size(195, 28);
            splitContainer3.SplitterDistance = 150;
            splitContainer3.TabIndex = 7;
            // 
            // textBox2
            // 
            textBox2.Dock = DockStyle.Fill;
            textBox2.Location = new Point(3, 2);
            textBox2.Margin = new Padding(3, 2, 3, 2);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(146, 23);
            textBox2.TabIndex = 1;
            // 
            // button5
            // 
            button5.Dock = DockStyle.Fill;
            button5.Location = new Point(0, 2);
            button5.Margin = new Padding(3, 2, 3, 2);
            button5.Name = "button5";
            button5.Size = new Size(38, 24);
            button5.TabIndex = 2;
            button5.Text = "...";
            button5.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Dock = DockStyle.Top;
            label7.Location = new Point(3, 19);
            label7.Name = "label7";
            label7.Padding = new Padding(0, 5, 0, 1);
            label7.Size = new Size(103, 21);
            label7.TabIndex = 6;
            label7.Text = "Background Color";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(303, 19);
            label8.Name = "label8";
            label8.Padding = new Padding(0, 5, 0, 1);
            label8.Size = new Size(76, 21);
            label8.TabIndex = 12;
            label8.Text = "Layer Format";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(204, 19);
            label9.Name = "label9";
            label9.Padding = new Padding(0, 5, 0, 1);
            label9.Size = new Size(69, 21);
            label9.TabIndex = 11;
            label9.Text = "Frame Time";
            // 
            // numericUpDown6
            // 
            numericUpDown6.Location = new Point(204, 43);
            numericUpDown6.Name = "numericUpDown6";
            numericUpDown6.Size = new Size(93, 23);
            numericUpDown6.TabIndex = 9;
            // 
            // configLayerFormat
            // 
            configLayerFormat.DropDownStyle = ComboBoxStyle.DropDownList;
            configLayerFormat.FormattingEnabled = true;
            configLayerFormat.Items.AddRange(new object[] { "base64", "gzip + base64", "zlib + base64", "csv" });
            configLayerFormat.Location = new Point(303, 44);
            configLayerFormat.Margin = new Padding(3, 2, 3, 2);
            configLayerFormat.Name = "configLayerFormat";
            configLayerFormat.Size = new Size(93, 23);
            configLayerFormat.TabIndex = 13;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(434, 586);
            Controls.Add(splitContainer1);
            Controls.Add(progressBar1);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            HelpButton = true;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "MainForm";
            Padding = new Padding(6);
            Text = "MainForm";
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            tabPage2.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            splitContainer4.Panel1.ResumeLayout(false);
            splitContainer4.Panel1.PerformLayout();
            splitContainer4.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer4).EndInit();
            splitContainer4.ResumeLayout(false);
            splitContainer2.Panel1.ResumeLayout(false);
            splitContainer2.Panel1.PerformLayout();
            splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer2).EndInit();
            splitContainer2.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            tabControl2.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown2).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown3).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown4).EndInit();
            splitContainer3.Panel1.ResumeLayout(false);
            splitContainer3.Panel1.PerformLayout();
            splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer3).EndInit();
            splitContainer3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)numericUpDown6).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button button1;
        private Button button2;
        private Button button3;
        private ProgressBar progressBar1;
        private SplitContainer splitContainer1;
        private TabControl tabControl2;
        private TabPage tabPage2;
        private GroupBox groupBox3;
        private GroupBox groupBox2;
        private GroupBox groupBox1;
        private SplitContainer splitContainer2;
        private TextBox textBox1;
        private Button button4;
        private Label label2;
        private SplitContainer splitContainer4;
        private TextBox configTextOutput;
        private Button configButtonOutput;
        private Label label1;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private RichTextBox richTextBox1;
        private Label label3;
        private NumericUpDown numericUpDown4;
        private NumericUpDown numericUpDown3;
        private NumericUpDown numericUpDown2;
        private NumericUpDown numericUpDown1;
        private Label label6;
        private Label label5;
        private Label label4;
        private SplitContainer splitContainer3;
        private TextBox textBox2;
        private Button button5;
        private Label label7;
        private Label label8;
        private Label label9;
        private NumericUpDown numericUpDown6;
        private ComboBox configLayerFormat;
    }
}