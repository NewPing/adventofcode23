using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace AdventOfCode2023.puzzles.day03
{
    internal class Day03
    {
        public Day03()
        {
            var arr = File.ReadAllLines(@"puzzles\day03\input1.txt").Select(x => x.ToCharArray().ToList()).ToList();
            part1(arr);
        }

        public void part1(List<List<char>> arr)
        {
            var numbers = new List<int>();
            for (int iLine = 0; iLine < arr.Count(); iLine++)
            {
                for (int iChar = 0; iChar < arr[iLine].Count(); iChar++)
                {
                    if (isNumber(arr[iLine][iChar]))
                    {
                        if (hasNeighboringSymbol(arr, iLine, iChar))
                        {
                            numbers.Add(getNumber(arr, iLine, iChar));
                            while (iChar < arr[iLine].Count() && isNumber(arr[iLine][iChar]))
                            {
                                iChar++;
                            }
                        }
                    }
                }
            }
            Console.WriteLine(numbers.Sum());
        }

        public int getNumber(List<List<char>> arr, int iLine, int iChar)
        {
            var iMin = iChar;
            var iMax = iChar;
            while (iMin >= 0 && isNumber(arr[iLine][iMin]))
            {
                iMin--;
            }
            iMin++;
            while (iMax < arr[iLine].Count() && isNumber(arr[iLine][iMax]))
            {
                iMax++;
            }
            var str = "";
            for (int i = iMin; i < iMax; i++)
            {
                str += arr[iLine][i];
            }
            return int.Parse(str);
        }

        public bool hasNeighboringSymbol(List<List<char>> arr, int iLine, int iChar)
        {
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (iLine +i < 0 || iChar +j < 0)
                    {
                        break;
                    }
                    if (iLine +i >= arr.Count || iChar +j >= arr[iLine].Count)
                    {
                        break;
                    }
                    if (isSymbol(arr[iLine + i][iChar + j]))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool isSymbol(char c)
        {
            if (isNumber(c))
            {
                return false;
            }
            if (c.Equals('.'))
            {
                return false;
            }
            return true;
        }

        public bool isNumber(char c)
        {
            return Regex.IsMatch(c.ToString(), @"\d");
        }
    }
}
