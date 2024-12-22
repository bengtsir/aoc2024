using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aoc2024
{
    internal class Day22
    {
        internal long Iterate(long number)
        {
            number = number ^ (number * 64);
            number = number % 16777216;
            
            number = number ^(number / 32);
            number = number % 16777216;

            number = number ^ (number * 2048);
            number = number % 16777216;

            return number;
        }

        public void Part1()
        {
            var data = File.ReadAllLines(@"data\day22.txt");

            /*
            data = new[]
            {
                "1",
                "10",
                "100",
                "2024",
            };
            */

            long sum = 0;

            foreach (var line in data)
            {
                long secret = Int64.Parse(line);

                for (int i = 0; i < 2000; i++)
                {
                    secret = Iterate(secret);
                }

                Console.WriteLine($"Seed {line}: {secret}");

                sum += secret;
            }



            Console.WriteLine($"Answer is {sum}");
        }

        internal long FirstSums(int[] diffs, int[] numbers, int a, int b, int c, int d)
        {
            for (int i = 0; i < diffs.Length - 3; i++)
            {
                if (diffs[i] == a && diffs[i + 1] == b && diffs[i + 2] == c && diffs[i + 3] == d)
                {
                    return numbers[i + 3];
                }
            }

            return 0;
        }

        public void Part2()
        {
            var data = File.ReadAllLines(@"data\day22.txt");

            /*
            data = new[]
            {
                "1",
                "10",
                "100",
                "2024",
            };
            */

            int[][] diffs = new int[data.Length][];
            int[][] numbers = new int[data.Length][];

            long sum = 0;

            for (int row = 0; row < data.Length; row++)
            {
                var line = data[row];
                long secret = Int64.Parse(line);

                diffs[row] = new int[2000];
                numbers[row] = new int[2000];

                int lastlastDigit = (int)(secret % 10);

                for (int i = 0; i < 2000; i++)
                {
                    var newsec = Iterate(secret);
                    numbers[row][i] = (int)(newsec % 10);
                    diffs[row][i] = (int)(numbers[row][i] - lastlastDigit);
                    lastlastDigit = numbers[row][i];
                    secret = newsec;
                }
            }

            for (int a = -9; a <= 9; a++)
            {
                for (int b = -9; b <= 9; b++)
                {
                    Console.WriteLine($"a, b = {a}, {b}");
                    for (int c = -9; c <= 9; c++)
                    {
                        for (int d = -9; d <= 9; d++)
                        {
                            int[] testL = new []
                            {
                                9+a,
                                9+a+b,
                                9+a+b+c,
                                9+a+b+c+d,
                            };

                            int[] testH = new[]
                            {
                                -9+a,
                                -9+a+b,
                                -9+a+b+c,
                                -9+a+b+c+d,
                            };

                            if (testL.All(n => n >= -9) && testH.All(n => n <= 9))
                            {
                                long localsum = 0;

                                for (int i = 0; i < diffs.Length; i++)
                                {
                                    localsum += FirstSums(diffs[i], numbers[i], a, b, c, d);
                                }

                                if (localsum > sum)
                                {
                                    Console.WriteLine($"Found new max: {localsum} with seq {a}, {b}, {c}, {d}");
                                    sum = localsum;
                                }
                            }

                        }
                    }
                }
            }


            Console.WriteLine($"Answer is {sum}");
        }

    }
}
