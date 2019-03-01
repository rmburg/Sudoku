namespace SudokuUI
{
    partial class SudokuGenerator
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
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.buttonGenerateSolution = new System.Windows.Forms.Button();
            this.buttonGeneratePuzzle = new System.Windows.Forms.Button();
            this.bgW_GenerateSolution = new System.ComponentModel.BackgroundWorker();
            this.bgW_GeneratePuzzle = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 82);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(264, 23);
            this.progressBar1.TabIndex = 0;
            // 
            // buttonGenerateSolution
            // 
            this.buttonGenerateSolution.Location = new System.Drawing.Point(33, 28);
            this.buttonGenerateSolution.Name = "buttonGenerateSolution";
            this.buttonGenerateSolution.Size = new System.Drawing.Size(108, 31);
            this.buttonGenerateSolution.TabIndex = 1;
            this.buttonGenerateSolution.Text = "Generate a solution";
            this.buttonGenerateSolution.UseVisualStyleBackColor = true;
            // 
            // buttonGeneratePuzzle
            // 
            this.buttonGeneratePuzzle.Location = new System.Drawing.Point(147, 28);
            this.buttonGeneratePuzzle.Name = "buttonGeneratePuzzle";
            this.buttonGeneratePuzzle.Size = new System.Drawing.Size(108, 31);
            this.buttonGeneratePuzzle.TabIndex = 1;
            this.buttonGeneratePuzzle.Text = "Generate a puzzle";
            this.buttonGeneratePuzzle.UseVisualStyleBackColor = true;
            // 
            // bgW_GenerateSolution
            // 
            this.bgW_GenerateSolution.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // SudokuGenerator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(288, 117);
            this.Controls.Add(this.buttonGeneratePuzzle);
            this.Controls.Add(this.buttonGenerateSolution);
            this.Controls.Add(this.progressBar1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "SudokuGenerator";
            this.Text = "Generator";
            this.Load += new System.EventHandler(this.SudokuGenerator_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button buttonGenerateSolution;
        private System.Windows.Forms.Button buttonGeneratePuzzle;
        private System.ComponentModel.BackgroundWorker bgW_GenerateSolution;
        private System.ComponentModel.BackgroundWorker bgW_GeneratePuzzle;
    }
}