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
    public partial class Form1 : Form
    {
        Coords currentCell = new Coords(0, 0);
        Font font_cell_default = new Font("Verdana", 9f);
        Font font_cell_bold = new Font("Verdana", 9f, FontStyle.Bold);
        public static DataGridViewCellStyle style_premade = new DataGridViewCellStyle();
        

        public Form1()
        {
            InitializeComponent();

            style_premade.Font = font_cell_bold;

            //add table rows
            dataGridView1.Rows.Add();
            dataGridView1.Rows.Add();
            dataGridView1.Rows.Add();
            dataGridView1.Rows.Add();
            dataGridView1.Rows.Add();
            dataGridView1.Rows.Add();
            dataGridView1.Rows.Add();
            dataGridView1.Rows.Add();
            dataGridView1.Rows.Add();
            dataGridView1.RowTemplate.Height = 30;
            
            textBox1.Text = "test";
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

            buttonGenerate.Click += new EventHandler(GenerateEvent);
            buttonSolve.Click += new EventHandler(SolveEvent);

            dataGridView1.SelectionChanged += new EventHandler(SelectionUpdate);

            //apply color to cells
            dataGridView1.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.GridColor = Color.Black;
            dataGridView1.Font = font_cell_default;
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if ((i < 3 || i > 5) ^ (j < 3 || j > 5)) // 3x3 checkerboard pattern
                    {
                        dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.DarkGray;
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

            textBox1.Text = String.Format("{0}, {1}",pos.x, pos.y);
            currentCell = pos;
        }

        public void SetCell(Coords coords, int value)
        {
            dataGridView1.Rows[coords.y].Cells[coords.x].Value = value;
        }

        public DataGridViewCell GetCell(Coords coords)
        {
            return (DataGridViewCell)dataGridView1.Rows[coords.x].Cells[coords.y];
        }

        private void ButtonClick(object sender, EventArgs e)
        {
            string btnText = ((Button)sender).Text;
            textBox1.Text = btnText;
            SetCell(currentCell, int.Parse(btnText));
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = String.Format("{0}, {1}", e.ColumnIndex, e.RowIndex);
            currentCell.x = e.ColumnIndex;
            currentCell.y = e.RowIndex;
        }

        private void GenerateEvent(object sender, EventArgs e)
        {
            Grid puzzle = Sudoku.Sudoku.GenerateRandomSolution();
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (puzzle.Get(new Coords(i+1, j+1)) != 0)
                    {
                        SetCell(new Coords(i, j), puzzle.Get(new Coords(i + 1, j + 1)));
                        GetCell(new Coords(i, j)).Style = style_premade;
                    }
                }
            }
        }

        private void SolveEvent(object sender, EventArgs e)
        {

        }
    }
}
