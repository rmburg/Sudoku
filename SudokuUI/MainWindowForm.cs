using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Sudoku;
using System.IO;
using Newtonsoft.Json;
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
                if (ui_grid.internal_grid.Get(ui_grid.CurrentCellCoords()) >= 0) // if it is not a premade cell
                {
                    ui_grid.UserSetCell(ui_grid.CurrentCellCoords(), 0);
                    return;
                }
                return;
            }
            string btnText = ((Button)sender).Text;
            if (ui_grid.internal_grid.Get(ui_grid.CurrentCellCoords()) >= 0)
            {
                ui_grid.UserSetCell(ui_grid.CurrentCellCoords(), int.Parse(btnText)); // bad bad bad i know i know i know
            }
        }

        private void ResetEvent(object sender, EventArgs e)
        {
            ui_grid.ResetGrid();
        }

        private void KeyPressEvent(object sender, KeyPressEventArgs e)
        {
            char[] allowed = new char[] { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', (char)Keys.Back};
            if (ui_grid.internal_grid.Get(ui_grid.CurrentCellCoords()) >= 0)
            {
                if (e.KeyChar == (char)Keys.Back || e.KeyChar == '0')
                {
                    ui_grid.UserSetCell(ui_grid.CurrentCellCoords(), 0);
                    return;
                }
                if (allowed.Contains(e.KeyChar))
                {
                    ui_grid.UserSetCell(ui_grid.CurrentCellCoords(), int.Parse("" + e.KeyChar));
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
            SudokuGenerator sg = new SudokuGenerator();
            sg.Show();
        }

        public bool SolveRecursive(bool findAll)
        {
            if (!ui_grid.internal_grid.ContainsZeros()) // if it is fully solved
            {
                possible_solutions.Add(ui_grid.internal_grid.Clone());
                return true;
            }
            else
            {
                // find the first empty cell
                Coords emptyCell = ui_grid.internal_grid.GetFirstEmptyCell();

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

        public bool SolveRecursiveSilent(bool findAll)
        {
            if (!gridcopy.ContainsZeros()) // if it is fully solved
            {
                possible_solutions.Add(gridcopy.Clone());
                return true;
            }
            else
            {
                // find the first empty cell
                Coords emptyCell = gridcopy.GetFirstEmptyCell();

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

    public class UIgrid : DataGridView
    {
        public Grid internal_grid;

        public static readonly Font font_cell_default = new Font("Verdana", 9f);
        public static readonly Font font_cell_bold = new Font("Verdana", 9f, FontStyle.Bold);

        public UIgrid()
        {
            internal_grid = new Grid(9);

            RowTemplate.Height = 30;

            //apply style to cells
            DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            GridColor = Color.Black;
            Font = font_cell_default;
        }

        protected override void OnKeyDown(KeyEventArgs e) // just to avoid the standard behavior of keys like enter, tab and back
        {
            if (e.KeyData == Keys.Enter || e.KeyData == Keys.Tab || e.KeyData == Keys.Back)
            {
                e.Handled = true;
            }
            base.OnKeyDown(e);
        }

        public void UpdateGrid() // put the values of the internal grid into the UI
        {
            try
            {
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        if (internal_grid.Get(i, j) == 0)
                        {
                            this[j, i].Value = null;
                            MarkPremade(new Coords(i, j), false);
                        }
                        else
                        {
                            if (internal_grid.Get(i, j) < 0) // if it is a "premade" cell
                            {
                                this[j, i].Value = -internal_grid.Get(i, j);
                                MarkPremade(new Coords(i, j), true);
                            }
                            else
                            {
                                this[j, i].Value = internal_grid.Get(i, j);
                                MarkPremade(new Coords(i, j), false);
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show(ColumnCount + ", " + RowCount);
            }
        }

        public void ExportImage(string path)
        {
            Bitmap bitmap = new Bitmap(Width, Height);
            Rectangle rect = new Rectangle(0, 0, Width, Height);
            CurrentCell = null;
            SetColorsDefault();
            DrawToBitmap(bitmap, rect);
            bitmap.Save(path);
        }

        private void MarkPremade(Coords coords, bool isPremade)
        {
            this[coords.y, coords.x].Style.Font = isPremade ? font_cell_bold : font_cell_default; //set font weight
        }

        public void SetColorsDefault()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if ((i < 3 || i > 5) ^ (j < 3 || j > 5)) // 3x3 checkerboard pattern
                    {
                        this[i, j].Style.BackColor = Color.DarkGray;
                    }
                    else
                    {
                        this[i, j].Style.BackColor = Color.White;
                    }
                }
            }
        }

        public void SetCell(Coords coords, int value)
        {
            internal_grid.Set(coords, value);
            UpdateGrid(); ;
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

        public void UpdateHighlightColors()
        {
            //set all colors to normal
            SetColorsDefault();

            if (!(bool)Properties.Settings.Default["ColorHelpEnabled"])
            {
                return;
            }

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
                            this[j, i].Style.BackColor = Color.LightBlue;
                        }
                    }
                }
            }
        }

        public Coords CurrentCellCoords()
        {
            int x, y;

            y = CurrentCell.ColumnIndex;
            x = CurrentCell.RowIndex;

            return new Coords(x, y);
        }

        public void GridComplete(bool correct)
        {
            MessageBox.Show(correct ? "Congratulations! You found a valid solution." : "Sadly, this solution is not correct.");
        }

        public void ResetGrid()
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

        public void ClearGridAll()
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

        public void OpenImageSaveDialog()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            saveFileDialog.Filter = "PNG files (*.png)|*.png|All files (*.*)|*.*";
            saveFileDialog.FilterIndex = 0;
            saveFileDialog.RestoreDirectory = false;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    ExportImage(saveFileDialog.FileName);
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error: " + e.Message, "An error has occured");
                }
            }
        }

        public void OpenSaveDialog(Grid grid)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.InitialDirectory = Application.StartupPath + "\\Saved sudokus";
            saveFileDialog.Filter = "sudoku files (*.sudoku)|*.sudoku|All files (*.*)|*.*";
            saveFileDialog.FilterIndex = 0;
            saveFileDialog.RestoreDirectory = false;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    SaveSudokuFile(grid, saveFileDialog.FileName);
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error: " + e.Message, "An error has occured");
                }
            }
        }

        public void Save(Grid grid)
        {
            OpenSaveDialog(grid);
        }

        public void Save()
        {
            OpenSaveDialog(internal_grid);
        }

        public void OpenLoadDialog()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = Application.StartupPath + "\\Saved sudokus";
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

        public void SaveSudokuFile(Grid grid, string path)
        {
            File.WriteAllText(path, JsonConvert.SerializeObject(internal_grid.GetGrid()));
        }

        public void LoadSudokuFile(string path)
        {
            Grid deserialized_grid = new Grid(JsonConvert.DeserializeObject<int[][]>(File.ReadAllText(path)), 9);
            internal_grid = deserialized_grid;
            UpdateGrid();
            UpdateHighlightColors();
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
            if (secondSolFound)
            {
                return;
            }

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
