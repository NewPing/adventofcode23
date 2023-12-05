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
            
            var seeds = Regex.Matches(contentParts[0], @"\d+").Select(x => long.Parse(x.Value)).ToList();

            var maps = new List<Map>();
            for (long i = 1; i < contentParts.Length; i++)
            {
                var map = new Map();
                var partLines = contentParts[i].Split("\n");
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
            Console.WriteLine(locationNumbers.Min());
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
