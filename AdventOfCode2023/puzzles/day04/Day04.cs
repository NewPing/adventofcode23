using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2023.puzzles.day04
{
    internal class Day04
    {
        public Day04()
        {
            var lines = File.ReadAllLines(@"puzzles\day04\input1.txt").ToList();
            var cards = parseInput(lines);
            part1(cards);
        }

        private void part1(List<Card> cards)
        {
            var points = 0;
            foreach(var card in cards)
            {
                points += (int)Math.Pow(2, card.WinningNumbers.Intersect(card.DrawnNumbers).Count() - 1);
            }
            Console.WriteLine(points);
        }

        private List<Card> parseInput(List<string> lines)
        {
            var cards = new List<Card>();
            foreach(var line in lines)
            {
                var card = new Card();
                var lineparts = line.Split(new char[] { ':', '|' }); //front with line id; middle with winning numbers; back with drawn numbers

                card.ID = int.Parse(Regex.Match(lineparts[0], @"(?<=Card *)\d").Value);
                card.WinningNumbers = Regex.Matches(lineparts[1], @"\d+").Select(x => int.Parse(x.Value)).ToList();
                card.DrawnNumbers = Regex.Matches(lineparts[2], @"\d+").Select(x => int.Parse(x.Value)).ToList();
                cards.Add(card);
            }
            return cards;
        }
    }

    class Card
    {
        public int ID { get; set; }
        public List<int> WinningNumbers { get; set; } = new List<int>();
        public List<int> DrawnNumbers { get; set; } = new List<int>();
    }
}
