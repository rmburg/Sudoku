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
    public partial class SudokuGenerator : Form
    {
        public SudokuGenerator(MainWindowForm mainWindowForm)
        {
            InitializeComponent();

            buttonGeneratePuzzle.Click += new EventHandler(GeneratePuzzleEvent);
            buttonGenerateSolution.Click += new EventHandler(GenerateSolutionEvent);
        }

        private void SudokuGenerator_Load(object sender, EventArgs e)
        {

        }

        private void GeneratePuzzleEvent(object sender, EventArgs e)
        {
            // ask for solution file path
            // generate puzzle
            // ask: use now / save for later
            // if user wants to save, ask for path
            // save puzzle at file path
            //if user wants to use the puzzle now, apply grid to main window
        }

        private void GenerateSolutionEvent(object sender, EventArgs e)
        {
            // ask for file path
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.InitialDirectory = Application.StartupPath;
            saveFileDialog.Filter = "sudoku files (*.sudoku)|*.sudoku|All files (*.*)|*.*";
            saveFileDialog.FilterIndex = 0;
            saveFileDialog.RestoreDirectory = false;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                // generate solution
                // save solution in file
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

        }
    }
}
