using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;
using Sudoku;

namespace SudokuUI
{
    public partial class save_load_dialog : Form
    {
        MainWindowForm parentform;
        public save_load_dialog(MainWindowForm parent)
        {
            InitializeComponent();

            StartPosition = FormStartPosition.CenterParent;

            buttonLoad.Click += new EventHandler(LoadEvent);
            buttonSave.Click += new EventHandler(SaveEvent);
            parentform = parent;
        }

        private void save_load_dialog_Load(object sender, EventArgs e)
        {

        }

        private void LoadEvent(object sender, EventArgs e)
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
                        parentform.LoadSudokuFile(openFileDialog.FileName);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Error: " + e.ToString(), "An error has occured");
                    }
                }
            }
            Close();
        }

        private void SaveEvent(object sender, EventArgs e)
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
                    parentform.SaveSudokuFile(saveFileDialog.FileName);
                }
                catch (Exception)
                {
                    MessageBox.Show("Error: " + e.ToString(), "An error has occured");
                }
            }
            Close();
        }
    }
}
