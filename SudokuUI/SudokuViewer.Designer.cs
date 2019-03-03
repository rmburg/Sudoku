namespace SudokuUI
{
    partial class SudokuViewer
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SudokuViewer));
            this.ui_grid = new SudokuUI.UIgrid();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buttonGenSolution = new System.Windows.Forms.Button();
            this.buttonGenPuzzle = new System.Windows.Forms.Button();
            this.bgW_GenSolution = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.ui_grid)).BeginInit();
            this.SuspendLayout();
            // 
            // ui_grid
            // 
            this.ui_grid.AllowUserToAddRows = false;
            this.ui_grid.AllowUserToDeleteRows = false;
            this.ui_grid.AllowUserToResizeColumns = false;
            this.ui_grid.AllowUserToResizeRows = false;
            this.ui_grid.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.ui_grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ui_grid.ColumnHeadersVisible = false;
            this.ui_grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7,
            this.Column8,
            this.Column9});
            this.ui_grid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.ui_grid.Location = new System.Drawing.Point(12, 12);
            this.ui_grid.MultiSelect = false;
            this.ui_grid.Name = "ui_grid";
            this.ui_grid.ReadOnly = true;
            this.ui_grid.RowHeadersVisible = false;
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(1);
            this.ui_grid.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.ui_grid.RowTemplate.Height = 30;
            this.ui_grid.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.ui_grid.Size = new System.Drawing.Size(273, 273);
            this.ui_grid.TabIndex = 1;
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column1.HeaderText = "1";
            this.Column1.MinimumWidth = 30;
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column1.Width = 30;
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column2.HeaderText = "2";
            this.Column2.MinimumWidth = 30;
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column2.Width = 30;
            // 
            // Column3
            // 
            this.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column3.HeaderText = "3";
            this.Column3.MinimumWidth = 30;
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column3.Width = 30;
            // 
            // Column4
            // 
            this.Column4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column4.HeaderText = "4";
            this.Column4.MinimumWidth = 30;
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column4.Width = 30;
            // 
            // Column5
            // 
            this.Column5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column5.HeaderText = "5";
            this.Column5.MinimumWidth = 30;
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column5.Width = 30;
            // 
            // Column6
            // 
            this.Column6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column6.HeaderText = "6";
            this.Column6.MinimumWidth = 30;
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column6.Width = 30;
            // 
            // Column7
            // 
            this.Column7.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column7.HeaderText = "7";
            this.Column7.MinimumWidth = 30;
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            this.Column7.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column7.Width = 30;
            // 
            // Column8
            // 
            this.Column8.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column8.HeaderText = "8";
            this.Column8.MinimumWidth = 30;
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            this.Column8.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column8.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column8.Width = 30;
            // 
            // Column9
            // 
            this.Column9.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column9.HeaderText = "9";
            this.Column9.MinimumWidth = 30;
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            this.Column9.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column9.Width = 30;
            // 
            // buttonGenSolution
            // 
            this.buttonGenSolution.Location = new System.Drawing.Point(333, 12);
            this.buttonGenSolution.Name = "buttonGenSolution";
            this.buttonGenSolution.Size = new System.Drawing.Size(125, 23);
            this.buttonGenSolution.TabIndex = 2;
            this.buttonGenSolution.Text = "Generate a solution";
            this.buttonGenSolution.UseVisualStyleBackColor = true;
            this.buttonGenSolution.Click += new System.EventHandler(this.buttonGenSolution_Click);
            // 
            // buttonGenPuzzle
            // 
            this.buttonGenPuzzle.Location = new System.Drawing.Point(333, 41);
            this.buttonGenPuzzle.Name = "buttonGenPuzzle";
            this.buttonGenPuzzle.Size = new System.Drawing.Size(125, 23);
            this.buttonGenPuzzle.TabIndex = 2;
            this.buttonGenPuzzle.Text = "Generate a puzzle";
            this.buttonGenPuzzle.UseVisualStyleBackColor = true;
            this.buttonGenPuzzle.Click += new System.EventHandler(this.buttonGenPuzzle_Click);
            // 
            // bgW_GenSolution
            // 
            this.bgW_GenSolution.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgW_GenSolution_DoWork);
            // 
            // SudokuViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(498, 297);
            this.Controls.Add(this.buttonGenPuzzle);
            this.Controls.Add(this.buttonGenSolution);
            this.Controls.Add(this.ui_grid);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SudokuViewer";
            this.Text = "SudokuViewer";
            this.Load += new System.EventHandler(this.SudokuViewer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ui_grid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public UIgrid ui_grid;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.Button buttonGenSolution;
        private System.Windows.Forms.Button buttonGenPuzzle;
        private System.ComponentModel.BackgroundWorker bgW_GenSolution;
    }
}