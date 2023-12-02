using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2023.puzzles.day02
{
    internal class Day02
    {
        public Day02()
        {
            part1();
        }

        public void part1()
        {
            //only 12 red cubes, 13 green cubes, and 14 blue cubes
            var maxRed = 12;
            var maxGreen = 13;
            var maxBlue = 14;

            var lines = File.ReadAllLines(@"puzzles\day02\input1.txt");
            var sum = 0;
            foreach (var line in lines)
            {
                var gameInfo = new GameInfo();
                gameInfo.id = int.Parse(Regex.Match(line, @"(?<=Game )\d+").Value);
                var linePart2 = line.Replace("Game " + gameInfo.id + ":", "");
                var lineDraws = linePart2.Split(";");
                foreach (var lineDraw in lineDraws)
                {
                    var draw = new Draw();
                    draw.red = extractColoredBalls(lineDraw, "red");
                    draw.green = extractColoredBalls(lineDraw, "green");
                    draw.blue = extractColoredBalls(lineDraw, "blue");
                    gameInfo.draws.Add(draw);
                }
                if (gameInfo.IsValid(maxRed, maxGreen, maxBlue))
                {
                    Console.WriteLine("Game " + gameInfo.id + " is valid");
                    sum += gameInfo.id;
                } else
                {
                    Console.WriteLine("Game " + gameInfo.id + " is invalid");

                }
            }
            Console.WriteLine(sum);
        }

        public int extractColoredBalls(string lineDraw, string color)
        {
            var ballCount = Regex.Matches(lineDraw, @$"\d+(?= { color })");
            if (ballCount.Count > 0)
            {
                return int.Parse(ballCount.Single().Value);
            }
            return 0;
        }
    }

    class GameInfo
    {
        public int id { get; set; }
        public List<Draw> draws { get; set; } = new List<Draw>();

        public bool IsValid(int maxRed, int maxGreen, int maxBlue)
        {
            foreach(var draw in draws)
            {
                if (!draw.IsValid(maxRed, maxGreen, maxBlue))
                {
                    return false;
                }
            }
            return true;
        }
    }

    class Draw
    {
        public int red { get; set; }
        public int green { get; set; }
        public int blue { get; set; }

        public bool IsValid(int maxRed, int maxGreen, int maxBlue)
        {
            if (red > maxRed)
            {
                return false;
            }
            if (green > maxGreen)
            {
                return false;
            }
            if (blue > maxBlue)
            {
                return false;
            }
            return true;
        }
    }
}
