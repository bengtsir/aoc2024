using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace aoc2024
{
    internal class Day3
    {
        public void Part1()
        {
            var data = File.ReadAllText(@"data\day3.txt");

            string pat = @"mul\(([0-9]{1,3}),([0-9]{1,3})\)";

            Regex r = new Regex(pat);

            // Match the regular expression pattern against a text string.
            Match m = r.Match(data);

            int matchCount = 0;
            Int64 sum = 0;

            while (m.Success)
            {
                Console.WriteLine("Match" + (++matchCount));
                for (int i = 1; i <= 2; i++)
                {
                    Group g = m.Groups[i];
                    Console.WriteLine("Group" + i + "='" + g + "'");
                }

                var partsum = Convert.ToInt32(m.Groups[1].Value) * Convert.ToInt32(m.Groups[2].Value);

                sum += partsum;

                m = m.NextMatch();
            }



            Console.WriteLine($"Answer is {sum}");
        }

        public void Part2()
        {
            var data = File.ReadAllLines(@"data\day3.txt");

            List<int> cals = new List<int>();

            var values = data.Select(r => r.Split(' ')).ToArray();




            Console.WriteLine($"Answer is {values.Count()}");
        }

    }
}
