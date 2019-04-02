using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Sudoku;

namespace SudokuUI
{
    public partial class SudokuGenerator : Form
    {
        Difficulty selectedDifficulty;
        public Grid temp_grid;
        public List<Grid> possible_solutions;
        bool filled;

        public SudokuGenerator()
        {
            InitializeComponent();


            // set up background worker
            bgW_GenSolution.WorkerReportsProgress = true;
            bgW_GenSolution.ProgressChanged += ProgressUpdate;

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
            ui_grid.Font = UIgrid.font_cell_default;

            // apply background pattern
            ui_grid.SetColorsDefault();
        }

        private void buttonGenSolution_Click(object sender, EventArgs e)
        {
            if (!bgW_GenSolution.IsBusy)
            {
                bgW_GenSolution.RunWorkerAsync(true);
            }
            listBox1.Enabled = true;
            if (listBox1.SelectedIndex != -1)
            {
                buttonGenPuzzle.Enabled = true;
            }
        }

        private void buttonGenPuzzle_Click(object sender, EventArgs e)
        {
            if (!bgW_GenSolution.IsBusy)
            {
                bgW_GenSolution.RunWorkerAsync(false);
            }
            buttonGenPuzzle.Enabled = false;
            listBox1.Enabled = false;
            buttonGenSolution.Enabled = false;
            buttonSave.Enabled = false;
        }

        private void bgW_GenSolution_DoWork(object sender, DoWorkEventArgs e)
        {
            bool solution = (bool)e.Argument;
            if (solution)
            {
                ui_grid.internal_grid = new Grid(9);
                ui_grid.UpdateGrid();
                GenerateSolutionRecursive();
                filled = true;
            }
            else
            {
                GeneratePuzzle(selectedDifficulty);
            }
        }

        public bool GenerateSolutionRecursive()
        {
            if (!ui_grid.internal_grid.ContainsZeros()) // if it is fully solved
            {
                return true;
            }
            else
            {
                // find the first empty cell
                Coords emptyCell = ui_grid.internal_grid.GetFirstEmptyCell();

                // get all possibilities for (x, y)
                List<int> possibilities = Lib.Shuffle(ui_grid.internal_grid.GetAllPossibilities(emptyCell));
                foreach (int item in possibilities)
                {
                    ui_grid.SetCell(emptyCell, -item);
                    
                    if (GenerateSolutionRecursive()) // if the next method call reports that the grid is fully solved, do the same
                    {
                        return true;
                    }
                }
                //backtrack
                ui_grid.SetCell(emptyCell, 0);
                return false;
            }
        }

        public void GeneratePuzzle(Difficulty difficulty)
        {
            if (!filled)
            {
                MessageBox.Show("generate a solution first.");
                return;
            }
            temp_grid = ui_grid.internal_grid.Clone();
            List<Coords> cells = new List<Coords>();
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    cells.Add(new Coords(i, j));
                }
            }
            cells = Lib.Shuffle(cells);
            while (cells.Count > 0)
            {
                UniquenessChecker uc = new UniquenessChecker();
                int removedCellvalue = temp_grid.Get(cells[0]);
                Coords removedCell = cells[0];
                temp_grid.Set(cells[0], 0);
                cells.RemoveAt(0);
                if (!uc.Check(temp_grid))
                {
                    temp_grid.Set(removedCell, removedCellvalue);
                }
                bgW_GenSolution.ReportProgress((81 - cells.Count) * 100 / 81);
            }

            List<Coords> emptyCells = new List<Coords>();
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (temp_grid.Get(i, j) == 0)
                    {
                        emptyCells.Add(new Coords(i, j));
                    }
                }
            }
            emptyCells = Lib.Shuffle(emptyCells);
            // add a number of "hint numbers" to the grid, how many depends on the chosen difficulty
            int numbers_added = 0;
            while (numbers_added < (int)difficulty)
            {
                temp_grid.Set(emptyCells[0], ui_grid.internal_grid.Get(emptyCells[0])); // fill in a number from the solution
                numbers_added++;
                emptyCells.RemoveAt(0);
            }

            ui_grid.internal_grid = temp_grid.Clone();
            ui_grid.UpdateGrid();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonGenPuzzle.Enabled = true;
            switch (listBox1.SelectedIndex)
            {
                case 0:
                    selectedDifficulty = Difficulty.very_easy;
                    break;
                case 1:
                    selectedDifficulty = Difficulty.easy;
                    break;
                case 2:
                    selectedDifficulty = Difficulty.medium;
                    break;
                case 3:
                    selectedDifficulty = Difficulty.hard;
                    break;
                case 4:
                    selectedDifficulty = Difficulty.extreme;
                    break;
                default:
                    throw new Exception();
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            ui_grid.Save();
        }

        public Grid MakePremade(Grid input)
        {
            Grid output = input.Clone();
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    Coords coords = new Coords(i, j);
                    output.Set(coords, -Math.Abs(output.Get(coords)));
                }
            }
            return output;
        }

        void ProgressUpdate(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        private void bgW_GenSolution_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progressBar1.Value = 0;
            buttonSave.Enabled = true;
            buttonGenSolution.Enabled = true;
            buttonSave.Enabled = true;
        }
    }
}
