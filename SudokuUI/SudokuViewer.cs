using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Sudoku;

namespace SudokuUI
{
    public partial class SudokuViewer : Form
    {
        int currentIndex;
        List<Grid> gridList;

        public SudokuViewer(List<Grid> grids)
        {
            InitializeComponent();

            if (grids.Count == 0)
            {
                throw new Exception("no grids entered");
            }

            this.FindForm().Text = $"Viewer [Solutions: {grids.Count}]";

            currentIndex = 0;
            gridList = grids;
            
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

            ui_grid.SetColorsDefault();
            
            View(gridList[0]);
        }

        private void View(Grid grid)
        {
            ui_grid.internal_grid = grid;
            ui_grid.UpdateGrid();
            UpdateButtons();
        }

        private void button_Prev_Click(object sender, EventArgs e)
        {
            if (currentIndex > 0)
            {
                currentIndex--;
                View(gridList[currentIndex]);
            }
        }

        private void button_Next_Click(object sender, EventArgs e)
        {
            if (currentIndex < gridList.Count - 1)
            {
                currentIndex++;
                View(gridList[currentIndex]);
            }
        }

        private void UpdateButtons()
        {
            if (currentIndex == 0)
            {
                button_Prev.Enabled = false;
            }
            else
            {
                button_Prev.Enabled = true;
            }
            if (currentIndex == gridList.Count - 1)
            {
                button_Next.Enabled = false;
            }
            else
            {
                button_Next.Enabled = true;
            }
        }
    }
}
