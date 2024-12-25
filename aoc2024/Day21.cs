using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aoc2024
{
    internal class Day21
    {
        internal char[][] BigLayout = new[]
        {
            new[] { '7', '8', '9' },
            new[] { '4', '5', '6' },
            new[] { '1', '2', '3' },
            new[] { ' ', '0', 'A' },
        };

        internal char[][] SmallLayout = new[]
        {
            new[] { ' ', '^', 'A' },
            new[] { '<', 'v', '>' },
        };

        internal Dictionary<string, string[]> BigMoves;// = new Dictionary<string, string>();

        internal Dictionary<string, string[]> SmallMoves;// = new Dictionary<string, string>();


        internal void BuildDictionaries()
        {
            for (int r = 0; r < BigLayout.Length; r++)
            {
                for (int c = 0; c < BigLayout[0].Length; c++)
                {
                    for (int rr = 0; rr < BigLayout.Length; rr++)
                    {
                        for (int cc = 0; cc < BigLayout[0].Length; cc++)
                        {

                            if (BigLayout[r][c] != ' ' && BigLayout[rr][cc] != ' ')
                            {
                                List<char> moves = new List<char>();

                                if (cc > c)
                                {
                                    var mm = new List<string>();

                                    moves.AddRange(Enumerable.Repeat('>', cc - c));
                                    if (rr > r)
                                    {
                                        moves.AddRange(Enumerable.Repeat('v', rr - r));
                                    }
                                    else if (rr < r)
                                    {
                                        moves.AddRange(Enumerable.Repeat('^', r - rr));
                                    }
                                    moves.Add('A');

                                    mm.Add(new string(moves.ToArray()));

                                    if (c >= 1)
                                    {
                                        moves.Clear();
                                        if (rr > r)
                                        {
                                            moves.AddRange(Enumerable.Repeat('v', rr - r));
                                        }
                                        else if (rr < r)
                                        {
                                            moves.AddRange(Enumerable.Repeat('^', r - rr));
                                        }
                                        moves.AddRange(Enumerable.Repeat('>', cc - c));
                                        moves.Add('A');

                                        mm.Add(new string(moves.ToArray()));
                                    }

                                    BigMoves[new string(new[] { BigLayout[r][c], BigLayout[rr][cc] })] = mm.ToArray();
                                }
                                else if (cc <= c)
                                {
                                    var mm = new List<string>();

                                    if (rr < r)
                                    {
                                        moves.AddRange(Enumerable.Repeat('^', r - rr));
                                    }
                                    else if (rr > r)
                                    {
                                        moves.AddRange(Enumerable.Repeat('v', rr - r));
                                    }

                                    moves.AddRange(Enumerable.Repeat('<', c - cc));

                                    moves.Add('A');
                                    mm.Add(new string(moves.ToArray()));

                                    if (cc >= 1 || cc == 0 && r < 4)
                                    {
                                        moves.AddRange(Enumerable.Repeat('<', c - cc));
                                        if (rr < r)
                                        {
                                            moves.AddRange(Enumerable.Repeat('^', r - rr));
                                        }
                                        else if (rr > r)
                                        {
                                            moves.AddRange(Enumerable.Repeat('v', rr - r));
                                        }
                                        moves.Add('A');
                                        mm.Add(new string(moves.ToArray()));

                                        BigMoves[new string(new[] { BigLayout[r][c], BigLayout[rr][cc] })] = mm.ToArray();
                                    }
                                }
                                else
                                {
                                    moves.Add('A');

                                    BigMoves[new string(new[] { BigLayout[r][c], BigLayout[rr][cc] })] = new[] { new string(moves.ToArray()) };
                                }
                            }
                        }
                    }
                }
            }

            for (int r = 0; r < SmallLayout.Length; r++)
            {
                for (int c = 0; c < SmallLayout[0].Length; c++)
                {
                    for (int rr = 0; rr < SmallLayout.Length; rr++)
                    {
                        for (int cc = 0; cc < SmallLayout[0].Length; cc++)
                        {

                            if (SmallLayout[r][c] != ' ' && SmallLayout[rr][cc] != ' ')
                            {
                                List<char> moves = new List<char>();

                                if (cc > c)
                                {
                                    moves.AddRange(Enumerable.Repeat('>', cc - c));
                                    if (rr > r)
                                    {
                                        moves.AddRange(Enumerable.Repeat('v', rr - r));
                                    }
                                    else if (rr < r)
                                    {
                                        moves.AddRange(Enumerable.Repeat('^', r - rr));
                                    }
                                }
                                else if (cc <= c)
                                {
                                    if (rr < r)
                                    {
                                        moves.AddRange(Enumerable.Repeat('^', r - rr));
                                    }
                                    else if (rr > r)
                                    {
                                        moves.AddRange(Enumerable.Repeat('v', rr - r));
                                    }

                                    moves.AddRange(Enumerable.Repeat('<', c - cc));
                                }

                                moves.Add('A');

                                SmallMoves[new string(new[] { SmallLayout[r][c], SmallLayout[rr][cc] })] = new[] { new string(moves.ToArray()) };
                            }
                        }
                    }
                }
            }

        }

        public void Part1()
        {
            var data = File.ReadAllLines(@"data\day21.txt");

            data = new[]
            {
                "029A",
                "980A",
                "179A",
                "456A",
                "379A",
            };

            BuildDictionaries();

            long sum = 0;

            foreach (var line in data)
            {
                var modLine = "A" + line;

                int thisCount = 0;

                for (int p = 1; p < modLine.Length; p++)
                {
                    string S = "A" + BigMoves[modLine.Substring(p - 1, 2)];

                    for (int pp = 1; pp < S.Length; pp++)
                    {
                        string SS = "A" + SmallMoves[S.Substring(pp - 1, 2)];
                        /*
                        for (int ppp = 1; ppp < SS.Length; ppp++)
                        {
                            string SSS = SmallMoves[SS.Substring(ppp - 1, 2)];
                            Console.Write(SSS);
                            thisCount += SSS.Length;
                        }*/
                    }

                }

                Console.WriteLine();
                Console.WriteLine($"Count for line {line} is {thisCount} with value {Int32.Parse(line.Substring(0, 3))*thisCount}");

                sum += Int32.Parse(line.Substring(0, 3)) * thisCount;
            }

            Console.WriteLine($"Answer is {sum}");
        }

        public void Part2()
        {
            var data = File.ReadAllLines(@"data\day21.txt");

            List<int> cals = new List<int>();

            var values = data.Select(r => r.Split(' ')).ToArray();




            Console.WriteLine($"Answer is {values.Count()}");
        }

    }
}
