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
        Form1 parentform;
        public save_load_dialog(Form1 parent)
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
            string filePath;
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
                        Grid deserialized_grid = new Grid(JsonConvert.DeserializeObject<int[][]>(File.ReadAllText(openFileDialog.FileName)), 9);
                        parentform.internal_grid.SetGrid(deserialized_grid);
                        parentform.UpdateGrid();
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
                    File.WriteAllText(saveFileDialog.FileName, JsonConvert.SerializeObject(parentform.internal_grid.GetGrid()));
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
