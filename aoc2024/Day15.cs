using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aoc2024
{
    internal class Day15
    {
        internal char[][] Grid;
        internal string Moves;

        Dictionary<char, int> Xdiff = new Dictionary<char, int>
        {
            {'>', 1},
            {'<', -1},
            {'^', 0},
            {'v', 0}
        };

        Dictionary<char, int> Ydiff = new Dictionary<char, int>
        {
            {'>', 0},
            {'<', 0},
            {'^', -1},
            {'v', 1}
        };

        internal bool TryPush(int x, int y, char move)
        {
            if (Grid[y][x] == '.')
            {
                return true;
            }
            else if (Grid[y][x] == '#')
            {
                return false;
            }

            var newx = x + Xdiff[move];
            var newy = y + Ydiff[move];

            if (TryPush(newx, newy, move))
            {
                Grid[newy][newx] = Grid[y][x];
                Grid[y][x] = '.';
                return true;
            }

            return false;
        }

        internal void DoPush2(int x, int y, char move)
        {
            if (Grid[y][x] == '#')
            {
                return;
            }
            else if (Grid[y][x] == '.')
            {
                return;
            }

            var newx = x + Xdiff[move];
            var newy = y + Ydiff[move];

            DoPush2(newx, newy, move);
            Grid[newy][newx] = Grid[y][x];
            Grid[y][x] = '.';

            if (Grid[newy][newx] == '[')
            {
                DoPush2(x+1, y, move);
            }
            else if (Grid[newy][newx] == ']')
            {
                DoPush2(x - 1, y, move);
            }
        }

        internal bool TryPush2(int x, int y, char move, bool first = false)
        {
            if (Grid[y][x] == '.')
            {
                return true;
            }
            else if (Grid[y][x] == '#')
            {
                return false;
            }

            var newx = x + Xdiff[move];
            var newy = y + Ydiff[move];

            if (newy == y && TryPush2(newx, newy, move))
            {
                Grid[newy][newx] = Grid[y][x];
                Grid[y][x] = '.';
                return true;
            }
            else if (newy != y)
            {
                if (Grid[y][x] == '@')
                {
                    if (TryPush2(newx, newy, move))
                    {
                        if (first)
                        {
                            DoPush2(x, y, move);
                        }
                        return true;
                    }

                }
                else if (Grid[y][x] == '[')
                {
                    if (TryPush2(newx, newy, move) && TryPush2(newx + 1, newy, move))
                    {
                        if (first)
                        {
                            DoPush2(x, y, move);
                            DoPush2(x + 1, y, move);
                        }
                        return true;
                    }
                }
                else if (Grid[y][x] == ']')
                {
                    if (TryPush2(newx, newy, move) && TryPush2(newx - 1, newy, move))
                    {
                        if (first)
                        {
                            DoPush2(x, y, move);
                            DoPush2(x - 1, y, move);
                        }
                        return true;
                    }
                }
            }

            return false;
        }

        public void Part1()
        {
            var data = File.ReadAllLines(@"data\day15.txt");

            /*
            data = new string[]
            {
                "##########",
                "#..O..O.O#",
                "#......O.#",
                "#.OO..O.O#",
                "#..O@..O.#",
                "#O#..O...#",
                "#O..O..O.#",
                "#.OO.O.OO#",
                "#....O...#",
                "##########",
                "",
                "<vv>^<v^>v>^vv^v>v<>v^v<v<^vv<<<^><<><>>v<vvv<>^v^>^<<<><<v<<<v^vv^v>^",
                "vvv<<^>^v^^><<>>><>^<<><^vv^^<>vvv<>><^^v>^>vv<>v<<<<v<^v>^<^^>>>^<v<v",
                "><>vv>v^v^<>><>>>><^^>vv>v<^^^>>v^v^<^^>v^^>v^<^v>v<>>v^v^<v>v^^<^^vv<",
                "<<v<^>>^^^^>>>v^<>vvv^><v<<<>^^^vv^<vvv>^>v<^^^^v<>^>vvvv><>>v^<<^^^^^",
                "^><^><>>><>^^<<^^v>>><^<v>^<vv>>v>>>^v><>^v><<<<v>>v<v<v>vvv>^<><<>^><",
                "^>><>^v<><^vvv<^^<><v<<<<<><^v<<<><<<^^<v<^^^><^>>^<v^><<<^>>^v<v^v<v^",
                ">^>>^v>vv>^<<^v<>><<><<v<<v><>v<^vv<<<>^^v^>^^>>><<^v>>v^v><^^>>^<>vv^",
                "<><^^>^^^<><vvvvv^v<v<<>^v<v>v<<^><<><<><<<^^<<<^<<>><<><^^^>^^<>^>v<>",
                "^^>vv<^v^v<vv>^<><v<^v>^^^>>>^^vvv^>vvv<>>>^<^>>>>>^<<^v>^vvv<>^<><<v>",
                "v^^>>><<^^<>>^v^<v^vv<>v^<<>^<^v^v><^<<<><<^<v><v<>vv>>v><v^<vv<>v^<<^",
            };
            */

            var gridSizeY = Array.IndexOf(data, "");
            var gridSizeX = data[0].Length;

            Grid = data.Take(gridSizeY).Select(r => r.Select(c => c).ToArray()).ToArray();
            Moves = string.Join("", data.Skip(gridSizeY + 1));

            int currX = 0;
            int currY = 0;

            for (var row = 1; row < Grid.Length - 1; row++)
            {
                currX = Array.IndexOf(Grid[row], '@');
                if (currX > 0)
                {
                    currY = row;
                    break;
                }
            }

            foreach (var move in Moves)
            {
                if (TryPush(currX, currY, move))
                {
                    currX += Xdiff[move];
                    currY += Ydiff[move];
                }
            }

            foreach (var row in Grid)
            {
                Console.WriteLine(row);
            }

            Int64 sum = 0;
            for (int r = 0; r < Grid.Length; r++)
            {
                for (int c = 0; c < Grid[r].Length; c++)
                {
                    if (Grid[r][c] == 'O')
                    {
                        sum += 100*r + c;
                    }
                }
            }


            Console.WriteLine($"Answer is {sum}");
        }

        public void Part2()
        {
            var data = File.ReadAllLines(@"data\day15.txt");

            var cmap = new Dictionary<char, char[]>()
            {
                { '#', new[] { '#', '#' } },
                { '.', new[] { '.', '.' } },
                { '@', new[] { '@', '.' } },
                { 'O', new[] { '[', ']' } },
            };

            /*
            data = new string[]
            {
                "##########",
                "#..O..O.O#",
                "#......O.#",
                "#.OO..O.O#",
                "#..O@..O.#",
                "#O#..O...#",
                "#O..O..O.#",
                "#.OO.O.OO#",
                "#....O...#",
                "##########",
                "",
                "<vv>^<v^>v>^vv^v>v<>v^v<v<^vv<<<^><<><>>v<vvv<>^v^>^<<<><<v<<<v^vv^v>^",
                "vvv<<^>^v^^><<>>><>^<<><^vv^^<>vvv<>><^^v>^>vv<>v<<<<v<^v>^<^^>>>^<v<v",
                "><>vv>v^v^<>><>>>><^^>vv>v<^^^>>v^v^<^^>v^^>v^<^v>v<>>v^v^<v>v^^<^^vv<",
                "<<v<^>>^^^^>>>v^<>vvv^><v<<<>^^^vv^<vvv>^>v<^^^^v<>^>vvvv><>>v^<<^^^^^",
                "^><^><>>><>^^<<^^v>>><^<v>^<vv>>v>>>^v><>^v><<<<v>>v<v<v>vvv>^<><<>^><",
                "^>><>^v<><^vvv<^^<><v<<<<<><^v<<<><<<^^<v<^^^><^>>^<v^><<<^>>^v<v^v<v^",
                ">^>>^v>vv>^<<^v<>><<><<v<<v><>v<^vv<<<>^^v^>^^>>><<^v>>v^v><^^>>^<>vv^",
                "<><^^>^^^<><vvvvv^v<v<<>^v<v>v<<^><<><<><<<^^<<<^<<>><<><^^^>^^<>^>v<>",
                "^^>vv<^v^v<vv>^<><v<^v>^^^>>>^^vvv^>vvv<>>>^<^>>>>>^<<^v>^vvv<>^<><<v>",
                "v^^>>><<^^<>>^v^<v^vv<>v^<<>^<^v^v><^<<<><<^<v><v<>vv>>v><v^<vv<>v^<<^",
            };

            
            data = new string[]
            {
                "#######",
                "#...#.#",
                "#.....#",
                "#..OO@#",
                "#..O..#",
                "#.....#",
                "#######",
                "",
                "<vv<<^^<<^^",
            };
            */
            /*
            data = new string[]
            {
                "#######",
                "#...#.#",
                "#.OOO@#",
                "#..OO.#",
                "#..O..#",
                "#.....#",
                "#######",
                "",
                "v<v<v<^^>vv",
            };
            */

            var gridSizeY = Array.IndexOf(data, "");
            var origGridSizeX = data[0].Length;

            Grid = data.Take(gridSizeY).Select(r => r.SelectMany(c => cmap[c]).ToArray()).ToArray();
            Moves = string.Join("", data.Skip(gridSizeY + 1));

            var gridSizeX = Grid[0].Length;
            

            int currX = 0;
            int currY = 0;

            for (var row = 1; row < Grid.Length - 1; row++)
            {
                currX = Array.IndexOf(Grid[row], '@');
                if (currX > 0)
                {
                    currY = row;
                    break;
                }
            }

            foreach (var move in Moves)
            {
                if (TryPush2(currX, currY, move, true))
                {
                    currX += Xdiff[move];
                    currY += Ydiff[move];
                }

                /*
                Console.WriteLine($"Move: {move}");
                foreach (var row in Grid)
                {
                    Console.WriteLine(row);
                }
                */
            }

            foreach (var row in Grid)
            {
                Console.WriteLine(row);
            }

            Int64 sum = 0;
            for (int r = 0; r < Grid.Length; r++)
            {
                for (int c = 0; c < Grid[r].Length; c++)
                {
                    if (Grid[r][c] == '[')
                    {
                        sum += 100 * r + c;
                    }
                }
            }


            Console.WriteLine($"Answer is {sum}");
        }

    }
}
