using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aoc2024
{
    internal class Day23Node
    {
        public string Name { get; set; }
        public List<Day23Node> Connections { get; } = new List<Day23Node>();
        public bool Visited { get; set; }
        public List<string> Groups {get; } = new List<string>();
    };

    internal class Day23
    {
        public Dictionary<string, Day23Node> Nodes = new Dictionary<string, Day23Node>();

        public Dictionary<string, int> Groups = new Dictionary<string, int>();

        internal void BuildGraph(string[][] input)
        {
            foreach (var edge in input)
            {
                if (!Nodes.Keys.Any(k => k == edge[0]))
                {
                    Nodes.Add(edge[0], new Day23Node() { Name = edge[0] });
                }
                if (!Nodes.Keys.Any(k => k == edge[1]))
                {
                    Nodes.Add(edge[1], new Day23Node() { Name = edge[1] });
                }

                if (!Nodes[edge[0]].Connections.Any(c => c.Name == edge[1]))
                {
                    Nodes[edge[0]].Connections.Add(Nodes[edge[1]]);
                }
                if (!Nodes[edge[1]].Connections.Any(c => c.Name == edge[0]))
                {
                    Nodes[edge[1]].Connections.Add(Nodes[edge[0]]);
                }
            }
        }

        IEnumerable<List<string>> Combinations(List<string> input)
        {
            yield return new List<string>();

            if (input.Count >= 1)
            {
                yield return new List<string>() { input[0] };
                foreach (var e in Combinations(input.Skip(1).ToList()))
                {
                    yield return e;
                    e.Insert(0, input[0] );
                    yield return e;
                }
            }
        }

        internal void AssignGroups()
        {
            foreach (var node in Nodes.Values)
            {
                for (int i = 0; i < node.Connections.Count - 1; i++)
                {
                    for (int j = i + 1; j < node.Connections.Count; j++)
                    {
                        var nl = new List<string>
                            { node.Name, node.Connections[i].Name, node.Connections[j].Name };
                        nl.Sort();
                        var g = string.Join(",", nl);

                        if (Groups.Keys.All(k => k != g))
                        {
                            Groups[g] = 0;
                        }
                        Groups[g] += 1;
                    }
                }
            }
        }

        internal void AssignGroups2()
        {
            foreach (var node in Nodes.Values)
            {
                var nl = node.Connections.Select(n => n.Name).ToList();
                nl.Add(node.Name);
                nl.Sort();

                foreach (var ng in Combinations(node.Connections.Select(n => n.Name).ToList()))
                {
                    var nng = ng;
                    nng.Sort();
                    if (nng.Count >= 4)
                    {
                        var g = string.Join(",", nng);
                        if (Groups.Keys.All(k => k != g))
                        {
                            Groups[g] = 0;
                        }

                        Groups[g] += 1;
                    }
                }
            }
        }

        public void Part1()
        {
            var data = File.ReadAllLines(@"data\day23.txt");

            
            data = new[]
            {
                "kh-tc",
                "qp-kh",
                "de-cg",
                "ka-co",
                "yn-aq",
                "qp-ub",
                "cg-tb",
                "vc-aq",
                "tb-ka",
                "wh-tc",
                "yn-cg",
                "kh-ub",
                "ta-co",
                "de-co",
                "tc-td",
                "tb-wq",
                "wh-td",
                "ta-ka",
                "td-qp",
                "aq-cg",
                "wq-ub",
                "ub-vc",
                "de-ta",
                "wq-aq",
                "wq-vc",
                "wh-yn",
                "ka-de",
                "kh-ta",
                "co-tc",
                "wh-qp",
                "tb-vc",
                "td-yn",
            };
            

            var values = data.Select(r => r.Split('-').ToArray()).ToArray();

            BuildGraph(values);

            AssignGroups();

            int sum = 0;

            foreach (var n in Nodes.Values)
            {
                Console.WriteLine($"Node: {n.Name} c: {string.Join(",", n.Connections.Select(c => c.Name))}");
            }

            foreach (var group in Groups.Keys)
            {
                if (Groups[group] == 3)
                {
                    Console.WriteLine($"Group: {group}");
                    if (group.Split(',').Any(s => s[0] == 't'))
                    {
                        Console.WriteLine("Has t");
                        sum++;
                    }
                }
            }

            Console.WriteLine($"Answer is {sum}");
        }

        internal int LongestGroupSize = 0;
        internal string LongestGroup = "";

        internal void CheckInternal(List<Day23Node> left, List<Day23Node> right)
        {
            var tocheck = right.ToList();

            while (tocheck.Count > 0)
            {
                var checkNode = tocheck.First();
                tocheck.Remove(checkNode);

                if (left.All(n => checkNode.Connections.IndexOf(n) >= 0))
                {
                    left.Add(checkNode);
                    if (left.Count > LongestGroupSize)
                    {
                        LongestGroupSize = left.Count;
                        var leftNodes = left.Select(n => n.Name).ToList();
                        leftNodes.Sort();
                        LongestGroup = string.Join(",", leftNodes);
                    }

                    CheckInternal(left, tocheck);
                }
            }
        }

        internal void CheckGroups(Day23Node node)
        {
            List<Day23Node> currentSet = new List<Day23Node>() {node};

            var children = node.Connections.ToList(); // Copy
            foreach (var child in node.Connections)
            {
                children.Remove(child);
                currentSet.Add(child);

                CheckInternal(currentSet, children);

                currentSet.Remove(child);
                children.Add(child);
            }
        }

        public void Part2()
        {
            var data = File.ReadAllLines(@"data\day23.txt");

            /*
            data = new[]
            {
                "kh-tc",
                "qp-kh",
                "de-cg",
                "ka-co",
                "yn-aq",
                "qp-ub",
                "cg-tb",
                "vc-aq",
                "tb-ka",
                "wh-tc",
                "yn-cg",
                "kh-ub",
                "ta-co",
                "de-co",
                "tc-td",
                "tb-wq",
                "wh-td",
                "ta-ka",
                "td-qp",
                "aq-cg",
                "wq-ub",
                "ub-vc",
                "de-ta",
                "wq-aq",
                "wq-vc",
                "wh-yn",
                "ka-de",
                "kh-ta",
                "co-tc",
                "wh-qp",
                "tb-vc",
                "td-yn",
            };
            */

            var values = data.Select(r => r.Split('-').ToArray()).ToArray();

            BuildGraph(values);

            foreach (var n in Nodes.Values)
            {
                CheckGroups(n);
            }

            // Note! Will print one of the groups twice. Remove it before entering the code.

            Console.WriteLine($"Answer is {LongestGroup}");
        }

    }
}
