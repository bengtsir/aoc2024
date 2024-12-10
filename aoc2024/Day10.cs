using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aoc2024
{
    internal class Day10
    {
        int[][] Mark;

        internal bool IsPart2 = false;

        internal int Test(int[][] values, int r, int c, int currVal)
        {
            int sum = 0;

            Mark[r][c] = 1;

            if (values[r][c] == 9)
            {
                if (IsPart2)
                {
                    Mark[r][c] = 0;
                }
                return 1;
            }

            if (values[r - 1][c] == currVal + 1 && Mark[r - 1][c] == 0)
            {
                sum += Test(values, r - 1, c, currVal + 1);
            }
            if (values[r + 1][c] == currVal + 1 && Mark[r + 1][c] == 0)
            {
                sum += Test(values, r + 1, c, currVal + 1);
            }
            if (values[r][c - 1] == currVal + 1 && Mark[r][c - 1] == 0)
            {
                sum += Test(values, r, c - 1, currVal + 1);
            }
            if (values[r][c + 1] == currVal + 1 && Mark[r][c + 1] == 0)
            {
                sum += Test(values, r, c + 1, currVal + 1);
            }

            if (IsPart2)
            {
                Mark[r][c] = 0;
            }

            return sum;
        }

        internal int Cardinality(int[][] values, int zeror, int zeroc)
        {
            for (int r = 0; r < values.Length; r++)
            {
                for (int c = 0; c < values[0].Length; c++)
                {
                    Mark[r][c] = 0;
                }
            }

            return Test(values, zeror, zeroc, 0);

        }

        public void Part1()
        {
            var data = ArrayMethods.AddBorder(1, (char)('0' - 1), File.ReadAllLines(@"data\day10.txt"));

            var values = data.Select(r => r.Select(c => (int)(c - '0')).ToArray()).ToArray();

            Mark = data.Select(r => r.Select(c => (int) 0).ToArray()).ToArray();

            int sum = 0;

            for (int r = 0; r < values.Length; r++)
            {
                for (int c = 0; c < values[r].Length; c++)
                {
                    if (values[r][c] == 0)
                    {
                        sum += Cardinality(values, r, c);
                    }
                }
            }

            Console.WriteLine($"Answer is {sum}");
        }

        public void Part2()
        {
            IsPart2 = true;

            Part1();
        }

    }
}
