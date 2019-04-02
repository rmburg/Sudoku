namespace SudokuUI
{
    partial class SettingsMenu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsMenu));
            this.checkBox_ColorHelp = new System.Windows.Forms.CheckBox();
            this.groupBox_GeneralSettings = new System.Windows.Forms.GroupBox();
            this.button_SaveSettings = new System.Windows.Forms.Button();
            this.button_Cancel = new System.Windows.Forms.Button();
            this.radioButtonSolveSlow = new System.Windows.Forms.RadioButton();
            this.radioButtonSolveFast = new System.Windows.Forms.RadioButton();
            this.groupBoxSolveOption = new System.Windows.Forms.GroupBox();
            this.groupBox_GeneralSettings.SuspendLayout();
            this.groupBoxSolveOption.SuspendLayout();
            this.SuspendLayout();
            // 
            // checkBox_ColorHelp
            // 
            this.checkBox_ColorHelp.AutoSize = true;
            this.checkBox_ColorHelp.Checked = true;
            this.checkBox_ColorHelp.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_ColorHelp.Location = new System.Drawing.Point(6, 19);
            this.checkBox_ColorHelp.Name = "checkBox_ColorHelp";
            this.checkBox_ColorHelp.Size = new System.Drawing.Size(118, 17);
            this.checkBox_ColorHelp.TabIndex = 0;
            this.checkBox_ColorHelp.Text = "Enable \"color help\"";
            this.checkBox_ColorHelp.UseVisualStyleBackColor = true;
            // 
            // groupBox_GeneralSettings
            // 
            this.groupBox_GeneralSettings.Controls.Add(this.groupBoxSolveOption);
            this.groupBox_GeneralSettings.Controls.Add(this.checkBox_ColorHelp);
            this.groupBox_GeneralSettings.Location = new System.Drawing.Point(12, 12);
            this.groupBox_GeneralSettings.Name = "groupBox_GeneralSettings";
            this.groupBox_GeneralSettings.Size = new System.Drawing.Size(206, 119);
            this.groupBox_GeneralSettings.TabIndex = 1;
            this.groupBox_GeneralSettings.TabStop = false;
            this.groupBox_GeneralSettings.Text = "General settings";
            // 
            // button_SaveSettings
            // 
            this.button_SaveSettings.Location = new System.Drawing.Point(12, 137);
            this.button_SaveSettings.Name = "button_SaveSettings";
            this.button_SaveSettings.Size = new System.Drawing.Size(100, 25);
            this.button_SaveSettings.TabIndex = 2;
            this.button_SaveSettings.Text = "Save settings";
            this.button_SaveSettings.UseVisualStyleBackColor = true;
            this.button_SaveSettings.Click += new System.EventHandler(this.button_SaveSettings_Click);
            // 
            // button_Cancel
            // 
            this.button_Cancel.Location = new System.Drawing.Point(118, 137);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(100, 25);
            this.button_Cancel.TabIndex = 2;
            this.button_Cancel.Text = "Cancel";
            this.button_Cancel.UseVisualStyleBackColor = true;
            this.button_Cancel.Click += new System.EventHandler(this.button_Cancel_Click);
            // 
            // radioButtonSolveSlow
            // 
            this.radioButtonSolveSlow.AutoSize = true;
            this.radioButtonSolveSlow.Checked = true;
            this.radioButtonSolveSlow.Location = new System.Drawing.Point(14, 19);
            this.radioButtonSolveSlow.Name = "radioButtonSolveSlow";
            this.radioButtonSolveSlow.Size = new System.Drawing.Size(84, 17);
            this.radioButtonSolveSlow.TabIndex = 1;
            this.radioButtonSolveSlow.TabStop = true;
            this.radioButtonSolveSlow.Text = "Visible, Slow";
            this.radioButtonSolveSlow.UseVisualStyleBackColor = true;
            // 
            // radioButtonSolveFast
            // 
            this.radioButtonSolveFast.AutoSize = true;
            this.radioButtonSolveFast.Location = new System.Drawing.Point(14, 42);
            this.radioButtonSolveFast.Name = "radioButtonSolveFast";
            this.radioButtonSolveFast.Size = new System.Drawing.Size(89, 17);
            this.radioButtonSolveFast.TabIndex = 2;
            this.radioButtonSolveFast.Text = "Invisible, Fast";
            this.radioButtonSolveFast.UseVisualStyleBackColor = true;
            // 
            // groupBoxSolveOption
            // 
            this.groupBoxSolveOption.Controls.Add(this.radioButtonSolveSlow);
            this.groupBoxSolveOption.Controls.Add(this.radioButtonSolveFast);
            this.groupBoxSolveOption.Location = new System.Drawing.Point(6, 42);
            this.groupBoxSolveOption.Name = "groupBoxSolveOption";
            this.groupBoxSolveOption.Size = new System.Drawing.Size(194, 71);
            this.groupBoxSolveOption.TabIndex = 3;
            this.groupBoxSolveOption.TabStop = false;
            this.groupBoxSolveOption.Text = "Solving";
            // 
            // SettingsMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(230, 170);
            this.Controls.Add(this.button_Cancel);
            this.Controls.Add(this.button_SaveSettings);
            this.Controls.Add(this.groupBox_GeneralSettings);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "SettingsMenu";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.groupBox_GeneralSettings.ResumeLayout(false);
            this.groupBox_GeneralSettings.PerformLayout();
            this.groupBoxSolveOption.ResumeLayout(false);
            this.groupBoxSolveOption.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBox_ColorHelp;
        private System.Windows.Forms.GroupBox groupBox_GeneralSettings;
        private System.Windows.Forms.Button button_SaveSettings;
        private System.Windows.Forms.Button button_Cancel;
        private System.Windows.Forms.GroupBox groupBoxSolveOption;
        private System.Windows.Forms.RadioButton radioButtonSolveSlow;
        private System.Windows.Forms.RadioButton radioButtonSolveFast;
    }
}