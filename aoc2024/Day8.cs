using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aoc2024
{
    internal class Day8
    {
        public void Part1()
        {
            var data = File.ReadAllLines(@"data\day8.txt");

            var values = data.Select(r => r.Select(c => c).ToArray()).ToArray();
            var mark = data.Select(r => r.Select(c => '.').ToArray()).ToArray();

            int sum = 0;

            for (int r = 0; r < values.Length; r++)
            {
                for (int c = 0; c < values[r].Length; c++)
                {
                    if (values[r][c] != '.')
                    {
                        for (int rr = 0; rr < values.Length; rr++)
                        {
                            for (int cc = 0; cc < values[rr].Length; cc++)
                            {
                                if (rr != r && cc != c && values[rr][cc] == values[r][c])
                                {
                                    var rdiff = rr - r;
                                    var cdiff = cc - c;

                                    if (r - rdiff >= 0 &&
                                        r - rdiff < values.Length &&
                                        c - cdiff >= 0 &&
                                        c - cdiff < values[rr].Length)
                                    {
                                        mark[r - rdiff][c - cdiff] = '#';
                                    }
                                }
                            }
                        }
                    }
                }
            }


            Console.WriteLine($"Answer is {mark.Sum(r => r.Count(c => c == '#'))}");
        }

        public void Part2()
        {
            var data = File.ReadAllLines(@"data\day8.txt");

            var values = data.Select(r => r.Select(c => c).ToArray()).ToArray();
            var mark = data.Select(r => r.Select(c => '.').ToArray()).ToArray();

            int sum = 0;

            for (int r = 0; r < values.Length; r++)
            {
                for (int c = 0; c < values[r].Length; c++)
                {
                    if (values[r][c] != '.')
                    {
                        for (int rr = 0; rr < values.Length; rr++)
                        {
                            for (int cc = 0; cc < values[rr].Length; cc++)
                            {
                                if (rr != r && cc != c && values[rr][cc] == values[r][c])
                                {
                                    mark[r][c] = '#';
                                    mark[rr][cc] = '#';

                                    var rdiff = rr - r;
                                    var cdiff = cc - c;

                                    var rt = r - rdiff;
                                    var ct = c - cdiff;

                                    while (rt >= 0 &&
                                        rt < values.Length &&
                                        ct >= 0 &&
                                        ct < values[rr].Length)
                                    {
                                        mark[rt][ct] = '#';
                                        rt -= rdiff;
                                        ct -= cdiff;
                                    }
                                }
                            }
                        }
                    }
                }
            }


            Console.WriteLine($"Answer is {mark.Sum(r => r.Count(c => c == '#'))}");
        }

    }
}
