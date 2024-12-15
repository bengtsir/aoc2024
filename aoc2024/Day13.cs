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
                var minTokens = 999999;

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
                        if (thisCost < minTokens)
                        {
                            minTokens = thisCost;

                            Console.WriteLine($"{puzzle} {ax} {ay} {bx} {by} {tx} {ty} {a} {b} {thisCost}");
                        }
                    }
                }

                if (minTokens < 999999)
                {
                    sum += minTokens;
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
                long minTokens = 999999;

                long ax = values[puzzle * 3][0];
                long ay = values[puzzle * 3][1];
                long bx = values[puzzle * 3 + 1][0];
                long by = values[puzzle * 3 + 1][1];
                long tx = values[puzzle * 3 + 2][0] + 10_000_000_000_000;
                long ty = values[puzzle * 3 + 2][1] + 10_000_000_000_000;

                /*
                ax = 258;
                bx = 147;
                tx = 369;
                */

                var axf = ArrayMethods.PrimeFactors(ax);
                var ayf = ArrayMethods.PrimeFactors(ay);
                var bxf = ArrayMethods.PrimeFactors(bx);
                var byf = ArrayMethods.PrimeFactors(by);

                Console.WriteLine($"Puzzle {puzzle}");
                Console.WriteLine($"ax = {ax} (" + string.Join(" ", axf.Select(v => v.ToString())) + ")");
                Console.WriteLine($"ay = {ay} (" + string.Join(" ", ayf.Select(v => v.ToString())) + ")");
                Console.WriteLine($"bx = {bx} (" + string.Join(" ", bxf.Select(v => v.ToString())) + ")");
                Console.WriteLine($"by = {by} (" + string.Join(" ", byf.Select(v => v.ToString())) + ")");
                var gcdx = Gcd(ax, bx, out var xgcdlist);
                var gcdy = Gcd(ay, by, out var ygcdlist);
                Console.WriteLine($"GCD(ax,bx) = {gcdx}, GCD(ay,by) = {gcdy}");
                var res = (tx % gcdx) == 0 && (ty % gcdy) == 0 && gcdx > 1 && gcdy > 1 ? "Yes" : "No";
                Console.WriteLine($"Has solution: {res}");

                var xmax = ax > bx ? ax : bx;
                var xmin = ax <= bx ? ax : bx;

                if (res == "Yes")
                {
                    int idx = xgcdlist.Count - 2;
                    long a = 1;
                    long b = -xgcdlist[idx - 1] / xgcdlist[idx];
                    while (idx > 1)
                    {
                        idx--;
                        var anew = b;
                        var bnew = a - (b * (xgcdlist[idx-1]/xgcdlist[idx]));
                        a = anew;
                        b = bnew;
                    }

                    a *= tx/gcdx;
                    b *= tx/gcdx;

                    Console.WriteLine($"{a}*{xmax} + {b}*{xmin} = {a*xmax + b*xmin}, should be {tx}");
                    Console.WriteLine($"{a-3}*{xmax} + {b+14}*{xmin} = {(a-3) * xmax + (b+14) * xmin}, should be {tx}");
                    Console.WriteLine($"{a-2*3}*{xmax} + {b+28}*{xmin} = {(a-6) * xmax + (b+28) * xmin}, should be {tx}");


                    var xfa = a;
                    var xfb = ax/gcdx;
                    if (b < 0)
                    {
                        xfb = -xfb;
                    }

                    var yfa = b;
                    var yfb = bx/gcdx;
                    if (a < 0)
                    {
                        yfb = -yfb;
                    }

                    Console.WriteLine($"x = {xfa} + {xfb}*r, y = {yfa} + {yfb}*r");

                    long minr = 0;
                    long maxr = 0;

                    if (xfa < 0)
                    {
                        minr = -(xfa - xfb + 1) / xfb;
                        maxr = yfa / -yfb;
                    }
                    else
                    {
                        minr = -(yfa - yfb + 1) / yfb;
                        maxr = xfa / -xfb;
                    }

                    long minRTokens = 3 * (xfa + xfb * minr) + (yfa + yfb * minr);
                    long maxRTokens = 3 * (xfa + xfb * maxr) + (yfa + yfb * maxr);

                    Console.WriteLine($"minr = {minr} maxr = {maxr}");
                    Console.WriteLine($"Min: x = {xfa + xfb * minr} y = {yfa + yfb * minr}, tokens = {minRTokens} res = {(xfa + xfb * minr) * xmax + (yfa + yfb * minr) * xmin}");
                    Console.WriteLine($"Max: x = {xfa + xfb * maxr} y = {yfa + yfb * maxr}, tokens = {maxRTokens} res = {(xfa + xfb * maxr) * xmax + (yfa + yfb * maxr) * xmin}");

                    if (minRTokens < 1 || maxRTokens < 1)
                    {
                        Console.WriteLine($"Error!");
                    }

                    if (minRTokens < maxRTokens)
                    {
                        sum += minRTokens;
                    }
                    else
                    {
                        sum += maxRTokens;
                    }
                }



            }


            Console.WriteLine($"Answer is {sum}");
        }

    }
}
