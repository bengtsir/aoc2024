using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aoc2024
{
    internal class Day11
    {
        internal int NumberDecDigits(Int64 i)
        {

            if (i >= 0 && i < 10)
            {
                return 1;
            }
            else if (i >= 10 && i < 100)
            {
                return 2;
            }
            else if (i >= 100 && i < 1000)
            {
                return 3;
            }
            else if (i >= 1000 && i < 10000)
            {
                return 4;
            }
            else if (i >= 10000 && i < 100000)
            {
                return 5;
            }
            else if (i >= 100000 && i < 1000000)
            {
                return 6;
            }
            else if (i >= 1000000 && i < 10000000)
            {
                return 7;
            }
            else if (i >= 10000000 && i < 100000000)
            {
                return 8;
            }
            else if (i >= 100000000 && i < 1000000000)
            {
                return 9;
            }
            else if (i >= 1000000000 && i < 10000000000)
            {
                return 10;
            }
            else if (i >= 10000000000 && i < 100000000000)
            {
                return 11;
            }
            else if (i >= 100000000000 && i < 1000000000000)
            {
                return 12;
            }
            else if (i >= 1000000000000 && i < 10000000000000)
            {
                return 13;
            }
            else if (i >= 10000000000000 && i < 100000000000000)
            {
                return 14;
            }
            else if (i >= 100000000000000 && i < 1000000000000000)
            {
                return 15;
            }
            else if (i >= 1000000000000000 && i < 10000000000000000)
            {
                return 16;
            }

            throw new Exception($"Unknown number {i}");
        }

        public void Part1()
        {
            var data = File.ReadAllLines(@"data\day11.txt");

            /*
            data = new[]
            {
                "0 1 10 99 999",
                "125 17"
            };
            */

            var values = data[0].Split(' ').Select(Int64.Parse).ToList();

            int iterations = 25;

            for (int i = 0; i < iterations; i++)
            {
                int pc = 0;

                while (pc < values.Count)
                {
                    int n = NumberDecDigits(values[pc]);

                    if (values[pc] == 0)
                    {
                        values.RemoveAt(pc);
                        values.Insert(pc, 1);
                    }
                    else if ((n % 2) == 0)
                    {
                        var s = values[pc].ToString();
                        Int64 a = Int64.Parse(s.Substring(0, s.Length / 2));
                        Int64 b = Int64.Parse(s.Substring(s.Length / 2));

                        values.RemoveAt(pc);
                        values.Insert(pc, a);
                        pc++;
                        values.Insert(pc, b);
                    }
                    else
                    {
                        var v = values[pc] * 2024;
                        values.RemoveAt(pc);
                        values.Insert(pc, v);
                    }

                    pc++;
                }
            }

            Console.WriteLine($"Answer is {values.Count}");
        }

        Dictionary<Int64, Int64>[] Cache;

        internal Int64 NumberofResults(Int64 v, int depth)
        {
            if (depth == 0)
            {
                return 1;
            }

            if (Cache[depth].TryGetValue(v, out var cacheVal))
            {
                return cacheVal;
            }

            if (v == 0)
            {
                Int64 N = NumberofResults(1, depth - 1);
                Cache[depth][v] = N;
                return N;
            }

            int n = NumberDecDigits(v);

            if ((n % 2) == 0)
            {
                var s = v.ToString();
                Int64 a = Int64.Parse(s.Substring(0, s.Length / 2));
                Int64 b = Int64.Parse(s.Substring(s.Length / 2));

                Int64 N = NumberofResults(a, depth - 1) + NumberofResults(b, depth - 1);
                Cache[depth][v] = N;
                return N;
            }

            Int64 NN = NumberofResults(v * 2024, depth - 1);
            Cache[depth][v] = NN;
            return NN;
        }

        public void Part2()
        {
            var data = File.ReadAllLines(@"data\day11.txt");

            /*
            data = new[]
            {
                "0 1 10 99 999",
                "125 17"
            };
            */

            var values = data[0].Split(' ').Select(Int64.Parse).ToList();

            int iterations = 75;

            Cache = new Dictionary<Int64, Int64>[iterations+1];

            for (int i = 0; i < iterations+1; i++)
            {
                Cache[i] = new Dictionary<Int64, Int64>();
            }

            var n = values.Sum(v => NumberofResults(v, iterations));

            Console.WriteLine($"Answer is {n}");
        }

        internal Int64 FindValue(Int64 indexToFind, Int64 v, int depth)
        {
            if (depth == 0)
            {
                return v;
            }

            if (v == 0)
            {
                return FindValue(indexToFind, 1, depth - 1);
            }

            int n = NumberDecDigits(v);

            if ((n % 2) == 0)
            {
                var s = v.ToString();
                Int64 a = Int64.Parse(s.Substring(0, s.Length / 2));
                Int64 b = Int64.Parse(s.Substring(s.Length / 2));

                Int64 N = NumberofResults(a, depth - 1);
                if (N < indexToFind)
                {
                    return FindValue(indexToFind - N, b, depth - 1);
                }
                else
                {
                    return FindValue(indexToFind, a, depth - 1);
                }
            }

            return FindValue(indexToFind, v * 2024, depth - 1);
        }

        public void Part3()
        {
            // As suggested by Jon Sten

            var data = File.ReadAllLines(@"data\day11.txt");

            data = new[]
            {
                "0 1 10 99 999",
                "125 17"
            };
            

            var values = data[0].Split(' ').Select(Int64.Parse).ToList();

            int iterations = 75;
            Int64 indexToFind = 1_000_000_000_000;

            Cache = new Dictionary<Int64, Int64>[iterations + 1];

            for (int i = 0; i < iterations + 1; i++)
            {
                Cache[i] = new Dictionary<Int64, Int64>();
            }

            Int64 K = -1;

            foreach (var v in values)
            {
                var N = NumberofResults(v, iterations);
                if (N < indexToFind)
                {
                    indexToFind -= N;
                }
                else
                {
                    K = FindValue(indexToFind, v, iterations);
                }
            }

            Console.WriteLine($"Answer is {K}");
        }

    }
}
