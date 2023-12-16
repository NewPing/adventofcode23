﻿using Microsoft.VisualBasic;
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

            int output;
            output = part1(raceEntries);
            Console.WriteLine(output);
        }

        private int part1(List<RaceEntry> raceEntries)
        {
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
            return output;
        }
    }

    class RaceEntry
    {
        public int Time { get; set; }
        public int Distance { get; set; }
    }
}
