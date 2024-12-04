using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aoc2024
{
    internal class Day4
    {
        public int CountXmases(char[][] values, int row, int column)
        {
            int count = 0;

            for (int rofs = -1; rofs <= 1; rofs++)
            {
                for (int cofs = -1; cofs <= 1; cofs++)
                {
                    // Don't need to remove the (0, 0) case

                    if (values[row         ][column         ] == 'X' &&
                        values[row +   rofs][column +   cofs] == 'M' &&
                        values[row + 2*rofs][column + 2*cofs] == 'A' &&
                        values[row + 3*rofs][column + 3*cofs] == 'S')
                    {
                        count++;
                    }

                }
            }
            return count;
        }


        public int CountCrossMases(char[][] values, int row, int col)
        {
            int count = 0;

            if (values[row][col] == 'A')
            {
                if ((values[row - 1][col - 1] == 'M' && values[row + 1][col + 1] == 'S') ||
                    (values[row - 1][col - 1] == 'S' && values[row + 1][col + 1] == 'M'))
                {
                    if ((values[row - 1][col + 1] == 'M' && values[row + 1][col - 1] == 'S') ||
                        (values[row - 1][col + 1] == 'S' && values[row + 1][col - 1] == 'M'))
                    {
                        count++;
                    }
                }
            }

            return count;
        }


        public void Part1()
        {
            var data = File.ReadAllLines(@"data\day4.txt");

            var values = ArrayMethods.AddBorder(3, '.', data).Select(r => r.Select(c => c).ToArray()).ToArray();

            Int64 sum = 0;

            for (int i = 0; i < values.Length; i++)
            {
                for (int j = 0; j < values[i].Length; j++)
                {
                    sum += CountXmases(values, i, j);
                }
            }

            Console.WriteLine($"Answer is {sum}");
        }

        public void Part2()
        {
            var data = File.ReadAllLines(@"data\day4.txt");

            var values = ArrayMethods.AddBorder(3, '.', data).Select(r => r.Select(c => c).ToArray()).ToArray();

            Int64 sum = 0;

            for (int i = 0; i < values.Length; i++)
            {
                for (int j = 0; j < values[i].Length; j++)
                {
                    sum += CountCrossMases(values, i, j);
                }
            }

            Console.WriteLine($"Answer is {sum}");
        }

    }
}
