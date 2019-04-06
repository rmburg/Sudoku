using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku
{
#pragma warning disable CS0660 // Typ definiert Operator == oder Operator !=, überschreibt jedoch nicht Object.Equals(Objekt o)
#pragma warning disable CS0661 // Typ definiert Operator == oder Operator !=, überschreibt jedoch nicht Object.GetHashCode()
    public struct Coords
#pragma warning restore CS0661 // Typ definiert Operator == oder Operator !=, überschreibt jedoch nicht Object.GetHashCode()
#pragma warning restore CS0660 // Typ definiert Operator == oder Operator !=, überschreibt jedoch nicht Object.Equals(Objekt o)
    {
        public int x;
        public int y;

        public Coords(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public static bool operator ==(Coords a, Coords b)
        {
            return ((a.x == b.x) && (a.y == b.y));
        }

        public static bool operator !=(Coords a, Coords b)
        {
            return !((a.x == b.x) && (a.y == b.y));
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

        public int Get(Coords coords)
        {
            return internal_grid[coords.x][coords.y];
        }

        public void Set(int x, int y, int value)
        {
            internal_grid[x][y] = value;
        }

        public void Set(Coords coords, int value)
        {
            internal_grid[coords.x][coords.y] = value;
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

        public List<int> GetLinePossibilities(Coords coords)
        {
            List<int> possibilities = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            for (int i = 0; i < 9; i++)
            {
                if (i != coords.x)
                {
                    possibilities.Remove(Math.Abs(Get(i, coords.y)));
                }
            }
            return possibilities;
        }

        public List<int> GetRowPossibilities(Coords coords)
        {
            List<int> possibilities = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            for (int i = 0; i < 9; i++)
            {
                if (i != coords.y)
                {
                    possibilities.Remove(Math.Abs(Get(coords.x, i)));
                }
            }
            return possibilities;
        }

        public List<int> GetSquarePossibilities(Coords coords)
        {
            List<int> possibilities = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            Grid subgrid = new Grid(3);
            for (int i = 0; i < 3; i++) // make a new 3x3 subgrid that represents the square the cell is in
            {
                for (int j = 0; j < 3; j++)
                {
                    Coords checked_cell = new Coords(((coords.x) / 3) * 3 + i, ((coords.y) / 3) * 3 + j);
                    if (checked_cell != coords) // if the input cell is being checked, don't remove its number (because it shouldn't make itself impossible)
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

        public List<int> GetAllPossibilities(Coords coords)
        {
            List<int> lineP = GetLinePossibilities(coords);
            List<int> rowP = GetRowPossibilities(coords);
            List<int> sqP = GetSquarePossibilities(coords);

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
                    Coords coords = new Coords(i, j);
                    if (!(GetAllPossibilities(coords).Contains(Math.Abs(Get(coords))) || Get(coords) == 0)) // check if each number is valid or zero. If not, return false
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public Coords GetFirstEmptyCell()
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (Get(i, j) == 0)
                    {
                        return new Coords(i, j);
                    }
                }
            }
            return new Coords(-1, -1);
        }
    }
}
/*
+=======+=======+=======+
‖ 1 1 1 ‖       ‖       ‖
‖ 1 1 1 ‖       ‖       ‖
‖ 1 1 1 ‖       ‖       ‖
+=======+=======+=======+
‖       ‖       ‖       ‖
‖       ‖       ‖       ‖
‖       ‖       ‖       ‖
+=======+=======+=======+
‖       ‖       ‖       ‖
‖       ‖       ‖       ‖
‖       ‖       ‖       ‖
+=======+=======+=======+
*/
