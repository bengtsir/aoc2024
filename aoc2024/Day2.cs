using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aoc2024
{
    internal class Day2
    {
        public void Part1()
        {
            var data = File.ReadAllLines(@"data\day2.txt");

            var values = data.Select(r => r.Split(' ').Select(s => Convert.ToInt32(s)).ToArray()).ToArray();

            int safe = 0;

            foreach (var r in values)
            {
                var tmpl = new List<int>();

                for (int i = 0; i < r.Length - 1; i++)
                {
                    tmpl.Add(r[i + 1] - r[i]);
                }

                if (tmpl.All(v => v >= 1 && v <= 3) || tmpl.All(v => v >= -3 && v <= -1))
                {
                    safe++;
                }
             }

            Console.WriteLine($"Answer is {safe}");
        }

        public void Part2()
        {
            var data = File.ReadAllLines(@"data\day2.txt");

            var values = data.Select(r => r.Split(' ').Select(s => Convert.ToInt32(s)).ToArray()).ToArray();

            int safe = 0;

            foreach (var r in values)
            {
                var tmpl = new List<int>();

                for (int i = 0; i < r.Length - 1; i++)
                {
                    tmpl.Add(r[i + 1] - r[i]);
                }

                if (tmpl.All(v => v >= 1 && v <= 3) || tmpl.All(v => v >= -3 && v <= -1))
                {
                    safe++;
                }
                else
                {
                    for (int rem = 0; rem < r.Length; rem++)
                    {
                        tmpl = new List<int>();
                        var rmod = r.Select(c => c).ToList();
                        rmod.RemoveAt(rem);

                        for (int i = 0; i < rmod.Count - 1; i++)
                        {
                            tmpl.Add(rmod[i + 1] - rmod[i]);
                        }
                        if (tmpl.All(v => v >= 1 && v <= 3) || tmpl.All(v => v >= -3 && v <= -1))
                        {
                            safe++;
                            break;
                        }
                    }
                }
            }

            Console.WriteLine($"Answer is {safe}");
        }

    }
}
