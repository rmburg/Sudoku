using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sudoku;

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

    public partial class Form1 : Form
    {
        Coords currentCell = new Coords(0, 0);
        static Font font_cell_default = new Font("Verdana", 9f);
        static Font font_cell_bold = new Font("Verdana", 9f, FontStyle.Bold);
        public static DataGridViewCellStyle style_premade = new DataGridViewCellStyle();
        public Grid internal_grid = new Grid(9);

        public Form1()
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
            buttonSaveLoad.Click += new EventHandler(FileDiagEvent);

            buttonGenerate.Click += new EventHandler(GenerateEvent);
            buttonSolve.Click += new EventHandler(SolveEvent);
            buttonReset.Click += new EventHandler(ResetEvent);

            ui_grid.SelectionChanged += new EventHandler(SelectionUpdate);
            ui_grid.KeyPress += new KeyPressEventHandler(KeyPressEvent);

            backgroundWorker1.DoWork += BackgroundWorker1_DoWork; //TODO

            //apply color to cells
            ui_grid.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            ui_grid.GridColor = Color.Black;
            ui_grid.Font = font_cell_default;
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

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    ui_grid[i, j].Tag = new Tag(false);
                }
            }
        }

        private void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void UpdateGrid()
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

            Grid puzzle = Sudoku.Sudoku.exampleGrid3;
            SetPremadeGrid(puzzle);
        }

        private void SolveEvent(object sender, EventArgs e)
        {
            Grid puzzle = Sudoku.Sudoku.exampleGrid4_hard;
            SetPremadeGrid(puzzle);
        }

        private void ResetEvent(object sender, EventArgs e)
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

        private void FileDiagEvent(object sender, EventArgs e)
        {
            save_load_dialog diag = new save_load_dialog(this);
            diag.ShowDialog(this);
        }

        private void MarkPremade(Coords coords, bool isPremade)
        {
            ui_grid[coords.x, coords.y].Tag = new Tag(isPremade); // mark as premade
            ui_grid[coords.x, coords.y].Style.Font = isPremade?font_cell_bold:font_cell_default; //set font weight
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
