using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Sudoku;

namespace SudokuUI
{
    public partial class SudokuViewer : Form
    {
        public static Font font_cell_default = new Font("Verdana", 9f);
        public static Font font_cell_bold = new Font("Verdana", 9f, FontStyle.Bold);

        public Grid internal_grid = new Grid(9);

        public SudokuViewer()
        {
            InitializeComponent();

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
        }

        public void SetCell(Coords coords, int value)
        {
            internal_grid.Set(coords, value);
            UpdateGrid();
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
                            ui_grid[j, i].Value = -internal_grid.Get(i, j);
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

        private void MarkPremade(Coords coords, bool isPremade)
        {
            ui_grid[coords.y, coords.x].Style.Font = isPremade ? font_cell_bold : font_cell_default; //set font weight
        }

        public void GenerateSolution()
        {

        }

        public void GeneratePuzzle()
        {

        }

        private void SudokuViewer_Load(object sender, EventArgs e)
        {

        }

        private void buttonGenSolution_Click(object sender, EventArgs e)
        {
            if (!bgW_GenSolution.IsBusy)
            {
                bgW_GenSolution.RunWorkerAsync(true);
            }
        }

        private void buttonGenPuzzle_Click(object sender, EventArgs e)
        {
            if (!bgW_GenSolution.IsBusy)
            {
                bgW_GenSolution.RunWorkerAsync(false);
            }
        }

        private void bgW_GenSolution_DoWork(object sender, DoWorkEventArgs e)
        {
            bool solution = (bool)e.Argument;
            if (solution)
            {
                internal_grid = new Grid(9);
                UpdateGrid();
                GenerateSolutionRecursive();
            }
            else
            {
                MessageBox.Show("generating a solution isn't implemented yet."); // TODO
            }
        }

        public bool GenerateSolutionRecursive()
        {
            if (!internal_grid.ContainsZeros()) // if it is fully solved
            {
                return true;
            }
            else
            {
                // find the first empty cell
                Coords emptyCell = internal_grid.GetFirstEmptyCell();

                // get all possibilities for (x, y)
                List<int> possibilities = Lib.Shuffle(internal_grid.GetAllPossibilities(emptyCell));
                foreach (int item in possibilities)
                {
                    SetCell(emptyCell, item);
                    
                    if (GenerateSolutionRecursive()) // if the next method call reports that the grid is fully solved, do the same
                    {
                        return true;
                    }
                }
                //backtrack
                SetCell(emptyCell, 0);
                return false;
            }
        }

        public void GeneratePuzzleRecursive()
        {
            // TODO
        }
    }
}
