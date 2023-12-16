using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2023.puzzles.day06
{
    internal class Day06
    {
        public Day06()
        {
            var lines = File.ReadAllLines(@"puzzles\day06\input1.txt");
            part1(lines);
            part2(lines);
        }

        private void part1(string[] lines)
        {
            var times = Regex.Matches(lines[0], @"\d+").Select(x => int.Parse(x.Value)).ToList();
            var distances = Regex.Matches(lines[1], @"\d+").Select(x => int.Parse(x.Value)).ToList();

            var raceEntries = new List<RaceEntry>();
            for (int i = 0; i < times.Count; i++)
            {
                var raceEntry = new RaceEntry();
                raceEntry.Time = times[i];
                raceEntry.Distance = distances[i];
                raceEntries.Add(raceEntry);
            }

            var output = 1;
            foreach(var raceEntry in raceEntries)
            {
                var possibleWins = 0;
                for (int i = 0; i < raceEntry.Time; i++)
                {
                    var reachedDistance = i * (raceEntry.Time - i);
                    if (reachedDistance > raceEntry.Distance)
                    {
                        possibleWins++;
                    }
                }
                output *= possibleWins;
            }
            Console.WriteLine(output);
        }

        private void part2(string[] lines)
        {
            var time = long.Parse(Regex.Match(lines[0].Replace(" ", ""), @"\d+").Value);
            var distance = long.Parse(Regex.Match(lines[1].Replace(" ", ""), @"\d+").Value);

            var possibleWins = 0;
            for (int i = 0; i < time; i++)
            {
                var reachedDistance = i * (time - i);
                if (reachedDistance > distance)
                {
                    possibleWins++;
                }
            }
            Console.WriteLine(possibleWins);
        }
    }

    class RaceEntry
    {
        public int Time { get; set; }
        public int Distance { get; set; }
    }
}
