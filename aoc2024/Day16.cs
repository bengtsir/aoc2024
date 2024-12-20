using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using aoc2024.Structs;

namespace aoc2024
{
    internal class Day16State
    {
        public int X;
        public int Y;
        public int Dir;
    }

    internal class Day16
    {
        private char[][] Board;

        bool IsPart2 = false;

        internal int[][] Directions = new []
        {
            new[] {-1, 0},
            new[] {0, 1},
            new[] {1, 0},
            new[] {0, -1},
        };

        private bool[][][] Visited;
        private long[][][] Values;

        Int64 Inf = 999_999_999_999;

        internal bool Dijkstra(Point start, Point end)
        {
            Visited = Board.Select(r => r.Select(c => new bool[] {false, false, false, false}).ToArray()).ToArray();
            Values = Board.Select(r => r.Select(c => new long[] { Inf, Inf, Inf, Inf }).ToArray()).ToArray();
            Values[start.Y][start.X][1] = 0;

            Day16State startState = new Day16State() { X = start.X, Y = start.Y, Dir = 1 };

            var totalNodes = Visited.Length * Visited[0].Length * 4;
            var visitedNodes = 0;

            List<Day16State> toVisit = new List<Day16State>() { startState };

            while (true)
            {
                var currVal = Inf;
                Day16State selected = null;

                foreach (var st in toVisit)
                {
                    if (Values[st.Y][st.X][st.Dir] < currVal)
                    {
                        selected = st;
                        currVal = Values[st.Y][st.X][st.Dir];
                    }
                }

                /*
                for (int r = 1; r < Board.Length - 1; r++)
                {
                    for (int c = 1; c < Board[r].Length - 1; c++)
                    {
                        for (int d = 0; d < 4; d++)
                        {
                            if (!Visited[r][c][d] && Board[r][c] != '#' && Values[r][c][d] < currVal)
                            {
                                selected.X = c;
                                selected.Y = r;
                                selected.Dir = d;
                                currVal = Values[r][c][d];
                            }
                        }
                    }
                }
                */

                if ((visitedNodes % 10000) == 0)
                {
                    Console.WriteLine($"Visited: {visitedNodes} of {totalNodes}");
                }


                if (selected == null)
                {
                    Console.WriteLine("Could not find any unvisited node");
                    return false;
                }

                var ty = selected.Y + Directions[selected.Dir][0];
                var tx = selected.X + Directions[selected.Dir][1];

                // Walk
                if (!Visited[ty][tx][selected.Dir] && Board[ty][tx] != '#' && Values[ty][tx][selected.Dir] > currVal + 1)
                {
                    Values[ty][tx][selected.Dir] = currVal + 1;
                    if (!toVisit.Any(st => st.X == tx && st.Y == ty && st.Dir == selected.Dir))
                    {
                        toVisit.Add(new Day16State() { X = tx, Y = ty, Dir = selected.Dir });
                    }
                }

                // Turn
                tx = selected.X;
                ty = selected.Y;

                var td = (selected.Dir + 1) % 4;

                if (!Visited[ty][tx][td] && Board[ty][tx] != '#' && Values[ty][tx][td] > currVal + 1000)
                {
                    Values[ty][tx][td] = currVal + 1000;
                    if (!toVisit.Any(st => st.X == tx && st.Y == ty && st.Dir == td))
                    {
                        toVisit.Add(new Day16State() { X = tx, Y = ty, Dir = td });
                    }
                }

                td = (selected.Dir + 3) % 4;

                if (!Visited[ty][tx][td] && Board[ty][tx] != '#' && Values[ty][tx][td] > currVal + 1000)
                {
                    Values[ty][tx][td] = currVal + 1000;
                    if (!toVisit.Any(st => st.X == tx && st.Y == ty && st.Dir == td))
                    {
                        toVisit.Add(new Day16State() { X = tx, Y = ty, Dir = td });
                    }
                }

                Visited[selected.Y][selected.X][selected.Dir] = true;
                toVisit.Remove(selected);
                visitedNodes++;

                if (selected.Y == end.Y && selected.X == end.X)
                {
                    Console.WriteLine($"Found path with cost {Values[end.Y][end.X].Min()}");
                    if (!IsPart2)
                    {
                        return true;
                    }
                }
            }
        }


        public void Part1()
        {
            var data = File.ReadAllLines(@"data\day16.txt");

            /*
            data = new[]
            {
                "###############",
                "#.......#....E#",
                "#.#.###.#.###.#",
                "#.....#.#...#.#",
                "#.###.#####.#.#",
                "#.#.#.......#.#",
                "#.#.#####.###.#",
                "#...........#.#",
                "###.#.#####.#.#",
                "#...#.....#.#.#",
                "#.#.#.###.#.#.#",
                "#.....#...#.#.#",
                "#.###.#.#.#.#.#",
                "#S..#.....#...#",
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



            Console.WriteLine();
            Console.WriteLine($"Answer is printed above");
        }

        internal void Traverse(Day16State current, Point end)
        {
            int cx = current.X;
            int cy = current.Y;
            int cd = current.Dir;

            if (cx == end.X && cy == end.Y)
            {
                Board[cy][cx] = 'O';
                return;
            }

            if (Visited[cy][cx][cd])
            {
                return;
            }

            Board[cy][cx] = 'O';
            Visited[cy][cx][cd] = true;

            var currVal = Values[cy][cx][cd];

            int ty = cy - Directions[cd][0];
            int tx = cx - Directions[cd][1];

            int branches = 0;

            if (Values[ty][tx][cd] + 1 == currVal)
            {
                Traverse(new Day16State() { Y = ty, X = tx, Dir = cd }, end);
                branches++;
            }
            if (Values[cy][cx][(cd + 1) % 4] + 1000 == currVal)
            {
                Traverse(new Day16State() { Y = cy, X = cx, Dir = (cd + 1) % 4 }, end);
                branches++;
            }
            if (Values[cy][cx][(cd + 3) % 4] + 1000 == currVal)
            {
                Traverse(new Day16State() { Y = cy, X = cx, Dir = (cd + 3) % 4 }, end);
                branches++;
            }
            if (branches > 1)
            {
                Console.WriteLine($"Branch at Y = {cy} X = {cx}");
            }
        }

        internal void MarkAllPaths(Point start, Point end)
        {
            Day16State current = new Day16State() { X = start.X, Y = start.Y };

            for (int r = 0; r < Visited.Length; r++)
            {
                for (int c = 0; c < Visited[r].Length; c++)
                {
                    for (int d = 0; d < 4; d++)
                    {
                        Visited[r][c][d] = false;
                    }
                }
            }

            var currVal = Inf;

            for (int dir = 0; dir < 4; dir++)
            {
                if (Values[start.Y][start.X][dir] < currVal)
                {
                    currVal = Values[start.Y][start.X][dir];
                }
            }

            // Check all with lowest score
            for (int dir = 0; dir < 4; dir++)
            {
                if (Values[start.Y][start.X][dir] == currVal)
                {
                    current.Dir = dir;
                    Traverse(current, end);
                }
            }

        }

        public void Part2()
        {
            var data = File.ReadAllLines(@"data\day16.txt");

            /*
            data = new[]
            {
                "###############",
                "#.......#....E#",
                "#.#.###.#.###.#",
                "#.....#.#...#.#",
                "#.###.#####.#.#",
                "#.#.#.......#.#",
                "#.#.#####.###.#",
                "#...........#.#",
                "###.#.#####.#.#",
                "#...#.....#.#.#",
                "#.#.#.###.#.#.#",
                "#.....#...#.#.#",
                "#.###.#.#.#.#.#",
                "#S..#.....#...#",
                "###############",
            };
            */

            Board = data.Select(r => r.Select(c => c).ToArray()).ToArray();

            int startx = 0;
            int starty = 0;

            int endx = 0;
            int endy = 0;

            IsPart2 = true;

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

            MarkAllPaths(new Point(endx, endy), new Point(startx, starty));

            int sum = Board.Sum(r => r.Count(c => c == 'O'));

            foreach (var line in Board)
            {
                Console.WriteLine(new string(line));
            }

            Console.WriteLine();
            Console.WriteLine($"Answer is {sum}");
        }

    }
}
