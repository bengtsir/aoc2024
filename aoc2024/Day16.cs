using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aoc2024
{
    internal class Day16
    {
        private char[][] Board;

        internal int[][] Directions = new []
        {
            new[] {-1, 0},
            new[] {0, 1},
            new[] {1, 0},
            new[] {0, -1},
        };

        private bool[][][] Mark;
        private long[][][] Values;

        public void Part1()
        {
            var data = File.ReadAllLines(@"data\day16.txt");

            Int64 inf = 999_999_999_999;

            Board = data.Select(r => r.Select(c => c).ToArray()).ToArray();
            Mark = data.Select(r => r.Select(c => new bool[]{false, false, false, false}).ToArray()).ToArray();
            Values = data.Select(r => r.Select(c => new long[] { inf, inf, inf, inf }).ToArray()).ToArray();

            int startx = 0;
            int starty = 0;
            int startdir = 1;

            for (int r = 0; r < Board.Length && starty == 0; r++)
            {
                for (int c = 0; c < Board[r].Length; c++)
                {
                    if (Board[r][c] == 'S')
                    {
                        startx = c;
                        starty = r;
                        Values[r][c][startdir] = 0;
                        break;
                    }
                }
            }



            /*
            var values = data.Select(r => r.Length == 0 ? -1 : Int32.Parse(r)).ToArray();

            var values = data.Select(r => r.Select(c => (int)(c - 'a')).ToArray()).ToArray();

            var values = data.Select(r => Int64.Parse(r)).ToArray();
            */


            Console.WriteLine();
            Console.WriteLine($"Answer is printed above");
        }

        public void Part2()
        {
            var data = File.ReadAllLines(@"data\day16.txt");

            List<int> cals = new List<int>();

            var values = data.Select(r => r.Split(' ')).ToArray();




            Console.WriteLine($"Answer is {values.Count()}");
        }

    }
}
