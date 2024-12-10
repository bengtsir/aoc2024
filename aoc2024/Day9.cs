using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aoc2024
{
    internal class Day9
    {
        internal class Entry
        {
            public int id;
            public int length;
            public bool isSpace;
        };

        internal void Trim(ref List<Entry> entries)
        {
            entries.RemoveAll(e => e.length == 0);
            if (entries[entries.Count - 1].isSpace)
            {
                entries.RemoveAt(entries.Count - 1);
            }
        }

        internal void PrintLine(List<Entry> entries)
        {
            foreach (Entry e in entries)
            {
                var mark = (e.isSpace ? "." : e.id.ToString());

                for (int i = 0; i < e.length; i++)
                {
                    Console.Write(mark);
                }
            }
            Console.WriteLine();
        }

        public void Part1()
        {
            var data = File.ReadAllLines(@"data\day9.txt");

            /*
            data = new string[]
            {
                "2333133121414131402",
                "12345",
            };
            */

            var rawValues = data[0].Select(c => (int)(c - '0')).ToArray();

            List<Entry> values = new List<Entry>();
            int fileId = 0;
            for (int i = 0; i < rawValues.Length; i++)
            {
                values.Add(new Entry { id = fileId, length = rawValues[i], isSpace = ((i % 2) == 1) });
                if ((i % 2) == 1)
                {
                    fileId++;
                }
            }

            //PrintLine(values);

            Trim(ref values);

            //PrintLine(values);

            int currPos = 0;
            while (currPos < values.Count && !values[currPos].isSpace)
            {
                currPos++;
            }

            while (currPos < values.Count)
            {
                var spcVal = values[currPos];
                var lastVal = values[values.Count - 1];

                if (lastVal.length >= spcVal.length)
                {
                    lastVal.length = lastVal.length - spcVal.length;
                    spcVal.id = lastVal.id;
                    spcVal.isSpace = false;
                }
                else
                {
                    spcVal.length = spcVal.length - lastVal.length;
                    values.Remove(lastVal);
                    values.Insert(currPos, lastVal);
                }

                Trim(ref values);

                while (currPos < values.Count && !values[currPos].isSpace)
                {
                    currPos++;
                }
                //PrintLine(values);
            }

            Int64 sum = 0;
            currPos = 0;

            for (int i = 0; i < values.Count; i++)
            {
                for (int j = 0; j < values[i].length; j++)
                {
                    sum += currPos * values[i].id;
                    currPos++;
                }
            }


            Console.WriteLine($"Answer is {sum}");
        }

        public void Part2()
        {
            var data = File.ReadAllLines(@"data\day9.txt");

            /*
            data = new string[]
            {
                "2333133121414131402",
                "12345",
            };
            */

            var rawValues = data[0].Select(c => (int)(c - '0')).ToArray();

            List<Entry> values = new List<Entry>();
            int fileId = 0;
            for (int i = 0; i < rawValues.Length; i++)
            {
                values.Add(new Entry { id = fileId, length = rawValues[i], isSpace = ((i % 2) == 1) });
                if ((i % 2) == 1)
                {
                    fileId++;
                }
            }

            //PrintLine(values);

            Trim(ref values);

            //PrintLine(values);

            int currId = values[values.Count - 1].id;

            while (currId > 0)
            {
                var toMove = values.Find(x => x.id == currId);
                var vid = values.IndexOf(toMove);
                var firstSpace = values.Find(x => x.isSpace && x.length >= toMove.length);

                if (firstSpace != null)
                {
                    var firstSpaceIdx = values.IndexOf(firstSpace);

                    if (firstSpaceIdx < vid)
                    {
                        values.Insert(firstSpaceIdx, new Entry { id = toMove.id, isSpace = toMove.isSpace, length = toMove.length});
                        firstSpace.length -= toMove.length;
                        toMove.isSpace = true;
                    }
                }

                currId--;

                //PrintLine(values);
            }

            Int64 sum = 0;
            int currPos = 0;

            for (int i = 0; i < values.Count; i++)
            {
                for (int j = 0; j < values[i].length; j++)
                {
                    if (!values[i].isSpace)
                    {
                        sum += currPos * values[i].id;
                    }
                    currPos++;
                }
            }


            Console.WriteLine($"Answer is {sum}");
        }

        internal Int64 P2standalone(string input)
        {
            var rawValues = input.Select(c => (int)(c - '0')).ToArray();

            List<Entry> values = new List<Entry>();
            int fileId = 0;
            for (int i = 0; i < rawValues.Length; i++)
            {
                values.Add(new Entry { id = fileId, length = rawValues[i], isSpace = ((i % 2) == 1) });
                if ((i % 2) == 1)
                {
                    fileId++;
                }
            }

            Trim(ref values);

            int currId = values[values.Count - 1].id;

            while (currId > 0)
            {
                var toMove = values.Find(x => x.id == currId);
                var vid = values.IndexOf(toMove);
                var firstSpace = values.Find(x => x.isSpace && x.length >= toMove.length);

                if (firstSpace != null)
                {
                    var firstSpaceIdx = values.IndexOf(firstSpace);

                    if (firstSpaceIdx < vid)
                    {
                        values.Insert(firstSpaceIdx, new Entry { id = toMove.id, isSpace = toMove.isSpace, length = toMove.length });
                        firstSpace.length -= toMove.length;
                        toMove.isSpace = true;
                    }
                }

                currId--;
            }

            Int64 sum = 0;
            int currPos = 0;

            for (int i = 0; i < values.Count; i++)
            {
                for (int j = 0; j < values[i].length; j++)
                {
                    if (!values[i].isSpace)
                    {
                        sum += currPos * values[i].id;
                    }
                    currPos++;
                }
            }


            return sum;

        }


        internal void P2gen()
        {
            var s = "";
            var r = new Random();
            for (int i = 0; i < 50; i++)
            {
                s = "";
                int slen = r.Next(5) + 10;

                for (int j = 0; j < slen; j++)
                {
                    s += (char)(r.Next(10) + '0');
                }

                while (s.IndexOf("00") >= 0)
                {
                    s = s.Replace("00", "0");
                }

                Console.WriteLine($"Input: {s}");
                Console.WriteLine($"Value: {P2standalone(s)}");
            }
        }
    }
}
