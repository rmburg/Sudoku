using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Sudoku;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
using static SudokuUI.Lib;

namespace SudokuUI
{
    public partial class MainWindowForm : Form
    {
        public static Font font_cell_default = new Font("Verdana", 9f);
        public static Font font_cell_bold = new Font("Verdana", 9f, FontStyle.Bold);
        public Grid internal_grid = new Grid(9);

        List<Grid> possible_solutions = new List<Grid>();

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
            ui_grid.RowTemplate.Height = 30;
            
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

            ui_grid.SelectionChanged += new EventHandler(SelectionUpdate);
            ui_grid.KeyPress += new KeyPressEventHandler(KeyPressEvent);
            


            //apply style to cells
            ui_grid.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            ui_grid.GridColor = Color.Black;
            ui_grid.Font = font_cell_default;
            // apply background pattern
            SetColorsDefault();

            if (arg != string.Empty)
            {
                LoadSudokuFile(arg);
            }
        }

        public void UpdateGrid() // put the values of the internal grid into the UI
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (internal_grid.Get(i, j) == 0)
                    {
                        ui_grid[j, i].Value = null;
                        MarkPremade(new Coords(i, j), false);
                    }
                    else
                    {
                        if (internal_grid.Get(i, j) < 0) // if it is a "premade" cell
                        {
                            ui_grid[j, i].Value = - internal_grid.Get(i, j);
                            MarkPremade(new Coords(i, j), true);
                        }
                        else
                        {
                            ui_grid[j, i].Value = internal_grid.Get(i, j);
                            MarkPremade(new Coords(i, j), false);
                        }
                    }
                }
            }
        }

        private void SetColorsDefault()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if ((i < 3 || i > 5) ^ (j < 3 || j > 5)) // 3x3 checkerboard pattern
                    {
                        ui_grid[i, j].Style.BackColor = Color.DarkGray;
                    }
                    else
                    {
                        ui_grid[i, j].Style.BackColor = Color.White;
                    }
                }
            }
        }

        private void UpdateHighlightColors()
        {
            if (!(bool)Properties.Settings.Default["ColorHelpEnabled"])
            {
                SetColorsDefault();
                return;
            }
            //set all colors to normal
            SetColorsDefault();
            //set all numbers that are the same as the selected one to e.g. blue
            int selectedNum = Math.Abs(internal_grid.Get(CurrentCellCoords()));
            if (selectedNum != 0)
            {
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        if (Math.Abs(internal_grid.Get(new Coords(i, j))) == selectedNum)
                        {
                            ui_grid[j, i].Style.BackColor = Color.LightBlue;
                        }
                    }
                }
            }
        }

        // called when a new cell is selected
        private void SelectionUpdate(object sender, EventArgs e)
        {
            UpdateHighlightColors();
        }

        public void SetCell(Coords coords, int value)
        {
            internal_grid.Set(coords, value);
            UpdateGrid();
        }

        //for doing additional stuff when the user sets a cell (so not while loading, solving etc...)
        public void UserSetCell(Coords coords, int value)
        {
            SetCell(coords, value);
            if (!internal_grid.ContainsZeros())
            {
                bool correct = internal_grid.IsValid();
                GridComplete(correct);
            }
            UpdateHighlightColors();
        }

        public void GridComplete(bool correct)
        {
            MessageBox.Show(correct?"Congratulations! You found a valid solution.":"Sadly, this solution is not correct.");
        }

        private void ButtonClick(object sender, EventArgs e)
        {
            if (sender == buttonRemove)
            {
                if (internal_grid.Get(CurrentCellCoords()) >= 0) // if it is not a premade cell
                {
                    UserSetCell(CurrentCellCoords(), 0);
                    return;
                }
                return;
            }
            string btnText = ((Button)sender).Text;
            if (internal_grid.Get(CurrentCellCoords()) >= 0)
            {
                UserSetCell(CurrentCellCoords(), int.Parse(btnText)); // bad bad bad i know i know i know
            }
        }

        private Coords CurrentCellCoords()
        {
            int x, y;

            y = ui_grid.CurrentCell.ColumnIndex;
            x = ui_grid.CurrentCell.RowIndex;

            return new Coords(x, y);
        }

        void ResetGrid()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (internal_grid.Get(i, j) > 0)
                    {
                        SetCell(new Coords(i, j), 0);
                    }
                }
            }
            UpdateHighlightColors();
        }

        void ClearGridAll()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    SetCell(new Coords(i, j), 0);
                }
            }
            UpdateHighlightColors();
        }

        private void ResetEvent(object sender, EventArgs e)
        {
            ResetGrid();
        }

        private void KeyPressEvent(object sender, KeyPressEventArgs e)
        {
            char[] allowed = new char[] { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', (char)Keys.Back};
            if (internal_grid.Get(CurrentCellCoords()) >= 0)
            {
                if (e.KeyChar == (char)Keys.Back || e.KeyChar == '0')
                {
                    UserSetCell(CurrentCellCoords(), 0);
                    return;
                }
                if (allowed.Contains(e.KeyChar))
                {
                    UserSetCell(CurrentCellCoords(), int.Parse("" + e.KeyChar));
                }
            }
        }

        private void MarkPremade(Coords coords, bool isPremade)
        {
            ui_grid[coords.y, coords.x].Style.Font = isPremade?font_cell_bold:font_cell_default; //set font weight
        }

        public void LoadSudokuFile(string path)
        {
            Grid deserialized_grid = new Grid(JsonConvert.DeserializeObject<int[][]>(File.ReadAllText(path)), 9);
            internal_grid = deserialized_grid;
            UpdateGrid();
            UpdateHighlightColors();
        }

        private void loadSudokuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenLoadDialog();
        }

        private void saveSudokuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenSaveDialog();
        }

        void OpenLoadDialog()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = Application.StartupPath;
                openFileDialog.Filter = "sudoku files (*.sudoku)|*.sudoku|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 0;
                openFileDialog.RestoreDirectory = false;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        LoadSudokuFile(openFileDialog.FileName);
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("Error: " + e.Message, "An error has occured");
                    }
                }
            }
        }

        void OpenSaveDialog()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.InitialDirectory = Application.StartupPath;
            saveFileDialog.Filter = "sudoku files (*.sudoku)|*.sudoku|All files (*.*)|*.*";
            saveFileDialog.FilterIndex = 0;
            saveFileDialog.RestoreDirectory = false;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    SaveSudokuFile(internal_grid, saveFileDialog.FileName);
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error: " + e.Message, "An error has occured");
                }
            }
        }

        private void leavePremadeNumbersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure? This will delete all the numbers you entered.", "Please confirm", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                ResetGrid();
            }
        }

        private void clearAllNumbersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure? This will delete all numbers on the grid.", "Please confirm", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                ClearGridAll();
            }
        }

        private void generateASudokuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SudokuGenerator sv = new SudokuGenerator();
            sv.Show();
        }

        public bool SolveRecursive(bool findAll)
        {
            if (!internal_grid.ContainsZeros()) // if it is fully solved
            {
                possible_solutions.Add(internal_grid.Clone());
                return true;
            }
            else
            {
                // find the first empty cell
                Coords emptyCell = internal_grid.GetFirstEmptyCell();

                // get all possibilities for the first empty cell
                
                List<int> possibilities = internal_grid.GetAllPossibilities(emptyCell);
                
                foreach (int item in possibilities) // try each possible number
                {
                    SetCell(emptyCell, item);

                    bool solutionFound = SolveRecursive(findAll); // if the next fuction call returns true (has found a solution)
                    if (solutionFound && !findAll) // if only one solution should be found, return after one is found
                    {
                        return true;
                    }
                }
                //backtrack
                SetCell(emptyCell, 0);
                return false;
            }
        }

        private void worker_solve_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            bool findAll = (bool)e.Argument;
            possible_solutions.Clear(); // reset the list of possible solutions

            if (findAll) // if the user wats to find all solutions
            {
                SolveRecursive(true);
                MessageBox.Show("Solutions found: "+ possible_solutions.Count);
                if (possible_solutions.Count > 0)
                {
                    internal_grid = possible_solutions[0];
                    UpdateGrid();
                }
            }
            else
            {
                SolveRecursive(false);
                if (possible_solutions.Count > 0)
                {
                    internal_grid = possible_solutions[0];
                    UpdateGrid();
                }
                else
                {
                    MessageBox.Show("No solution was found.");
                }
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
        }
    }

    public class UIgrid : DataGridView // derived class just to avoid the standard behavior of keys like enter, tab and back
    {
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter || e.KeyData == Keys.Tab || e.KeyData == Keys.Back)
            {
                e.Handled = true;
            }
            base.OnKeyDown(e);
        }
    }

    public class UniquenessChecker
    {
        bool solutionFound = false;
        bool secondSolFound = false;
        Grid grid_to_check;
        public bool Check(Grid grid)
        {
            grid_to_check = grid.Clone();
            CheckUniqueRecursive();
            return (solutionFound && !secondSolFound);
        }

        private void CheckUniqueRecursive()
        {
            if (!secondSolFound)
            {
                if (!grid_to_check.ContainsZeros()) // if it is fully solved
                {
                    if (solutionFound)
                    {
                        secondSolFound = true;
                        return;
                    }
                    solutionFound = true;
                    return;
                }
                else
                {
                    // find the first empty cell
                    Coords emptyCell = grid_to_check.GetFirstEmptyCell();

                    // get all possibilities for the first empty cell
                    List<int> possibilities = grid_to_check.GetAllPossibilities(emptyCell);

                    foreach (int item in possibilities) // try each possible number
                    {
                        grid_to_check.Set(emptyCell, item);

                        CheckUniqueRecursive(); // if the next fuction call returns true (has found a solution)
                    }
                    //backtrack
                    grid_to_check.Set(emptyCell, 0);
                }
            }
        }
    }
}
