using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aoc2024
{
    internal class Day7
    {
        public bool IsPossible(Int64 target, Int64 cumsum, List<Int64> values)
        {
            if (values.Count == 0)
            {
                return target == cumsum;
            }

            if (cumsum * values[0] <= target && IsPossible(target, cumsum * values[0], values.Skip(1).ToList()))
            {
                return true;
            }
            else if (cumsum + values[0] <= target && IsPossible(target, cumsum + values[0], values.Skip(1).ToList()))
            {
                return true;
            }

            return false;
        }

        public bool IsMorePossible(Int64 target, Int64 cumsum, List<Int64> values)
        {
            if (values.Count == 0)
            {
                return target == cumsum;
            }

            if (cumsum * values[0] <= target && IsMorePossible(target, cumsum * values[0], values.Skip(1).ToList()))
            {
                return true;
            }
            else if (cumsum + values[0] <= target && IsMorePossible(target, cumsum + values[0], values.Skip(1).ToList()))
            {
                return true;
            }

            var cated = Int64.Parse(cumsum.ToString() + values[0].ToString());

            if (cated <= target && IsMorePossible(target, cated, values.Skip(1).ToList()))
            {
                return true;
            }

            return false;
        }

        public void Part1()
        {
            var data = File.ReadAllLines(@"data\day7.txt");

            var values = data.Select(r => r.Split(new[] {' ', ':'}, StringSplitOptions.RemoveEmptyEntries).Select(Int64.Parse).ToList()).ToArray();

            Int64 sum = 0;

            foreach (var line in values)
            {
                if (IsPossible(line[0], line[1], line.Skip(2).ToList()))
                {
                    sum += line[0];
                }
            }


            Console.WriteLine($"Answer is {sum}");
        }

        public void Part2()
        {
            var data = File.ReadAllLines(@"data\day7.txt");

            var values = data.Select(r => r.Split(new[] { ' ', ':' }, StringSplitOptions.RemoveEmptyEntries).Select(Int64.Parse).ToList()).ToArray();

            Int64 sum = 0;

            foreach (var line in values)
            {
                if (IsMorePossible(line[0], line[1], line.Skip(2).ToList()))
                {
                    sum += line[0];
                }
            }


            Console.WriteLine($"Answer is {sum}");
        }

    }
}
