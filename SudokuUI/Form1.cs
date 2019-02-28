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
        public Grid viewGrid = new Grid(9);

        public Form1()
        {
            InitializeComponent();

            style_premade.Font = font_cell_bold;

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

            buttonGenerate.Click += new EventHandler(GenerateEvent);
            buttonSolve.Click += new EventHandler(SolveEvent);

            ui_grid.SelectionChanged += new EventHandler(SelectionUpdate);
            ui_grid.KeyPress += new KeyPressEventHandler(KeyPressEvent);
            ui_grid.PreviewKeyDown += new PreviewKeyDownEventHandler(KeyPressPreview);

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
                        ui_grid.Rows[i].Cells[j].Style.BackColor = Color.DarkGray;
                    }
                }
            }

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    ui_grid.Rows[i].Cells[j].Tag = new Tag(false);
                }
            }
        }

        private void UpdateGrid()
        {
            SetPremadeGrid(viewGrid);
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
            ui_grid.Rows[coords.y].Cells[coords.x].Value = value;
        }

        public DataGridViewCell GetCell(Coords coords)
        {
            return ui_grid.Rows[coords.x].Cells[coords.y];
        }

        private void ButtonClick(object sender, EventArgs e)
        {
            Tag tag = (Tag)(GetCell(currentCell).Tag);

            if (sender == buttonRemove)
            {
                if (!tag.premade)
                {
                    ui_grid.CurrentCell.Value = null;
                    return;
                }
            }
            string btnText = ((Button)sender).Text;
            if (!tag.premade)
            {
                ui_grid.CurrentCell.Value = int.Parse(btnText);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            currentCell.x = e.ColumnIndex;
            currentCell.y = e.RowIndex;
        }

        private void GenerateEvent(object sender, EventArgs e)
        {
            //Grid puzzle = Sudoku.Sudoku.GeneratePuzzle(Sudoku.Sudoku.GenerateRandomSolution(), Sudoku.Sudoku.Difficulty.medium);
            Grid puzzle = Sudoku.Sudoku.exampleGrid3;
            SetPremadeGrid(puzzle);
        }

        private void SolveEvent(object sender, EventArgs e)
        {
            Grid puzzle = Sudoku.Sudoku.exampleGrid4_hard;
            SetPremadeGrid(puzzle);
        }

        void SetPremadeGrid(Grid grid)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (grid.Get(new Coords(j + 1, i + 1)) != 0)
                    {
                        SetCell(new Coords(i, j), grid.Get(new Coords(j + 1, i + 1)));
                        GetCell(new Coords(j, i)).Style.Font = font_cell_bold;
                        GetCell(new Coords(i, j)).Tag = new Tag(true);
                    }
                    else
                    {
                        GetCell(new Coords(j, i)).Value = null;
                        GetCell(new Coords(j, i)).Style.Font = font_cell_default;
                        GetCell(new Coords(i, j)).Tag = new Tag(false);
                    }
                }
            }
        }


        private void KeyPressEvent(object sender, KeyPressEventArgs e)
        {
            char[] allowed = new char[] { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', (char)Keys.Back};
            if (allowed.Contains(e.KeyChar))
            {
                MessageBox.Show("" + e.KeyChar);
            }
            if (e.KeyChar == (char)Keys.Enter)
            {
                MessageBox.Show("enter");
                e.Handled = true;
            }
        }

        private void KeyPressPreview(object sender, PreviewKeyDownEventArgs e)
        {
            char[] allowed = new char[] { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', };
            if (allowed.Contains((char)e.KeyCode))
            {
                MessageBox.Show("" + (char)e.KeyCode);
            }
            if (((char)e.KeyCode) == (char)Keys.Enter)
            {
                MessageBox.Show("enter2");
            }
        }
    }

    public class UIgrid : DataGridView
    {
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                e.Handled = true;
            }
            base.OnKeyDown(e);
        }
    }
}
