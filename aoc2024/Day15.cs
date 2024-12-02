﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aoc2024
{
    internal class Day15
    {
        public void Part1()
        {
            var data = File.ReadAllLines(@"data\day15.txt");

            List<int> cals = new List<int>();

            var values = data.Select(r => r.Split(' ')).ToArray();

            /*
            var values = data.Select(r => r.Length == 0 ? -1 : Int32.Parse(r)).ToArray();

            var values = data.Select(r => r.Select(c => (int)(c - 'a')).ToArray()).ToArray();

            var values = data.Select(r => Int64.Parse(r)).ToArray();
            */



            Console.WriteLine($"Answer is {values.Count()}");
        }

        public void Part2()
        {
            var data = File.ReadAllLines(@"data\day15.txt");

            List<int> cals = new List<int>();

            var values = data.Select(r => r.Split(' ')).ToArray();




            Console.WriteLine($"Answer is {values.Count()}");
        }

    }
}
