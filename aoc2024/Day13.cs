using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace aoc2024
{
    internal class Day13
    {
        public void Part1()
        {
            var data = File.ReadAllLines(@"data\day13.txt");

            /*
            data = new[]
            {
                "Button A: X+94, Y+34",
                "Button B: X+22, Y+67",
                "Prize: X=8400, Y=5400",
                "",
                "Button A: X+26, Y+66",
                "Button B: X+67, Y+21",
                "Prize: X=12748, Y=12176",
                "",
                "Button A: X+17, Y+86",
                "Button B: X+84, Y+37",
                "Prize: X=7870, Y=6450",
                "",
                "Button A: X+69, Y+23",
                "Button B: X+27, Y+71",
                "Prize: X=18641, Y=10279",
            };
            */

            var re = new Regex(@"X=?([+\d]+), Y=?([´+\d]+)");

            var values = data.Where(r => r.Length > 2).Select(r => re.Match(r)).Select(m => new int[] { Int32.Parse(m.Groups[1].Value), Int32.Parse(m.Groups[2].Value) }).ToArray();

            Int64 sum = 0;

            for (int puzzle = 0; puzzle < values.Length / 3; puzzle++)
            {
                var minpress = 999999;

                int ax = values[puzzle * 3][0];
                int ay = values[puzzle * 3][1];
                int bx = values[puzzle * 3 + 1][0];
                int by = values[puzzle * 3 + 1][1];
                int tx = values[puzzle * 3 + 2][0];
                int ty = values[puzzle * 3 + 2][1];

                for (int a = 0; a <= 100 && a <= tx / ax && a <= ty / ay; a++)
                {
                    int b = (tx - a * ax) / bx;

                    if (a * ax + b * bx == tx &&
                        a * ay + b * by == ty)
                    {
                        var thisCost = a * 3 + b;
                        if (thisCost < minpress)
                        {
                            minpress = thisCost;

                            Console.WriteLine($"{puzzle} {ax} {ay} {bx} {by} {tx} {ty} {a} {b} {thisCost}");
                        }
                    }
                }

                if (minpress < 999999)
                {
                    sum += minpress;
                }

            }


            Console.WriteLine($"Answer is {sum}");
        }

        internal long Gcd(long a, long b, out List<long> values)
        {
            values = new List<long>();

            long r2 = a > b ? a : b;
            long r1 = a <= b ? a : b;
            long r0 = r2 % r1;

            values.Add(r2);
            values.Add(r1);

            if (r0 == 0)
            {
                values.Add(0);
                return r1;
            }

            while (r0 != 0)
            {
                r2 = r1;
                r1 = r0;
                r0 = r2 % r1;
                values.Add(r1);
            }

            return r1;

        }

        public void Part2()
        {
            var data = File.ReadAllLines(@"data\day13.txt");

            /*
            data = new[]
            {
                "Button A: X+94, Y+34",
                "Button B: X+22, Y+67",
                "Prize: X=8400, Y=5400",
                "",
                "Button A: X+26, Y+66",
                "Button B: X+67, Y+21",
                "Prize: X=12748, Y=12176",
                "",
                "Button A: X+17, Y+86",
                "Button B: X+84, Y+37",
                "Prize: X=7870, Y=6450",
                "",
                "Button A: X+69, Y+23",
                "Button B: X+27, Y+71",
                "Prize: X=18641, Y=10279",
            };
            */

            var re = new Regex(@"X=?([+\d]+), Y=?([´+\d]+)");

            var values = data.Where(r => r.Length > 2).Select(r => re.Match(r)).Select(m => new int[] { Int32.Parse(m.Groups[1].Value), Int32.Parse(m.Groups[2].Value) }).ToArray();

            Int64 sum = 0;

            for (int puzzle = 0; puzzle < values.Length / 3; puzzle++)
            {
                var ax = values[puzzle * 3][0];
                var ay = values[puzzle * 3][1];
                var bx = values[puzzle * 3 + 1][0];
                var by = values[puzzle * 3 + 1][1];
                var tx = values[puzzle * 3 + 2][0] + 10_000_000_000_000;
                var ty = values[puzzle * 3 + 2][1] + 10_000_000_000_000;

                /*
                 * x * ax + y * bx = tx
                 * x * ay + y * by = ty
                 *
                 * x = (tx - (bx * ty)/by) / (ax - (bx * ay)/by)
                 * y = (tx - (ax * ty)/ay) / (ay - (ax * by)/ay)
                 */

                /*
                ax = 258;
                bx = 147;
                tx = 369;
                */

                /*
                var axf = ArrayMethods.PrimeFactors(ax);
                var ayf = ArrayMethods.PrimeFactors(ay);
                var bxf = ArrayMethods.PrimeFactors(bx);
                var byf = ArrayMethods.PrimeFactors(by);
                */

                Console.WriteLine($"Puzzle {puzzle}");
                /*
                var gcdx = Gcd(ax, bx, out var xgcdlist);
                var gcdy = Gcd(ay, by, out var ygcdlist);
                var res = (tx % gcdx) == 0 && (ty % gcdy) == 0 && gcdx > 1 && gcdy > 1 ? "Yes" : "No";
                Console.WriteLine($"Has solution: {res}");
                */

                // A presses
                Int64 t = tx * by - bx * ty;
                Int64 n = ax * by - bx * ay;

                if (t % n != 0)
                {
                    Console.WriteLine("No solution");
                }
                else
                {
                    Int64 apress = t / n;

                    t = tx * ay - ax * ty;
                    n = bx * ay - ax * by;

                    if (t % n != 0)
                    {
                        Console.WriteLine("No solution");
                    }
                    else
                    {
                        Int64 bpress = t / n;

                        Console.WriteLine($"{apress}*{ax} + {bpress}*{bx} = {apress * ax + bpress * bx} should be {tx}");
                        Console.WriteLine($"{apress}*{ay} + {bpress}*{by} = {apress * ay + bpress * by} should be {ty}");

                        sum += 3 * apress + bpress;
                    }
                }


            }


            Console.WriteLine($"Answer is {sum}");
        }

    }
}
