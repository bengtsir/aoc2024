using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aoc2024
{
    internal class Day6
    {
        public void Part1()
        {
            var data = File.ReadAllLines(@"data\day6.txt");

            var offsets = new[]{
                new[]{-1, 0 }, // Up
                new[]{0, 1}, // Eight
                new[]{1, 0}, // Down
                new[]{0, -1}, // Left
            };

            var values = ArrayMethods.AddBorder(1, 'X', data).Select(vv => vv.ToArray()).ToArray();

            int r = 0;
            int c = 0;
            int dir = 0;

            for (r = 0; r < values.Length; r++)
            {
                c = Array.IndexOf(values[r], '^');
                if (c > 0)
                {
                    break;
                }
            }

            while (values[r][c] != 'X')
            {
                values[r][c] = 'V'; // Visited
                r += offsets[dir][0];
                c += offsets[dir][1];

                if (values[r][c] == '#')
                {
                    r -= offsets[dir][0];
                    c -= offsets[dir][1];

                    dir = (dir + 1) % 4;
                }
            }


            Console.WriteLine($"Answer is {values.Sum(line => line.Count(cc => cc == 'V'))}");
        }

        public bool WillLoop(Array<Array<char>> board)
        {
            int r = 0;
            int c = 0;
            int dir = 0;

            for (r = 0; r < values.Length; r++)
            {
                c = Array.IndexOf(values[r], '^');
                if (c > 0)
                {
                    break;
                }
            }

            while (board[r][c] != 'X')
            {
                if (board[r][c] >= '0' && board[r][c] < '9')
                {
                    var marks = board[r][c] - '0';
                }
                values[r][c] = 'V'; // Visited
                r += offsets[dir][0];
                c += offsets[dir][1];

                if (values[r][c] == '#')
                {
                    r -= offsets[dir][0];
                    c -= offsets[dir][1];

                    dir = (dir + 1) % 4;
                }
            }

        }

        public void Part2()
        {
            var data = File.ReadAllLines(@"data\day6.txt");

            var offsets = new[]{
                new[]{-1, 0 }, // Up
                new[]{0, 1}, // Eight
                new[]{1, 0}, // Down
                new[]{0, -1}, // Left
            };

            var values = ArrayMethods.AddBorder(1, 'X', data).Select(vv => vv.ToArray()).ToArray();



            Console.WriteLine($"Answer is {values.Sum(line => line.Count(cc => cc == 'V'))}");
        }

    }
}
