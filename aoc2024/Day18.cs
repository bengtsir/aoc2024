using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using aoc2024.Structs;

namespace aoc2024
{
    internal class Day18
    {
        internal bool[][] Visited;
        internal int[][] Values;
        internal char[][] Board;

        internal int Inf = 999999999;

        internal int[][] Directions = new[]
        {
            new[] { 1, 0 },
            new[] { 0, 1 },
            new[] { -1, 0 },
            new[] { 0, -1 },
        };

        internal bool Dijkstra(Point start, Point end)
        {
            Visited = Board.Select(r => r.Select(c => false).ToArray()).ToArray();
            Values = Board.Select(r => r.Select(c => Inf).ToArray()).ToArray();
            Values[start.Y][start.X] = 0;

            while (true)
            {
                var currVal = Inf;
                var selected = new Point(-1, -1);

                for (int r = 1; r < 72; r++)
                {
                    for (int c = 1; c < 72; c++)
                    {
                        if (!Visited[r][c] && Board[r][c] != '#' && Values[r][c] < currVal)
                        {
                            selected.X = c;
                            selected.Y = r;
                            currVal = Values[r][c];
                        }
                    }
                }

                if (selected.X < 0)
                {
                    Console.WriteLine("Could not find any unvisited node");
                    return false;
                }

                foreach (var d in Directions)
                {
                    var ty = selected.Y + d[1];
                    var tx = selected.X + d[0];
                    if (!Visited[ty][tx] && Board[ty][tx] != '#' && Values[ty][tx] > currVal + 1)
                    {
                        Values[ty][tx] = currVal + 1;
                    }
                }

                Visited[selected.Y][selected.X] = true;

                if (selected.Y == end.Y && selected.X == end.X)
                {
                    Console.WriteLine($"Found path with cost {Values[end.Y][end.X]}");
                    return true;
                }
            }
        }

        private int[][] Moves;

        public void Make(int i)
        {
            Board[Moves[i][1]+1][Moves[i][0]+1] = '#';
        }

        public void Retract(int i)
        {
            Board[Moves[i][1]+1][Moves[i][0]+1] = '.';
        }

        public void Part1()
        {
            var data = File.ReadAllLines(@"data\day18.txt");

            Moves = data.Select(r => r.Split(',').Select(Int32.Parse).ToArray()).ToArray();

            var origBoard = Enumerable.Repeat(0, 71).Select(v => new string(Enumerable.Repeat('.', 71).ToArray())).ToArray();

            Board = ArrayMethods.AddBorder(1, '#', origBoard).Select(r => r.Select(c => c).ToArray()).ToArray();

            var Start = new Point(1, 1);
            var End = new Point(71, 71);

            for (int i = 0; i < 1024; i++)
            {
                Make(i);
            }

            Dijkstra(Start, End);


            Console.WriteLine($"Answer is written above");
        }

        public void Part2()
        {
            var data = File.ReadAllLines(@"data\day18.txt");

            Moves = data.Select(r => r.Split(',').Select(Int32.Parse).ToArray()).ToArray();

            var origBoard = Enumerable.Repeat(0, 71).Select(v => new string(Enumerable.Repeat('.', 71).ToArray())).ToArray();

            Board = ArrayMethods.AddBorder(1, '#', origBoard).Select(r => r.Select(c => c).ToArray()).ToArray();

            var Start = new Point(1, 1);
            var End = new Point(71, 71);

            var lb = 0;
            var ub = Moves.Length;

            while (lb + 1 < ub)
            {
                for (int i = 0; i < Moves.Length; i++)
                {
                    Retract(i);
                }

                var mid = (lb + ub) / 2;

                Console.WriteLine($"Testing at {mid}");

                for (int i = 0; i < mid; i++)
                {
                    Make(i);
                }

                if (Dijkstra(Start, End))
                {
                    lb = mid;
                }
                else
                {
                    ub = mid;
                }
            }

            Console.WriteLine($"First blocking block is at {Moves[lb][0]},{Moves[lb][1]}");


            Console.WriteLine($"Answer is written above");
        }

    }
}
