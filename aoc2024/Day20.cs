using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using aoc2024.Structs;

namespace aoc2024
{
    internal class Day20
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

                for (int r = 1; r < Board.Length - 1; r++)
                {
                    for (int c = 1; c < Board[0].Length - 1; c++)
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
                    Console.WriteLine("Dijkstra done");
                    return true;
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
                    // return true; // Let it run all paths
                }
            }
        }


        public void Part1()
        {
            var data = File.ReadAllLines(@"data\day20.txt");

            /*
            data = new[]
            {
                "###############",
                "#...#...#.....#",
                "#.#.#.#.#.###.#",
                "#S#...#.#.#...#",
                "#######.#.#.###",
                "#######.#.#...#",
                "#######.#.###.#",
                "###..E#...#...#",
                "###.#######.###",
                "#...###...#...#",
                "#.#####.#.###.#",
                "#.#...#.#.#...#",
                "#.#.#.#.#.#.###",
                "#...#...#...###",
                "###############",
            };
            */

            Board = data.Select(r => r.Select(c => c).ToArray()).ToArray();

            int startx = 0;
            int starty = 0;

            int endx = 0;
            int endy = 0;

            for (int r = 0; r < Board.Length && (starty == 0 || endy == 0); r++)
            {
                for (int c = 0; c < Board[r].Length; c++)
                {
                    if (Board[r][c] == 'S')
                    {
                        startx = c;
                        starty = r;
                        break;
                    }
                    else if (Board[r][c] == 'E')
                    {
                        endx = c;
                        endy = r;
                        break;
                    }
                }
            }

            Dijkstra(new Point(startx, starty), new Point(endx, endy));

            Console.WriteLine($"Dijkstra done with path cost {Values[endy][endx]}");

            // Check cheats
            int[] nCheats = Enumerable.Repeat(0, 10000).ToArray();

            for (int r = 1; r < Board.Length - 1; r++)
            {
                for (int c = 1; c < Board[r].Length - 1; c++)
                {
                    if (Board[r][c] == '#')
                    {
                        if (Values[r - 1][c] < Inf && Values[r + 1][c] < Inf)
                        {
                            int saves = Values[r + 1][c] - Values[r - 1][c];
                            if (saves < 0)
                            {
                                saves = -saves;
                            }
                            saves -= 2;
                            nCheats[saves]++;
                            Console.WriteLine($"Found cheat at (y, x) = ({r},{c}) with save {saves}");
                        }
                        if (Values[r][c - 1] < Inf && Values[r][c + 1] < Inf)
                        {
                            int saves = Values[r][c + 1] - Values[r][c - 1];
                            if (saves < 0)
                            {
                                saves = -saves;
                            }
                            saves -= 2;
                            nCheats[saves]++;
                            Console.WriteLine($"Found cheat at (y, x) = ({r},{c}) with save {saves}");
                        }
                    }
                }
            }

            for (int i = 0; i <  nCheats.Length; i++)
            {
                if (nCheats[i] != 0)
                {
                    Console.WriteLine($"Found {nCheats[i]} cheats that save {i} picoseconds.");
                }
            }

            Console.WriteLine($"Answer is {nCheats.Skip(100).Sum()}");

        }

        public void Part2()
        {
            var data = File.ReadAllLines(@"data\day20.txt");

            /*
            data = new[]
            {
                "###############",
                "#...#...#.....#",
                "#.#.#.#.#.###.#",
                "#S#...#.#.#...#",
                "#######.#.#.###",
                "#######.#.#...#",
                "#######.#.###.#",
                "###..E#...#...#",
                "###.#######.###",
                "#...###...#...#",
                "#.#####.#.###.#",
                "#.#...#.#.#...#",
                "#.#.#.#.#.#.###",
                "#...#...#...###",
                "###############",
            };
            */

            Board = data.Select(r => r.Select(c => c).ToArray()).ToArray();

            int startx = 0;
            int starty = 0;

            int endx = 0;
            int endy = 0;

            for (int r = 0; r < Board.Length && (starty == 0 || endy == 0); r++)
            {
                for (int c = 0; c < Board[r].Length; c++)
                {
                    if (Board[r][c] == 'S')
                    {
                        startx = c;
                        starty = r;
                        break;
                    }
                    else if (Board[r][c] == 'E')
                    {
                        endx = c;
                        endy = r;
                        break;
                    }
                }
            }

            Dijkstra(new Point(startx, starty), new Point(endx, endy));

            Console.WriteLine($"Dijkstra done with path cost {Values[endy][endx]}");

            // Check cheats
            int[] nCheats = Enumerable.Repeat(0, 15000).ToArray();

            for (int r = 1; r < Board.Length - 1; r++)
            {
                for (int c = 1; c < Board[r].Length - 1; c++)
                {
                    if (Values[r][c] < Inf)
                    {
                        Point p1 = new Point(c, r);

                        for (int rr = r; rr < Board.Length; rr++)
                        {
                            for (int cc = 1; cc < Board[rr].Length; cc++)
                            {
                                if (((rr > r) || (rr == r && cc > c)) && Values[rr][cc] < Inf)
                                {
                                    Point p2 = new Point(cc, rr);
                                    if (p1.ManhattanDist(p2) <= 20)
                                    {
                                        int saves = Values[r][c] - Values[rr][cc];
                                        if (saves < 0)
                                        {
                                            saves = -saves;
                                        }
                                        saves -= p1.ManhattanDist(p2);

                                        if (saves > 0)
                                        {
                                            nCheats[saves]++;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            for (int i = 0; i < nCheats.Length; i++)
            {
                if (nCheats[i] != 0)
                {
                    Console.WriteLine($"Found {nCheats[i]} cheats that save {i} picoseconds.");
                }
            }

            Console.WriteLine($"Answer is {nCheats.Skip(100).Sum()}");

        }

    }
}
