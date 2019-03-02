using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Sudoku;
using System.IO;
using Newtonsoft.Json;

namespace SudokuUI
{
    public struct Tag
    {
        public bool premade;
        public Tag(bool premade)
        {
            this.premade = premade;
        }
    }

    public partial class MainWindowForm : Form
    {
        Coords currentCell = new Coords(0, 0);
        static Font font_cell_default = new Font("Verdana", 9f);
        static Font font_cell_bold = new Font("Verdana", 9f, FontStyle.Bold);
        public static DataGridViewCellStyle style_premade = new DataGridViewCellStyle();
        public Grid internal_grid = new Grid(9);

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
            button1.Click += new EventHandler(ButtonClick);
            button2.Click += new EventHandler(ButtonClick);
            button3.Click += new EventHandler(ButtonClick);
            button4.Click += new EventHandler(ButtonClick);
            button5.Click += new EventHandler(ButtonClick);
            button6.Click += new EventHandler(ButtonClick);
            button7.Click += new EventHandler(ButtonClick);
            button8.Click += new EventHandler(ButtonClick);
            button9.Click += new EventHandler(ButtonClick);
            buttonRemove.Click += new EventHandler(ButtonClick);

            ui_grid.SelectionChanged += new EventHandler(SelectionUpdate);
            ui_grid.KeyPress += new KeyPressEventHandler(KeyPressEvent);
            


            //apply style to cells
            ui_grid.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            ui_grid.GridColor = Color.Black;
            ui_grid.Font = font_cell_default;
            // apply background pattern
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if ((i < 3 || i > 5) ^ (j < 3 || j > 5)) // 3x3 checkerboard pattern
                    {
                        ui_grid[i, j].Style.BackColor = Color.DarkGray;
                    }
                }
            }

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
                        ui_grid[i, j].Value = null;
                        MarkPremade(new Coords(i, j), false);
                    }
                    else
                    {
                        if (internal_grid.Get(i, j) < 0) // if it is a "premade" cell
                        {
                            ui_grid[i, j].Value = - internal_grid.Get(i, j);
                            MarkPremade(new Coords(i, j), true);
                        }
                        else
                        {
                            ui_grid[i, j].Value = internal_grid.Get(i, j);
                            MarkPremade(new Coords(i, j), false);
                        }
                    }
                }
            }
        }

        private void SolveGrid()
        {
            MessageBox.Show("solving isn't implemented yet.");
        }

        private void SelectionUpdate(object sender, EventArgs e)
        {
            DataGridView datagrid = (DataGridView)sender;
            Coords pos = new Coords();
            pos.x = datagrid.CurrentCell.ColumnIndex;
            pos.y = datagrid.CurrentCell.RowIndex;

            currentCell = pos;
        }

        public void SetCell(Coords coords, int value)
        {
            internal_grid.Set(coords, value);
            UpdateGrid();
        }

        public DataGridViewCell GetCell(Coords coords)
        {
            return ui_grid[coords.x, coords.y];
        }

        private void ButtonClick(object sender, EventArgs e)
        {
            if (sender == buttonRemove)
            {
                if (internal_grid.Get(CurrentCellCoords()) >= 0) // if it is not a premade cell
                {
                    ui_grid.CurrentCell.Value = null;
                    return;
                }
                return;
            }
            string btnText = ((Button)sender).Text;
            if (internal_grid.Get(CurrentCellCoords()) >= 0)
            {
                SetCell(CurrentCellCoords(), int.Parse(btnText));
            }
        }

        private Coords CurrentCellCoords()
        {
            int x, y;

            x = ui_grid.CurrentCell.ColumnIndex;
            y = ui_grid.CurrentCell.RowIndex;

            return new Coords(x, y);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            currentCell.x = e.ColumnIndex;
            currentCell.y = e.RowIndex;
        }

        private void GenerateEvent(object sender, EventArgs e)
        {
            MessageBox.Show("not implemented yet.");
        }

        private void SolveEvent(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure?", "Please confirm", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                SolveGrid();
            }
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
            UpdateGrid();
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
            UpdateGrid();
        }

        private void ResetEvent(object sender, EventArgs e)
        {
            ResetGrid();
        }

        void SetPremadeGrid(Grid grid)
        {
            internal_grid = new Grid(9);
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (grid.Get(new Coords(j, i)) != 0)
                    {
                        SetCell(new Coords(i, j), -grid.Get(new Coords(j, i)));
                        MarkPremade(new Coords(i, j), true);
                    }
                    else
                    {
                        SetCell(new Coords(i, j), 0);
                        GetCell(new Coords(i, j)).Value = null;
                        MarkPremade(new Coords(i, j), false);
                    }
                }
            }
            UpdateGrid();
        }

        private void KeyPressEvent(object sender, KeyPressEventArgs e)
        {
            char[] allowed = new char[] { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', (char)Keys.Back};
            if (internal_grid.Get(CurrentCellCoords()) >= 0)
            {
                if (e.KeyChar == (char)Keys.Back || e.KeyChar == '0')
                {
                    SetCell(CurrentCellCoords(), 0);
                    return;
                }
                if (allowed.Contains(e.KeyChar))
                {
                    SetCell(CurrentCellCoords(), int.Parse("" + e.KeyChar));
                }
            }
        }

        private void MarkPremade(Coords coords, bool isPremade)
        {
            ui_grid[coords.x, coords.y].Tag = new Tag(isPremade); // mark as premade
            ui_grid[coords.x, coords.y].Style.Font = isPremade?font_cell_bold:font_cell_default; //set font weight
        }

        public void LoadSudokuFile(string path)
        {
            Grid deserialized_grid = new Grid(JsonConvert.DeserializeObject<int[][]>(File.ReadAllText(path)), 9);
            internal_grid.SetGrid(deserialized_grid);
            UpdateGrid();
        }

        public void SaveSudokuFile(string path)
        {
            File.WriteAllText(path, JsonConvert.SerializeObject(internal_grid.GetGrid()));
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
                    SaveSudokuFile(saveFileDialog.FileName);
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error: " + e.Message, "An error has occured");
                }
            }
        }

        private void leavePremadeNumbersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure? This will delete all the numbers you entered", "Please confirm", MessageBoxButtons.YesNo);
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

        private void solveGridToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure? This will solve the grid. If you entered any wrong numbers, no solution may be possible.", "Please confirm", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                SolveGrid();
            }
        }
    }

    public class UIgrid : DataGridView
    {
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter || e.KeyData == Keys.Tab)
            {
                e.Handled = true;
            }
            base.OnKeyDown(e);
        }
    }
}
