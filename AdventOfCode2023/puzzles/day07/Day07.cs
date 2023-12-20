using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.puzzles.day07
{
    internal class Day07
    {
        public Day07()
        {
            var lines = File.ReadAllLines($@"puzzles\{ GetType().Name.ToLower() }\input1.txt");
            var hands = lines.Select(x => new Hand(x)).ToList();
            hands.Sort((x, y) => IsBigger(x, y));
            for (int i = 0; i < hands.Count; i++)
            {
                hands[i].Winnings += hands[i].Bid * (i +1);
            }
            Console.WriteLine(hands.Select(x => x.Winnings).Sum());
        }

        int IsBigger(Hand a, Hand b)
        {
            if (a.Type > b.Type)
            {
                return 1;
            } else if (a.Type < b.Type)
            {
                return -1;
            } else
            {
                //they are of equal type
                for (int i = 0; i < a.Cards.Count(); i++)
                {
                    if (a.Cards[i] != b.Cards[i])
                    {
                        return IsBigger(a.Cards[i], b.Cards[i]) ? 1: -1;
                    }
                }
                return 0; //they are completely equal
            }
        }

        bool IsBigger(char a, char b)
        {
            return GetValue(a) > GetValue(b);
        }

        int GetValue(char a)
        {
            //A, K, Q, J, T, 9, 8, 7, 6, 5, 4, 3, or 2
            switch (a)
            {
                case 'A':
                    return 14;
                case 'K':
                    return 13;
                case 'Q':
                    return 12;
                case 'J':
                    return 11;
                case 'T':
                    return 10;
                default:
                    return int.Parse(a.ToString());
            }
        }

    }
    
    class Hand
    {
        public string Cards { get; set; }
        public int Bid { get; set; }
        public HandType Type { get; set; }
        public Dictionary<char, int> UniqueCardCount { get; set; } = new Dictionary<char, int>();
        public int Winnings { get; set; }
        
        public Hand(string line)
        {
            Cards = line.Split(" ")[0];
            Bid = int.Parse(line.Split(" ")[1]);
            SetUniqueCardCount();
            SetHandType();
        }

        public void SetUniqueCardCount()
        {
            foreach(var card in Cards)
            {
                if (UniqueCardCount.ContainsKey(card))
                {
                    UniqueCardCount[card]++;
                } else
                {
                    UniqueCardCount.Add(card, 1);
                }
            }
        }

        public void SetHandType()
        {
            var handType = HandType.None;
            if (UniqueCardCount.Count() == 1)
            {
                handType = HandType.FiveOfAKind;
            }
            else if (IsFourOfAKind())
            {
                handType = HandType.FourOfAKind;
            }
            else if (IsFullHouse())
            {
                handType = HandType.FullHouse;
            }
            else if (IsThreeOfAKind())
            {
                handType = HandType.ThreeOfAKind;
            }
            else if (IsTwoPair())
            {
                handType = HandType.TwoPair;
            }
            else if (IsOnePair())
            {
                handType = HandType.OnePair;
            }
            else if (IsHighCard())
            {
                handType = HandType.HighCard;
            }
            Type = handType;
        }

        public bool IsFourOfAKind()
        {
            foreach(var cardCount in UniqueCardCount)
            {
                if (cardCount.Value == 4)
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsFullHouse()
        {
            var sameThreeCards = false;
            foreach (var cardCount in UniqueCardCount)
            {
                if (cardCount.Value == 3)
                {
                    sameThreeCards = true;
                }
            }
            if (sameThreeCards == false)
            {
                return false;
            }
            foreach (var cardCount in UniqueCardCount)
            {
                if (cardCount.Value == 2)
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsThreeOfAKind()
        {
            var sameThreeCards = false;
            foreach (var cardCount in UniqueCardCount)
            {
                if (cardCount.Value == 3)
                {
                    sameThreeCards = true;
                }
            }
            if (sameThreeCards == false)
            {
                return false;
            }
            foreach (var cardCount in UniqueCardCount)
            {
                if (cardCount.Value != 1 && cardCount.Value != 3)
                {
                    return false;
                }
            }
            return true;
        }

        public bool IsTwoPair()
        {
            if (UniqueCardCount.Count != 3)
            {
                return false;
            }
            foreach (var cardCount in UniqueCardCount)
            {
                if (cardCount.Value != 2 && cardCount.Value != 1)
                {
                    return false;
                }
            }
            return true;
        }

        public bool IsOnePair()
        {
            if (UniqueCardCount.Count != 4)
            {
                return false;
            }
            foreach (var cardCount in UniqueCardCount)
            {
                if (cardCount.Value != 2 && cardCount.Value != 1)
                {
                    return false;
                }
            }
            return true;
        }

        public bool IsHighCard()
        {
            if (UniqueCardCount.Count == 5)
            {
                return true;
            } else
            {
                return false;
            }
        }
    }

    enum HandType
    {
        None,
        HighCard,
        OnePair,
        TwoPair,
        ThreeOfAKind,
        FullHouse,
        FourOfAKind,
        FiveOfAKind
    }
    
}
