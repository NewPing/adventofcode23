using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2023.puzzles.day01
{
    internal class Day01
    {
        public Day01()
        {
            //part1();
            part2();
        }

        private void part1()
        {
            var lines = File.ReadAllLines(@"puzzles\day01\input1.txt");
            int sum = 0;
            foreach (var line in lines)
            {
                var numbers = Regex.Matches(line, @"\d");
                if (numbers.Count == 1)
                {
                    sum += int.Parse(numbers.First().Value + numbers.First().Value);
                }
                else if (numbers.Count > 1)
                {
                    sum += int.Parse(numbers.First().Value + numbers.Last().Value);
                }
                else
                {
                    throw new Exception("numbers count cant be 0");
                }
            }
            Console.WriteLine(sum);
        }

        private void part2()
        {
            var lines = File.ReadAllLines(@"puzzles\day01\input1.txt").Select(x => x.ToLower());
            int sum = 0;
            foreach (var line in lines)
            {
                //var numbers = Regex.Matches(line, @"\d|one|two|three|four|five|six|seven|eight|nine"); //doesnt work since the characters of e.g. "two" get consumed for "twone"
                var matches = Regex.Matches(line, @"(?=(\d|one|two|three|four|five|six|seven|eight|nine))");
                var numbers = matches.Select(x => x.Groups[1].Value); //need to go for group since the match itself is empty and the group holds the value
                if (numbers.Count() == 1)
                {
                    sum += int.Parse(extractNumber(numbers.First()) + "" + extractNumber(numbers.First()));
                }
                else if (numbers.Count() > 1)
                {
                    sum += int.Parse(extractNumber(numbers.First()) + "" + extractNumber(numbers.Last()));
                }
                else
                {
                    throw new Exception("numbers count cant be 0");
                }
            }
            Console.WriteLine(sum);
        }

        private int extractNumber(string number)
        {
            var num = Regex.Matches(number, @"\d");
            if (num.Count == 1)
            {
                return int.Parse(num.Single().Value);
            } else
            {
                switch (number)
                {
                    case "one": return 1;
                    case "two": return 2;
                    case "three": return 3;
                    case "four": return 4;
                    case "five": return 5;
                    case "six": return 6;
                    case "seven": return 7;
                    case "eight": return 8;
                    case "nine": return 9;
                }
            }
            throw new Exception("number cant be converted");
        }
    }
}
