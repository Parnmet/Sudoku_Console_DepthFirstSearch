using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Sudoku_Console
{
    class ImportCSV
    {
        static int sqr = 9;
        public static int[,] array = new int[,]{{ 3, 4, 1, 2, 6, 7, 8, 5, 9 },//0},
                                    { 6, 2, 7, 0, 9, 8, 4, 0, 1 },
                                    { 8, 5, 9, 3, 1, 0, 7, 0, 2 },
                                    { 1, 0, 8, 0, 3, 0, 5, 7, 6 },
                                    { 5, 6, 4, 7, 0, 0, 0, 0, 3 },
                                    { 7, 3, 0, 0, 0, 0, 9, 8, 4 },
                             /*0*/  { 9, 7, 6, 8, 0, 1, 0, 0, 5 },
                                    { 2, 0, 0, 6, 0, 5, 0, 0, 7 },
                             /*0*/  { 4, 1, 5, 9, 7, 3, 6, 2, 8 } };

        public static int[,] array2 = new int[,]{{ 3, 4, 1, 2, 6, 7, 8, 5, 9 },//0},
                                    { 6, 2, 7, 0, 9, 8, 4, 0, 1 },
                                    { 8, 5, 9, 3, 1, 0, 7, 0, 2 },
                                    { 1, 0, 8, 0, 3, 0, 5, 7, 6 },
                                    { 5, 6, 4, 7, 0, 0, 0, 0, 3 },
                                    { 7, 3, 0, 0, 0, 0, 9, 8, 4 },
                             /*0*/  { 9, 7, 6, 8, 0, 1, 0, 0, 5 },
                                    { 2, 0, 0, 6, 0, 5, 0, 0, 7 },
                             /*0*/  { 4, 1, 5, 9, 7, 3, 6, 2, 8 } };

        public static IEnumerable<string[]> importSudoku()
        {
            //List<string> listA = new List<string>();
            //using (var fs = File.OpenRead(@"C:\Users\mos_t\Desktop\easy1.csv"))
            //using (var reader = new StreamReader(fs))
            //{
            //    while (!reader.EndOfStream)
            //    {
            //        var line = reader.ReadLine();
            //        var values = /*line*/.Split(',');
            //        listA.Add(line);
            //    }
            //}
            var lines = File.ReadAllLines(@"C:\Users\mos_t\Desktop\easy1.csv").Select(a => a.Split(';'));
            var csv = from line in lines
                      select (line[0].Split(',')).ToArray();
            return csv;
        }

        //public static int[,] InsertValueIfHasOneSlot(ref int[,] array)
        //{
        //    for (int i = 0; i < sqr; i++)
        //    {
        //        var sumOfRow = 0;
        //        for (int j = 0; j < sqr; j++)
        //        {
        //            sumOfRow += array[i,j];
        //        }
        //    }
        //    return array;
        //}

        public static Boolean Rule()
        {
            //Check row
            for (int i = 0; i < sqr; i++)
            {
                var row = new int[9];
                for (int j = 0; j < sqr; j++)
                {
                    row[j] = array[i,j];
                }

                //Check each value in row is unique.
                if (!(row.Distinct().Count() == row.Length))
                {
                    return false;
                }
            }

            return true;
            //Check column
            //for (int i = 0; i < sqr; i++)
            //{
            //    var row = new int[1];
            //    for (int j = 0; j < sqr; j++)
            //    {
            //        row[j] = array[i, j];
            //    }

            //    if (!(row.Distinct().Count() == row.Length))
            //    {
            //        return false;
            //    }
            //}

            //Check box

        }

        //bool allUnique = Numbers.Distinct().Count() == Numbers.Length;

        public static Dictionary<int, List<int>> GetEmptySlot(int[,] array)
        {
            Dictionary<int, List<int>> EmptySlot = new Dictionary<int, List<int>>();

            for (int i = 0; i < sqr; i++)
            {
                List<int> column = new List<int>();
                for (int j = 0; j < sqr; j++)
                {
                    if (array[i,j] == 0)
                    {
                        column.Add(j);
                    }
                }
                EmptySlot.Add(i,column);
            }
            return EmptySlot;
        }

        public static void AddValue(int indexOfEmptySlot, int value)
        {
            var EmptySlot = GetEmptySlot(array2);
            List<int> pos_list = new List<int>();
            foreach (var row in EmptySlot)
            {
                foreach (var column in row.Value)
                {
                    pos_list.Add(row.Key);
                    pos_list.Add(column);
                }
            }
            array[pos_list[indexOfEmptySlot*2], pos_list[indexOfEmptySlot*2+1]] = value;
        }

        public static int[] PossibilityList(int indexOfEmptySlot)
        {
            var EmptySlot = GetEmptySlot(array2);
            List<int[]> pos_list = new List<int[]>();
            foreach (var row in EmptySlot)
            {
                foreach (var column in row.Value)
                {
                    var pos = Possibility(array2, row.Key, column);
                    pos_list.Add(pos.ToArray());
                    //Console.WriteLine("[" + row.Key + "," + column + "] - " + singleString);
                }
            }
            if (indexOfEmptySlot >= pos_list.Count)//pos_list.Count)
            {
                return null;
            }
            return pos_list[indexOfEmptySlot];
        }

        public static List<int> Possibility(int[,] array, int row, int col)
        {          
            //Create exist number list 
            List<int> existNumber_list = new List<int>();
            for (int j = 0; j < sqr; j++)
            {
                existNumber_list.Add(array[row, j]);
            }
            for (int i = 0; i < sqr; i++)
            {
                existNumber_list.Add(array[i, col]);
            }
            int box_row = 0;
            int box_col = 0;
            for (int i = 1; i <= 3; i++)
            {
                if(row/3 < i)
                {
                    box_row = i - 1;
                    for (int j = 1; j <= 3; j++)
                    {
                        if(col/3 < j)
                        {
                            box_col = j - 1;
                            break; 
                        }
                    }
                    break;
                }
            }
            for (int i = box_row * 3; i < box_row * 3 + 3; i++)
            {
                for (int j = box_col * 3; j < box_col * 3 + 3; j++)
                {
                    existNumber_list.Add(array[i, j]);
                }
            }

            var possibilityNumber_IEnumerable = Enumerable.Range(1, 9).Except(existNumber_list);
            var possibilityNumber_list = possibilityNumber_IEnumerable.ToList();
            return possibilityNumber_list;
        }

        public static void PrintSudoku()
        {
            for(int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    Console.Write(array[i, j]);
                }
                Console.WriteLine();
            }
        }
    }
}
