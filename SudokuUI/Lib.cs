using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.IO;


namespace SudokuUI
{
    static class Lib
    {
        public static List<E> Shuffle<E>(List<E> inputList)
        {
            List<E> randomList = new List<E>();

            Random r = new Random();
            int randomIndex = 0;
            while (inputList.Count > 0)
            {
                randomIndex = r.Next(0, inputList.Count); //Choose a random object in the list
                randomList.Add(inputList[randomIndex]); //add it to the new, random list
                inputList.RemoveAt(randomIndex); //remove to avoid duplicates
            }

            return randomList; //return the new random list
        }
    }

    public enum Difficulty
    {
        extreme = 0,
        hard = 5,
        medium = 10,
        easy = 15,
        very_easy = 20
    }

    public class Grid
    {
        private int[][] internal_grid;
        public int size;

        public Grid(int[][] inGrid, int inSize)
        {
            internal_grid = inGrid;
            size = inSize;
        }

        public Grid(int size)
        {
            List<int[]> subgrids = new List<int[]>();
            List<int> zeroFillList = new List<int>();
            for (int i = 0; i < size; i++)
            {
                zeroFillList.Add(0);
            }

            int[] zeroFillArr = zeroFillList.ToArray();

            for (int i = 0; i < size; i++)
            {
                subgrids.Add((int[])zeroFillArr.Clone());
            }
            internal_grid = subgrids.ToArray();
            this.size = size;
        }

        public Grid Clone()
        {
            List<int[]> out_grid = new List<int[]>();
            foreach (int[] subgrid in internal_grid)
            {
                out_grid.Add((int[])subgrid.Clone());
            }

            return new Grid(out_grid.ToArray(), size);
        }

        public int Get(int x, int y)
        {
            return internal_grid[x][y];
        }

        public int Get(Point point)
        {
            return internal_grid[point.X][point.Y];
        }

        public void Set(int x, int y, int value)
        {
            internal_grid[x][y] = value;
        }

        public void Set(Point Point, int value)
        {
            internal_grid[Point.X][Point.Y] = value;
        }

        public int[][] GetGrid()
        {
            return internal_grid;
        }

        public List<int> GetAllNumbers()
        {
            List<int> list = new List<int>();
            foreach (int[] subarray in internal_grid)
            {
                foreach (int number in subarray)
                {
                    list.Add(number);
                }
            }
            return list;
        }

        public List<int> GetLinePossibilities(Point Point)
        {
            List<int> possibilities = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            for (int i = 0; i < 9; i++)
            {
                if (i != Point.X)
                {
                    possibilities.Remove(Math.Abs(Get(i, Point.Y)));
                }
            }
            return possibilities;
        }

        public List<int> GetRowPossibilities(Point Point)
        {
            List<int> possibilities = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            for (int i = 0; i < 9; i++)
            {
                if (i != Point.Y)
                {
                    possibilities.Remove(Math.Abs(Get(Point.X, i)));
                }
            }
            return possibilities;
        }

        public List<int> GetSquarePossibilities(Point Point)
        {
            List<int> possibilities = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            Grid subgrid = new Grid(3);
            for (int i = 0; i < 3; i++) // make a new 3x3 subgrid that represents the square the cell is in
            {
                for (int j = 0; j < 3; j++)
                {
                    Point checked_cell = new Point(((Point.X) / 3) * 3 + i, ((Point.Y) / 3) * 3 + j);
                    if (checked_cell != Point) // if the input cell is being checked, don't remove its number (because it shouldn't make itself impossible)
                    {
                        subgrid.Set(i, j, Get(checked_cell));
                    }
                    else
                    {
                        subgrid.Set(i, j, 0);
                    }
                }
            }
            foreach (int number in subgrid.GetAllNumbers())
            {
                possibilities.Remove(Math.Abs(number));
            }
            return possibilities;
        }

        public List<int> GetAllPossibilities(Point Point)
        {
            List<int> lineP = GetLinePossibilities(Point);
            List<int> rowP = GetRowPossibilities(Point);
            List<int> sqP = GetSquarePossibilities(Point);

            List<int> intersect = lineP.Intersect(rowP).Intersect(sqP).ToList(); // all numbers that are valid in the given row, column and 3x3 square

            return intersect;
        }

        public bool ContainsZeros()
        {
            foreach (int[] subgrid in internal_grid)
            {
                if (subgrid.Contains(0))
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsValid()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    Point Point = new Point(i, j);
                    if (!(GetAllPossibilities(Point).Contains(Math.Abs(Get(Point))) || Get(Point) == 0)) // check if each number is valid or zero. If not, return false
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public Point GetFirstEmptyCell()
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (Get(i, j) == 0)
                    {
                        return new Point(i, j);
                    }
                }
            }
            return new Point(-1, -1);
        }
    }

    public class UIgrid : DataGridView
    {
        public Grid internal_grid;

        public static readonly Font font_cell_default = new Font("Verdana", 9f);
        public static readonly Font font_cell_bold = new Font("Verdana", 9f, FontStyle.Bold);

        public UIgrid()
        {
            internal_grid = new Grid(9);

            RowTemplate.Height = 30;

            //apply style to cells
            DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            GridColor = Color.Black;
            Font = font_cell_default;
        }

        protected override void OnKeyDown(KeyEventArgs e) // just to avoid the standard behavior of keys like enter, tab and back
        {
            if (e.KeyData == Keys.Enter || e.KeyData == Keys.Tab || e.KeyData == Keys.Back)
            {
                e.Handled = true;
            }
            base.OnKeyDown(e);
        }

        public void UpdateGrid() // put the values of the internal grid into the UI
        {
            try
            {
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        if (internal_grid.Get(i, j) == 0)
                        {
                            this[j, i].Value = null;
                            MarkPremade(new Point(i, j), false);
                        }
                        else
                        {
                            if (internal_grid.Get(i, j) < 0) // if it is a "premade" cell
                            {
                                this[j, i].Value = -internal_grid.Get(i, j);
                                MarkPremade(new Point(i, j), true);
                            }
                            else
                            {
                                this[j, i].Value = internal_grid.Get(i, j);
                                MarkPremade(new Point(i, j), false);
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show(ColumnCount + ", " + RowCount);
            }
        }

        public void ExportImage(string path)
        {
            Bitmap bitmap = new Bitmap(Width, Height);
            Rectangle rect = new Rectangle(0, 0, Width, Height);
            CurrentCell = null;
            SetColorsDefault();
            DrawToBitmap(bitmap, rect);
            bitmap.Save(path);
        }

        private void MarkPremade(Point Point, bool isPremade)
        {
            this[Point.Y, Point.X].Style.Font = isPremade ? font_cell_bold : font_cell_default; //set font weight
        }

        public void SetColorsDefault()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if ((i < 3 || i > 5) ^ (j < 3 || j > 5)) // 3x3 checkerboard pattern
                    {
                        this[i, j].Style.BackColor = Color.DarkGray;
                    }
                    else
                    {
                        this[i, j].Style.BackColor = Color.White;
                    }
                }
            }
        }

        public void SetCell(Point Point, int value)
        {
            internal_grid.Set(Point, value);
            UpdateGrid(); ;
        }

        //for doing additional stuff when the user sets a cell (so not while loading, solving etc...)
        public void UserSetCell(Point Point, int value)
        {
            SetCell(Point, value);
            if (!internal_grid.ContainsZeros())
            {
                bool correct = internal_grid.IsValid();
                GridComplete(correct);
            }
            UpdateHighlightColors();
        }

        public void UpdateHighlightColors()
        {
            //set all colors to normal
            SetColorsDefault();

            if (!(bool)Properties.Settings.Default["ColorHelpEnabled"])
            {
                return;
            }

            //set all numbers that are the same as the selected one to e.g. blue
            int selectedNum = Math.Abs(internal_grid.Get(CurrentCellPoint()));
            if (selectedNum != 0)
            {
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        if (Math.Abs(internal_grid.Get(new Point(i, j))) == selectedNum)
                        {
                            this[j, i].Style.BackColor = Color.LightBlue;
                        }
                    }
                }
            }
        }

        public Point CurrentCellPoint()
        {
            int x, y;

            y = CurrentCell.ColumnIndex;
            x = CurrentCell.RowIndex;

            return new Point(x, y);
        }

        public void GridComplete(bool correct)
        {
            MessageBox.Show(correct ? "Congratulations! You found a valid solution." : "Sadly, this solution is not correct.");
        }

        public void ResetGrid()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (internal_grid.Get(i, j) > 0)
                    {
                        SetCell(new Point(i, j), 0);
                    }
                }
            }
            UpdateHighlightColors();
        }

        public void ClearGridAll()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    SetCell(new Point(i, j), 0);
                }
            }
            UpdateHighlightColors();
        }

        public void OpenImageSaveDialog()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            saveFileDialog.Filter = "PNG files (*.png)|*.png|All files (*.*)|*.*";
            saveFileDialog.FilterIndex = 0;
            saveFileDialog.RestoreDirectory = false;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    ExportImage(saveFileDialog.FileName);
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error: " + e.Message, "An error has occured");
                }
            }
        }

        public void OpenSaveDialog(Grid grid)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.InitialDirectory = Application.StartupPath + "\\Saved sudokus";
            saveFileDialog.Filter = "sudoku files (*.sudoku)|*.sudoku|All files (*.*)|*.*";
            saveFileDialog.FilterIndex = 0;
            saveFileDialog.RestoreDirectory = false;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    SaveSudokuFile(grid, saveFileDialog.FileName);
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error: " + e.Message, "An error has occured");
                }
            }
        }

        public void Save(Grid grid)
        {
            OpenSaveDialog(grid);
        }

        public void Save()
        {
            OpenSaveDialog(internal_grid);
        }

        public void OpenLoadDialog()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = Application.StartupPath + "\\Saved sudokus";
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

        public void SaveSudokuFile(Grid grid, string path)
        {
            File.WriteAllText(path, JsonConvert.SerializeObject(internal_grid.GetGrid()));
        }

        public void LoadSudokuFile(string path)
        {
            Grid deserialized_grid = new Grid(JsonConvert.DeserializeObject<int[][]>(File.ReadAllText(path)), 9);
            SetGrid(deserialized_grid);
        }

        public void SetGrid(Grid grid)
        {
            internal_grid = grid;
            UpdateGrid();
            UpdateHighlightColors();
        }
    }

    public class UniquenessChecker
    {
        bool solutionFound = false;
        bool secondSolFound = false;
        Grid grid_to_check;
        public bool Check(Grid grid)
        {
            grid_to_check = grid.Clone();
            CheckUniqueRecursive();
            return (solutionFound && !secondSolFound); // returns true only if there is exactly one solution to the given grid
        }

        private void CheckUniqueRecursive()
        {
            if (secondSolFound)
            {
                return;
            }

            if (!grid_to_check.ContainsZeros()) // if it is fully solved
            {
                if (solutionFound)
                {
                    secondSolFound = true;
                    return;
                }
                solutionFound = true;
                return;
            }
            else
            {
                // find the first empty cell
                Point emptyCell = grid_to_check.GetFirstEmptyCell();

                // get all possibilities for the first empty cell
                List<int> possibilities = grid_to_check.GetAllPossibilities(emptyCell);

                foreach (int item in possibilities) // try each possible number
                {
                    grid_to_check.Set(emptyCell, item);

                    CheckUniqueRecursive(); // if the next fuction call returns true (has found a solution)
                }
                //backtrack
                grid_to_check.Set(emptyCell, 0);
            }
        }
    }
}
