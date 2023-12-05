using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2023.puzzles.day05
{
    internal class Day05
    {
        public Day05()
        {
            var content = File.ReadAllText(@"puzzles\day05\input1.txt").Replace("\r\n", "\n");
            var contentParts = content.Split("\n\n");
            //part1(contentParts);
            part2(contentParts);
        }

        private void part1(string[] contentParts)
        {
            var maps = parseInput(contentParts);
            var seeds = Regex.Matches(contentParts[0], @"\d+").Select(x => long.Parse(x.Value)).ToList();
            var locationNumbers = SeedsToLocation(seeds, maps);
            Console.WriteLine(locationNumbers.Min());
        }

        private void part2(string[] contentParts)
        {
            var maps = parseInput(contentParts);
            var seedNumbers = Regex.Matches(contentParts[0], @"\d+").Select(x => long.Parse(x.Value)).ToList();
            var seedRanges = new List<MRange>();
            for (int i = 0; i < seedNumbers.Count -1; i+=2)
            {
                seedRanges.Add(new MRange(seedNumbers[i], seedNumbers[i + 1]));
            }
            for(int i = 0;i < maps.Count;i++)
            {
                for (int j = 0; j < seedRanges.Count;j++)
                {
                    MRange min = getNextLowestRange(seedRanges[j].SourceStartPos, maps[i]);
                    //if no smaller value then no fitting mapping overall
                    if(min.SourceStartPos == -1)
                    {
                        continue;
                    }
                    long cutoff = (min.SourceStartPos + min.RangeLength) - (seedRanges[j].SourceStartPos + seedRanges[j].RangeLength);
                    //the SeedRange fits completely in the found mapping
                    if(cutoff >= 0)
                    {
                        seedRanges[j].SourceStartPos = seedRanges[j].SourceStartPos + (min.DestinationStartPos-min.SourceStartPos);
                        continue;
                    }
                    //no mapping fits
                    if(Math.Abs(cutoff) >= seedRanges[j].RangeLength)
                    {
                        continue;
                    }
                    //the seedrange doesnt fit completely
                    cutoff = Math.Abs(cutoff);
                    //add leftoverrange first
                    seedRanges.Add(new MRange(seedRanges[j].SourceStartPos + (seedRanges[j].RangeLength - cutoff), cutoff));
                    //adjust rest
                    seedRanges[j].SourceStartPos = seedRanges[j].SourceStartPos + (min.DestinationStartPos - min.SourceStartPos);
                    seedRanges[j].RangeLength = seedRanges[j].RangeLength - cutoff;

                }   
            }
            Console.WriteLine(seedRanges.Select(x => x.SourceStartPos).Min());
        }
        MRange getNextLowestRange(long sourceStart, Map map)
        {
            MRange min = new MRange(-1,1);
            foreach(MRange range in map.Ranges)
            {
                if(range.SourceStartPos <= sourceStart && min.SourceStartPos < range.SourceStartPos )
                {
                    min = range;
                }
            }
            return min;
        }


        private List<long> ToRange(long start, long length)
        {
            var range = new List<long>();
            for (long i = 0; i < length; i++)
            {
                range.Add(start + i);
            }
            return range;
        }

        public List<long> SeedsToLocation(List<long> seeds, List<Map> maps)
        {
            var locationNumbers = new List<long>();
            foreach (var seed in seeds)
            {
                var currentSource = seed;
                for (int i = 0; i < maps.Count; i++)
                {
                    currentSource = maps[i].SourceToDest(currentSource);
                }
                locationNumbers.Add(currentSource);
            }
            return locationNumbers;
        }

        private List<Map> parseInput(string[] input)
        {
            var maps = new List<Map>();
            for (long i = 1; i < input.Length; i++)
            {
                var map = new Map();
                var partLines = input[i].Split("\n");
                for (long j = 1; j < partLines.Length; j++)
                {
                    var sections = Regex.Matches(partLines[j], @"\d+").Select(x => long.Parse(x.Value)).ToList();
                    var range = new MRange();
                    range.DestinationStartPos = sections[0];
                    range.SourceStartPos = sections[1];
                    range.RangeLength = sections[2];
                    map.Ranges.Add(range);
                }
                maps.Add(map);
            }
            return maps;
        }
        

    }

    class Map
    {
        public List<MRange> Ranges { get; set; } = new List<MRange>();
        
        public long SourceToDest(long source)
        {
            foreach(var range in Ranges)
            {
                if (range.IsInRange(source))
                {
                    return range.SourceToDest(source);
                }
            }
            return source;
        }
    }

    class MRange
    {
        public long DestinationStartPos { get; set; }
        public long SourceStartPos { get; set; }
        public long RangeLength { get; set; }
        public MRange() { }
        public MRange(long sourceStart, long lenght)
        {
            this.SourceStartPos = sourceStart;
            this.RangeLength = lenght;
        }
        public bool IsInRange(long source)
        {
            return SourceStartPos <= source && source <= SourceStartPos + RangeLength;
        }

        public long SourceToDest(long source)
        {
            return source + (DestinationStartPos - SourceStartPos);
        }
    }
}
