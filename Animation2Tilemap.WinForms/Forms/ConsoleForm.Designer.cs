﻿namespace Animation2Tilemap.WinForms.Forms
{
    partial class ConsoleForm
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
            outputBox = new RichTextBox();
            SuspendLayout();
            // 
            // outputBox
            // 
            outputBox.BackColor = Color.Black;
            outputBox.Dock = DockStyle.Fill;
            outputBox.Font = new Font("Cascadia Mono", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            outputBox.ForeColor = Color.White;
            outputBox.Location = new Point(0, 0);
            outputBox.Name = "outputBox";
            outputBox.Size = new Size(924, 501);
            outputBox.TabIndex = 0;
            outputBox.Text = "";
            outputBox.KeyDown += outputBox_KeyDown;
            // 
            // ConsoleForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(924, 501);
            Controls.Add(outputBox);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ConsoleForm";
            ShowIcon = false;
            Text = "Animation2Tilemap - Console";
            ResumeLayout(false);
        }

        #endregion

        private RichTextBox outputBox;
    }
}