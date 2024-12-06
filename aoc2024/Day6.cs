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
        int[][] offsets = new[]{
                new[]{-1, 0 }, // Up
                new[]{0, 1}, // Eight
                new[]{1, 0}, // Down
                new[]{0, -1}, // Left
            };

        public void Part1()
        {
            var data = File.ReadAllLines(@"data\day6.txt");

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

        public char Mark(char c, int dir)
        {
            char mask = (char) (1 << dir);
            if (c < 0x40 || c > 0x4F)
            {
                c = (char) 0x40;
            }
            c |= mask;
            return c;
        }

        public bool IsMarked(char c, int dir)
        {
            int mask = 1 << dir;
            if (c >= 0x40 && c <= 0x4F)
            {
                return (c & mask) != 0;
            }
            return false;
        }

        public bool WillLoop(char[][] board)
        {
            int r = 0;
            int c = 0;
            int dir = 0;

            for (r = 0; r < board.Length; r++)
            {
                c = Array.IndexOf(board[r], '^');
                if (c > 0)
                {
                    break;
                }
            }

            while (board[r][c] != 'X')
            {
                if (IsMarked(board[r][c], dir))
                {
                    return true;
                }
                board[r][c] = Mark(board[r][c], dir);

                r += offsets[dir][0];
                c += offsets[dir][1];

                if (board[r][c] == '#')
                {
                    r -= offsets[dir][0];
                    c -= offsets[dir][1];

                    dir = (dir + 1) % 4;
                }
            }

            return false;
        }

        public void Part2()
        {
            var data = File.ReadAllLines(@"data\day6.txt");

            var values = ArrayMethods.AddBorder(1, 'X', data).Select(vv => vv.ToArray()).ToArray();

            int sum = 0;
            for (int r = 0; r < values.Length; r++)
            {
                for (int c = 0; c < values[r].Length; c++)
                {
                    if (values[r][c] == '.')
                    {
                        var testarr = values.Select(vv => vv.ToArray()).ToArray();
                        testarr[r][c] = '#';
                        if (WillLoop(testarr))
                        {
                            sum++;
                        }
                    }
                }
            }


            Console.WriteLine($"Answer is {sum}");
        }

    }
}
