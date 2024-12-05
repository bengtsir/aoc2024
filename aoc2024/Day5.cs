using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using aoc2024.Structs;

namespace aoc2024
{
    internal class Day5
    {
        public bool MatchesRules(List<Point> rules, List<int> seq)
        {
            for (int i = 0; i < seq.Count; i++)
            {
                var left = seq.Take(i);
                var right = seq.Skip(i + 1);
                int v = seq[i];

                foreach (var rule in rules)
                {
                    if (v == rule.X)
                    {
                        if (left.Any(x => x == rule.Y))
                        {
                            return false;
                        }
                    }
                    else if (v == rule.Y)
                    {
                        if (right.Any(x => x == rule.X))
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        private static List<Point> Rules;

        internal static int Comparer(int left, int right)
        {
            foreach (var r in Rules)
            {
                if (left == r.X && right == r.Y)
                {
                    return -1;
                }
                else if (left == r.Y && right == r.X)
                {
                    return 1;
                }
            }

            return 0;
        }

        public void Part1()
        {
            var data = File.ReadAllLines(@"data\day5.txt");

            var rules = new List<Point>();
            int row = 0;
            int sum = 0;

            while (data[row].Any(c => c == '|'))
            {
                rules.Add(new Point(data[row++], '|'));
            }

            row++;

            var sequences = data.Skip(row).Select(r => r.Split(',').Select(Int32.Parse).ToList()).ToList();

            foreach (var seq in sequences.Where(s => MatchesRules(rules, s)))
            {
                int c = seq.Count();

                sum += seq[c / 2];
            }


            Console.WriteLine($"Answer is {sum}");
        }

        public void Part2()
        {
            var data = File.ReadAllLines(@"data\day5.txt");

            var rules = new List<Point>();
            int row = 0;
            int sum = 0;

            while (data[row].Any(c => c == '|'))
            {
                rules.Add(new Point(data[row++], '|'));
            }

            row++;

            var sequences = data.Skip(row).Select(r => r.Split(',').Select(Int32.Parse).ToList()).ToList();

            Rules = rules.Select(r => r).ToList();

            foreach (var seq in sequences.Where(s => !MatchesRules(rules, s)))
            {
                seq.Sort(Comparer);
                int c = seq.Count();

                sum += seq[c / 2];
            }


            Console.WriteLine($"Answer is {sum}");
        }

    }
}
