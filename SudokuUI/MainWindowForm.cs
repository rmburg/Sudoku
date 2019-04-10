using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;

namespace SudokuUI
{
    public partial class MainWindowForm : Form
    {
        List<Grid> possible_solutions = new List<Grid>();
        string arg;
        private Grid gridcopy;
        

        public MainWindowForm(string arg)
        {
            InitializeComponent();


            StartPosition = FormStartPosition.CenterScreen;

            //add table rows
            ui_grid.Rows.Add();
            ui_grid.Rows.Add();
            ui_grid.Rows.Add();
            ui_grid.Rows.Add();
            ui_grid.Rows.Add();
            ui_grid.Rows.Add();
            ui_grid.Rows.Add();
            ui_grid.Rows.Add();
            ui_grid.Rows.Add();

            //add event handlers to buttons
            button1.Click += ButtonClick;
            button2.Click += ButtonClick;
            button3.Click += ButtonClick;
            button4.Click += ButtonClick;
            button5.Click += ButtonClick;
            button6.Click += ButtonClick;
            button7.Click += ButtonClick;
            button8.Click += ButtonClick;
            button9.Click += ButtonClick;
            buttonRemove.Click += ButtonClick;

            ui_grid.SelectionChanged += SelectionUpdate;
            ui_grid.KeyPress += KeyPressEvent;

            this.arg = arg;
        }

        // called when a new cell is selected
        private void SelectionUpdate(object sender, EventArgs e)
        {
            ui_grid.UpdateHighlightColors();
        }

        private void ButtonClick(object sender, EventArgs e)
        {
            if (sender == buttonRemove)
            {
                if (ui_grid.internal_grid.Get(ui_grid.CurrentCellPoint()) >= 0) // if it is not a premade cell
                {
                    ui_grid.UserSetCell(ui_grid.CurrentCellPoint(), 0);
                    return;
                }
                return;
            }
            string btnText = ((Button)sender).Text;
            if (ui_grid.internal_grid.Get(ui_grid.CurrentCellPoint()) >= 0)
            {
                ui_grid.UserSetCell(ui_grid.CurrentCellPoint(), int.Parse(btnText)); // bad bad bad i know i know i know
            }
        }

        private void ResetEvent(object sender, EventArgs e)
        {
            ui_grid.ResetGrid();
        }

        private void KeyPressEvent(object sender, KeyPressEventArgs e)
        {
            char[] allowed = new char[] { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', (char)Keys.Back};
            if (ui_grid.internal_grid.Get(ui_grid.CurrentCellPoint()) >= 0)
            {
                if (e.KeyChar == (char)Keys.Back || e.KeyChar == '0')
                {
                    ui_grid.UserSetCell(ui_grid.CurrentCellPoint(), 0);
                    return;
                }
                if (allowed.Contains(e.KeyChar))
                {
                    ui_grid.UserSetCell(ui_grid.CurrentCellPoint(), int.Parse("" + e.KeyChar));
                }
            }
        }

        private void loadSudokuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ui_grid.OpenLoadDialog();
        }

        private void saveSudokuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ui_grid.Save();
        }

        private void leavePremadeNumbersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure? This will delete all the numbers you entered.", "Please confirm", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                ui_grid.ResetGrid();
            }
        }

        private void clearAllNumbersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure? This will delete all numbers on the grid.", "Please confirm", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                ui_grid.ClearGridAll();
            }
        }

        private void generateASudokuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SudokuGenerator sg = new SudokuGenerator(this);
            sg.Show();
        }

        private bool SolveRecursive(bool findAll)
        {
            if (!ui_grid.internal_grid.ContainsZeros()) // if it is fully solved
            {
                possible_solutions.Add(ui_grid.internal_grid.Clone());
                return true;
            }
            else
            {
                // find the first empty cell
                Point emptyCell = ui_grid.internal_grid.GetFirstEmptyCell();

                // get all possibilities for the first empty cell
                List<int> possibilities = ui_grid.internal_grid.GetAllPossibilities(emptyCell);

                foreach (int item in possibilities) // try each possible number
                {
                    ui_grid.SetCell(emptyCell, item);

                    // if the next fuction call returns true (has found a solution) and the user wants to find just 1 solution, return
                    if (SolveRecursive(findAll) && !findAll)
                    {
                        return true;
                    }
                }
                //backtrack
                ui_grid.SetCell(emptyCell, 0);
                return false;
            }
        }

        private bool SolveRecursiveSilent(bool findAll)
        {
            if (!gridcopy.ContainsZeros()) // if it is fully solved
            {
                possible_solutions.Add(gridcopy.Clone());
                return true;
            }
            else
            {
                // find the first empty cell
                Point emptyCell = gridcopy.GetFirstEmptyCell();

                // get all possibilities for the first empty cell
                List<int> possibilities = gridcopy.GetAllPossibilities(emptyCell);

                foreach (int item in possibilities) // try each possible number
                {
                    gridcopy.Set(emptyCell, item);

                    // if the next fuction call returns true (has found a solution) and the user wants to find just 1 solution, return
                    if (SolveRecursiveSilent(findAll) && !findAll)
                    {
                        return true;
                    }
                }
                //backtrack
                gridcopy.Set(emptyCell, 0);
                return false;
            }
        }

        private void worker_solve_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            bool findAll = (bool)e.Argument;
            possible_solutions.Clear(); // reset the list of possible solutions
            bool solveFast = (bool)Properties.Settings.Default["FastSolving"];
            gridcopy = ui_grid.internal_grid.Clone();

            if (solveFast)
            {
                SolveRecursiveSilent(findAll);
            }
            else
            {
                SolveRecursive(findAll);
            }
            MessageBox.Show("Solutions found: " + possible_solutions.Count);
            if (possible_solutions.Count > 0)
            {
                SudokuViewer sv = new SudokuViewer(possible_solutions);
                sv.ShowDialog();
            }
        }

        private void findOneSolutionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure? This will solve the grid. If you entered any wrong numbers, no solution may be possible.", "Please confirm", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                worker_solve.RunWorkerAsync(false);
            }
        }

        private void findAllSolutionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure? This will solve the grid. If you entered any wrong numbers, no solution may be possible.", "Please confirm", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                worker_solve.RunWorkerAsync(true);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            if (ModifierKeys == Keys.Control)
            {
                MessageBox.Show("Secret!");
            }
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingsMenu sm = new SettingsMenu();
            sm.ShowDialog();
            ui_grid.UpdateHighlightColors();
        }

        private void viewerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SudokuViewer sv = new SudokuViewer(new List<Grid>());
            sv.Show();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutPage ap = new AboutPage();
            ap.ShowDialog();
        }

        private void MainWindowForm_Load(object sender, EventArgs e)
        {
            if (arg != string.Empty)
            {
                ui_grid.LoadSudokuFile(arg);
            }
        }

        private void exportAsImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ui_grid.OpenImageSaveDialog();
        }
    }
}
