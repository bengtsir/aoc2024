using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace aoc2024
{
    internal class Day14
    {
        public void Part1()
        {
            var data = File.ReadAllLines(@"data\day14.txt");

            var r = new Regex(@"p=([\d]+),([\d]+) v=([-\d]+),([-\d]+)");

            int[][] res = new[]
            {
                new[] { 0, 0 },
                new[] { 0, 0 },
            };

            var values = data.Select(row => r.Match(row)).ToArray();

            foreach (var m in values)
            {
                var row = new[]
                {
                    0, 0, 0, 0
                };

                for (int i = 1; i <= 4; i++)
                {
                    row[i - 1] = int.Parse(m.Groups[i].Value);
                }
                var x = (row[0] + row[2] * 100);
                var y = (row[1] + row[3] * 100);

                if (x < 0)
                {
                    x += ((-x / 101)+1) * 101;
                }
                x %= 101;

                if (y < 0)
                {
                    y += ((-y / 103) + 1) * 103;
                }
                y %= 103;

                if (x < 50)
                {
                    if (y < 51)
                    {
                        res[0][0]++;
                    }
                    else if (y > 51)
                    {
                        res[1][0]++;
                    }
                }
                else if (x > 50)
                {
                    if (y < 51)
                    {
                        res[0][1]++;
                    }
                    else if (y > 51)
                    {
                        res[1][1]++;
                    }
                }
            }


            Console.WriteLine($"Answer is {res[0][0] * res[0][1] * res[1][0] * res[1][1]}");
        }

        public void Part2()
        {
            var data = File.ReadAllLines(@"data\day14.txt");

            var r = new Regex(@"p=([\d]+),([\d]+) v=([-\d]+),([-\d]+)");

            var values = data.Select(row => r.Match(row)).ToArray();

            for (int iter = 0; iter < 1000000; iter++)
            {
                var board = new char[103][];
                for (int i = 0; i < 103; i++)
                {
                    board[i] = Enumerable.Repeat('.', 101).ToArray();
                }

                foreach (var m in values)
                {
                    var row = new[]
                    {
                        0, 0, 0, 0
                    };

                    for (int i = 1; i <= 4; i++)
                    {
                        row[i - 1] = int.Parse(m.Groups[i].Value);
                    }
                    var x = (row[0] + row[2] * iter);
                    var y = (row[1] + row[3] * iter);

                    if (x < 0)
                    {
                        x += ((-x / 101) + 1) * 101;
                    }
                    x %= 101;

                    if (y < 0)
                    {
                        y += ((-y / 103) + 1) * 103;
                    }
                    y %= 103;

                    board[y][x] = '#';
                }

                if (board.Any(s => new string(s).Contains("######")))
                {

                    Console.WriteLine();
                    Console.WriteLine($"--- iter {iter} ---");
                    foreach (var line in board)
                    {
                        Console.WriteLine(new string(line));
                    }
                    Console.WriteLine($"--- iter {iter} ---");
                    Console.ReadLine();
                }
            }


        }

    }
}
