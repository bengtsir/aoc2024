using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aoc2024
{
    internal class Day19
    {
        string[] Towels;

        internal bool StringMatch(string full, string part, int pos)
        {
            for (int p = 0; p < part.Length; p++)
            {
                if (pos+p >= full.Length || full[pos + p] != part[p])
                {
                    return false;
                }
            }
            return true;
        }

        bool[] Tested;
        Int64[] Result;

        internal long IsPossible(string pattern, int pos)
        {
            if (pos == 0)
            {
                Tested = new bool[pattern.Length];
                Result = new long[pattern.Length];
            }

            if (pos >= pattern.Length)
            {
                return 1;
            }

            if (Tested[pos])
            {
                return Result[pos];
            }

            var sum = Towels.Sum(s => StringMatch(pattern, s, pos) ? IsPossible(pattern, pos + s.Length) : 0);
            Tested[pos] = true;
            Result[pos] = sum;
            return sum;
        }

        private static int LSort(string a, string b)
        {
            return a.Length - b.Length;
        }

        public void Part1()
        {
            var data = File.ReadAllLines(@"data\day19.txt");

            /*
            data = new[]
            {
                "r, wr, b, g, bwu, rb, gb, br",
                "",
                "brwrr",
                "bggr",
                "gbbr",
                "rrbgbr",
                "ubwu",
                "bwurrg",
                "brgr",
                "bbrgwb",
            };
            */

            Towels = data[0].Split(new[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries).ToArray();

            var patterns = data.Skip(2).ToArray();

            long sum = 0;
            long part2sum = 0;

            for (int i = 0; i < patterns.Length; i++)
            {
                Console.WriteLine($"String {i + 1} of {patterns.Length}");
                var possible = IsPossible(patterns[i], 0);
                if (possible > 0)
                {
                    sum += 1;
                }
                part2sum += possible;
                Console.WriteLine($"Possible: {sum} ({possible} combos) of {i + 1}");
            }

            //sum = patterns.Sum(s => IsPossible(s, 0));

            Console.WriteLine($"Answer is {sum}");
            Console.WriteLine($"Answer for part 2 is {part2sum}");
        }

        public void Part2()
        {
            var data = File.ReadAllLines(@"data\day19.txt");

            List<int> cals = new List<int>();

            var values = data.Select(r => r.Split(' ')).ToArray();




            Console.WriteLine($"Answer is (see above)");
        }

    }
}
