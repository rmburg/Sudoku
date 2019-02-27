using System;
using System.Collections.Generic;
using System.Linq;
using static Sudoku.Lib;

namespace Sudoku
{
    public struct Coords
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
    
    public class Sudoku
    {
        const string divLine = "+-------+-------+-------+";
        const string line = "| {0} {1} {2} | {3} {4} {5} | {6} {7} {8} |";
        public static Grid exampleGrid = new Grid(new int[][] {
            new int[]{4, 9, 3, 1, 6, 5, 2, 7, 8},
            new int[]{6, 7, 8, 9, 3, 2, 1, 5, 4},
            new int[]{5, 1, 2, 8, 4, 7, 9, 6, 3},
            new int[]{2, 5, 6, 3, 1, 8, 4, 9, 7},
            new int[]{9, 4, 7, 5, 2, 6, 8, 3, 1},
            new int[]{8, 3, 1, 4, 7, 9, 5, 2, 6},
            new int[]{1, 8, 5, 7, 9, 3, 6, 4, 2},
            new int[]{7, 6, 9, 2, 8, 4, 3, 1, 5},
            new int[]{3, 2, 4, 6, 5, 1, 7, 8, 9}
        }, 9);
        public static Grid exampleGrid2 = new Grid(new int[][] {
            new int[]{0, 9, 3, 0, 6, 0, 2, 7, 0},
            new int[]{6, 0, 0, 0, 3, 2, 0, 5, 4},
            new int[]{5, 0, 2, 8, 0, 7, 9, 6, 3},
            new int[]{0, 5, 0, 0, 1, 0, 4, 9, 0},
            new int[]{0, 4, 7, 0, 2, 6, 0, 3, 1},
            new int[]{8, 0, 1, 4, 0, 9, 5, 0, 6},
            new int[]{1, 0, 0, 7, 9, 0, 6, 4, 2},
            new int[]{7, 6, 9, 2, 0, 4, 3, 1, 5},
            new int[]{3, 0, 4, 0, 5, 1, 7, 8, 0}
        }, 9);
        public static Grid exampleGrid3 = new Grid(new int[][] {
            new int[]{0, 9, 0, 0, 0, 0, 2, 0, 0},
            new int[]{6, 0, 0, 0, 3, 2, 0, 5, 0},
            new int[]{0, 0, 2, 8, 0, 7, 0, 0, 0},
            new int[]{0, 5, 0, 0, 1, 0, 4, 9, 0},
            new int[]{0, 0, 7, 0, 2, 0, 0, 3, 1},
            new int[]{8, 0, 1, 4, 0, 9, 5, 0, 6},
            new int[]{0, 0, 0, 0, 9, 0, 0, 0, 0},
            new int[]{0, 6, 0, 2, 0, 0, 3, 0, 0},
            new int[]{0, 0, 4, 0, 5, 1, 7, 8, 0}
        }, 9);
        public static Grid exampleGrid4_hard = new Grid(new int[][] {
            new int[]{8, 0, 0, 0, 0, 0, 0, 0, 0},
            new int[]{0, 0, 3, 6, 0, 0, 0, 0, 0},
            new int[]{0, 7, 0, 0, 9, 0, 2, 0, 0},
            new int[]{0, 5, 0, 0, 0, 7, 0, 0, 0},
            new int[]{0, 0, 0, 0, 4, 5, 7, 0, 0},
            new int[]{0, 0, 0, 1, 0, 0, 0, 3, 0},
            new int[]{0, 0, 1, 0, 0, 0, 0, 6, 8},
            new int[]{0, 0, 8, 5, 0, 0, 0, 1, 0},
            new int[]{0, 9, 0, 0, 0, 0, 4, 0, 0},
        }, 9);
        public static Grid exampleGridEmpty = new Grid(new int[][] {
            new int[]{0, 0, 0, 0, 0, 0, 0, 0, 0},
            new int[]{0, 0, 0, 0, 0, 0, 0, 0, 0},
            new int[]{0, 0, 0, 0, 0, 0, 0, 0, 0},
            new int[]{0, 0, 0, 0, 0, 0, 0, 0, 0},
            new int[]{0, 0, 0, 0, 0, 0, 0, 0, 0},
            new int[]{0, 0, 0, 0, 0, 0, 0, 0, 0},
            new int[]{0, 0, 0, 0, 0, 0, 0, 0, 0},
            new int[]{0, 0, 0, 0, 0, 0, 0, 0, 0},
            new int[]{0, 0, 0, 0, 0, 0, 0, 0, 0},
        }, 9);
        public enum Difficulty
        {
            hard = 35,
            medium = 50,
            easy = 75
        }

        Difficulty MyDifficulty;
        public Grid grid;
        public Grid solution;
        public List<Grid> possible_solutions = new List<Grid>();

        public Sudoku(Grid grid)
        {
            this.grid = grid;
        }

        public Sudoku(Difficulty difficulty)
        {
            solution = GenerateRandomSolution();
            grid = GeneratePuzzle(solution, difficulty);
            MyDifficulty = difficulty;
        }

        public static Grid GenerateRandomSolution()
        {
            Generator gen = new Generator();
            return gen.Generate();
        }

        public Grid GeneratePuzzle(Grid solution, Difficulty difficulty)
        {
            return new Grid(9); // TODO
        }
        
        public void Solve()
        {
            Solver solver = new Solver();
            possible_solutions = solver.Solve(this);
        }
        
        public static Sudoku LoadRandomSudoku(Difficulty difficulty)
        {
            return new Sudoku(exampleGridEmpty);
        }
        
        //TODO: IsSolvable()
        //TODO: ShowHint()
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
            return internal_grid[x - 1][y - 1];
        }

        public int Get(Coords coords)
        {
            return internal_grid[coords.x - 1][coords.y - 1];
        }

        public void Set(int x, int y, int value)
        {
            internal_grid[x - 1][y - 1] = value;
        }

        public void SetGrid(Grid grid)
        {
            internal_grid = grid.internal_grid;
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
            for (int i = 1; i < 10; i++)
            {
                if (i != coords.x)
                {
                    possibilities.Remove(Get(i, coords.y));
                }
            }
            return possibilities;
        }

        public List<int> GetRowPossibilities(Coords coords)
        {
            List<int> possibilities = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            for (int i = 1; i < 10; i++)
            {
                if (i != coords.y)
                {
                    possibilities.Remove(Get(coords.x, i));
                }
            }
            return possibilities;
        }

        public List<int> GetSquarePossibilities(Coords coords)
        {
            List<int> possibilities = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            Grid subgrid = new Grid(3);
            for (int i = 1; i < 4; i++) // make a new 3x3 subgrid that represents the square the cell is in
            {
                for (int j = 1; j < 4; j++)
                {
                    Coords checked_cell = new Coords(((coords.x - 1) / 3) * 3 + i, ((coords.y - 1) / 3) * 3 + j);
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
                possibilities.Remove(number);
            }
            return possibilities;
        }

        public List<int> GetAllPossibilities(Coords coords)
        {
            List<int> lineP = GetLinePossibilities(coords);
            List<int> rowP = GetRowPossibilities(coords);
            List<int> sqP = GetSquarePossibilities(coords);

            List<int> intersect = lineP.Intersect(rowP).Intersect(sqP).ToList(); // alle nummern, die sowohl in der zeile, spalte und dem 3x3-Feld möglich sind

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
            for (int i = 1; i < 10; i++)
            {
                for (int j = 1; j < 10; j++)
                {
                    Coords coords = new Coords(i, j);
                    if (!(GetAllPossibilities(coords).Contains(Get(coords)) || Get(coords) == 0)) // check if each number is valid or zero. If not, return false
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public Coords GetFirstEmptyCell()
        {
            for (int i = 1; i < this.size + 1; i++)
            {
                for (int j = 1; j < this.size + 1; j++)
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
    public class Solver
    {
        Grid temp_grid;
        List<Grid> possible_solutions = new List<Grid>();

        public List<Grid> Solve(Sudoku input)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();

            temp_grid = input.grid.Clone();
            SolveRecursive(input.grid);

            watch.Stop();

            double elapsedSeconds = watch.ElapsedMilliseconds / 1000d;
            //TODO: use time
            return possible_solutions;
        }

        public void SolveRecursive(Grid input)
        {
            if (!input.ContainsZeros()) // if it is fully solved
            {
                possible_solutions.Add(input.Clone());
                return;
            }
            else
            {
                // find the first empty cell
                Coords emptyCell = input.GetFirstEmptyCell();

                // get all possibilities for (x, y)

                List<int> possibilities = input.GetAllPossibilities(emptyCell);
                foreach (int item in possibilities)
                {
                    input.Set(emptyCell.x, emptyCell.y, item);
                    SolveRecursive(input);
                }
                //backtrack
                input.Set(emptyCell.x, emptyCell.y, 0);
            }
        }

        public Grid SolveNaive(Grid input)
        {
            Grid output = input;
            bool changed = false;
            for (int i = 1; i < 10; i++)
            {
                for (int j = 1; j < 10; j++)
                {
                    if (output.GetAllPossibilities(new Coords(i, j)).Count == 1 && output.Get(i, j) == 0)
                    {
                        output.Set(i, j, input.GetAllPossibilities(new Coords(i, j))[0]);
                        changed = true;
                    }
                }
            }
            if (!changed) // if nothing was changed
            {
                return output;
            }
            else
            {
                return SolveNaive(output);
            }
        }
    }
    public class Generator
    {
        Grid temp_grid;
        Grid outGrid;
        bool done = false;
        public Grid Generate()
        {
            done = false;
            var watch = System.Diagnostics.Stopwatch.StartNew();

            temp_grid = new Grid(9);
            GenerateRecursive(temp_grid);

            watch.Stop();

            double elapsedSeconds = watch.ElapsedMilliseconds / 1000d;
            //TODO: use time
            return outGrid;
        }

        public void GenerateRecursive(Grid input)
        {
            if (!input.ContainsZeros()) // if it is fully solved
            {
                outGrid = input.Clone();
                done = true;
                return;
            }
            else
            {
                // find the first empty cell
                Coords emptyCell = input.GetFirstEmptyCell();

                // get all possibilities for (x, y)
                List<int> possibilities = Shuffle(input.GetAllPossibilities(emptyCell));
                foreach (int item in possibilities)
                {
                    input.Set(emptyCell.x, emptyCell.y, item);
                    
                    System.Threading.Thread.Sleep(50);

                    GenerateRecursive(input);
                    if (done)
                    {
                        return;
                    }
                }
                //backtrack
                input.Set(emptyCell.x, emptyCell.y, 0);
            }
        }
    }
    public static class Lib
    {
        public static List<T> Shuffle<T>(List<T> list)
        {
            Random rnd = new Random();
            for (int i = 0; i < list.Count; i++)
            {
                int k = rnd.Next(0, i);
                T value = list[k];
                list[k] = list[i];
                list[i] = value;
            }
            return list;
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
