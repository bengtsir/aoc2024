using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aoc2024
{
    internal class Day1
    {
        public void Part1()
        {
            var data = File.ReadAllLines(@"data\day1.txt");

            List<int> cals = new List<int>();

            var values = data.Select(r => r.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)).ToArray();

            var l1 = values.Select(v => Int32.Parse(v[0])).ToList();
            var l2 = values.Select(v => Int32.Parse(v[1])).ToList();

            l1.Sort();
            l2.Sort();

            Int64 sum = 0;

            for (int i = 0; i < l1.Count(); i++)
            {
                var diff = l1[i] - l2[i];
                if (diff < 0)
                {
                    diff = -diff;
                }
                sum += diff;
            }



            Console.WriteLine($"Answer is {sum}");
        }

        public void Part2()
        {
            var data = File.ReadAllLines(@"data\day1.txt");

            List<int> cals = new List<int>();

            var values = data.Select(r => r.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)).ToArray();

            var l1 = values.Select(v => Int32.Parse(v[0])).ToList();
            var l2 = values.Select(v => Int32.Parse(v[1])).ToList();

            l1.Sort();
            l2.Sort();

            Int64 sum = 0;

            for (int i = 0; i < l1.Count(); i++)
            {
                int c = 0;

                for (int k = 0; k < l2.Count(); k++)
                {
                    if (l1[i] == l2[k])
                    {
                        c++;
                    }
                }

                sum += l1[i] * c;
            }



            Console.WriteLine($"Answer is {sum}");
        }

    }
}
