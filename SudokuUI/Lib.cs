using System;
using System.Collections.Generic;
using Sudoku;
using System.IO;
using Newtonsoft.Json;


namespace SudokuUI
{
    class Lib
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

        public static void SaveSudokuFile(Grid grid, string path)
        {
            File.WriteAllText(path, JsonConvert.SerializeObject(grid.GetGrid()));
        }
    }
}
