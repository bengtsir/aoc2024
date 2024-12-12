using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aoc2024
{
    internal class Day12
    {
        internal int[][] Directions = new[]
        {
            new[] {-1, 0},
            new[] {0, -1},
            new[] {1, 0},
            new[] {0, 1}
        };

        internal char[][] Values;
        internal bool[][] Visited;


        internal void FindStuff(char toFind, int r, int c, ref int area, ref int circ)
        {
            Visited[r][c] = true;
            area++;

            foreach (var d in Directions)
            {
                if (Values[r + d[0]][c + d[1]] != toFind)
                {
                    circ++;
                }
                else
                {
                    if (!Visited[r+d[0]][c + d[1]])
                    {
                        FindStuff(toFind, r + d[0], c + d[1], ref area, ref circ);
                    }
                }
            }
        }

        public void Part1()
        {
            var data = File.ReadAllLines(@"data\day12.txt");

            Values = ArrayMethods.AddBorder(1, '.', data).Select(r => r.Select(c => c).ToArray()).ToArray();

            Visited = Values.Select(r => r.Select(c => c == '.').ToArray()).ToArray();

            Int64 sum = 0;

            for (var r = 0;  r < Values.Length; r++)
            {
                for (var c = 0; c < Values[r].Length; c++)
                {
                    if (!Visited[r][c])
                    {
                        char toFind = Values[r][c];

                        var area = 0;
                        var circ = 0;

                        FindStuff(toFind, r, c, ref area, ref circ);

                        sum += area * circ;
                    }
                }
            }



            Console.WriteLine($"Answer is {sum}");
        }

        internal void FindStuff2(char toFind, int r, int c, ref int area, ref int circ)
        {
            Visited[r][c] = true;
            area++;

            // tl
            if (Values[r - 1][c] != toFind && Values[r][c - 1] != toFind)
            {
                circ++;
            }
            else if (Values[r - 1][c] == toFind && Values[r][c - 1] == toFind && Values[r - 1][c - 1] != toFind)
            {
                circ++;
            }

            // tr
            if (Values[r + 1][c] != toFind && Values[r][c - 1] != toFind)
            {
                circ++;
            }
            else if (Values[r + 1][c] == toFind && Values[r][c - 1] == toFind && Values[r + 1][c - 1] != toFind)
            {
                circ++;
            }

            // bl
            if (Values[r - 1][c] != toFind && Values[r][c + 1] != toFind)
            {
                circ++;
            }
            else if (Values[r - 1][c] == toFind && Values[r][c + 1] == toFind && Values[r - 1][c + 1] != toFind)
            {
                circ++;
            }

            // br
            if (Values[r + 1][c] != toFind && Values[r][c + 1] != toFind)
            {
                circ++;
            }
            else if (Values[r + 1][c] == toFind && Values[r][c + 1] == toFind && Values[r + 1][c + 1] != toFind)
            {
                circ++;
            }

            // Flood fill
            foreach (var d in Directions)
            {
                if (Values[r + d[0]][c + d[1]] == toFind && !Visited[r + d[0]][c + d[1]])
                {
                    FindStuff2(toFind, r + d[0], c + d[1], ref area, ref circ);
                }
            }
        }


        public void Part2()
        {
            var data = File.ReadAllLines(@"data\day12.txt");

            /*
            data = new[]
            {
                "AAAAAA",
                "AAABBA",
                "AAABBA",
                "ABBAAA",
                "ABBAAA",
                "AAAAAA",
            };
            */

            Values = ArrayMethods.AddBorder(1, '.', data).Select(r => r.Select(c => c).ToArray()).ToArray();

            Visited = Values.Select(r => r.Select(c => c == '.').ToArray()).ToArray();

            Int64 sum = 0;

            for (var r = 0;  r < Values.Length; r++)
            {
                for (var c = 0; c < Values[r].Length; c++)
                {
                    if (!Visited[r][c])
                    {
                        char toFind = Values[r][c];

                        var area = 0;
                        var circ = 0;

                        FindStuff2(toFind, r, c, ref area, ref circ);

                        Console.WriteLine($"Area {toFind} with area {area} circ {circ} for a value of {area * circ}");

                        sum += area * circ;
                    }
                }
            }



            Console.WriteLine($"Answer is {sum}");
        }

    }
}
