using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aoc2024
{
    internal class Day17
    {
        public long A = 0;
        public long B = 0;
        public long C = 0;

        public int PC = 0;

        public int Retval = 0;

        public int[] Program;

        public long Combo(int value)
        {
            switch (value)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                    return value;
                case 4:
                    return A;
                case 5:
                    return B;
                case 6:
                    return C;
            }

            throw new Exception("Illegal combo value");
        }

        public bool IsPart2 = false;

        public bool Step()
        {
            int imm = Program[PC + 1];
            switch (Program[PC])
            {
                case 0:
                    // adv - divide A by 2^combo
                    A /= ((long)1 << (int) Combo(imm));
                    break;
                case 1:
                    // bxl - bitwise XOR of B and lit, store in B
                    B = B ^ imm;
                    break;
                case 2:
                    // bst - b <- combo op % 8
                    B = Combo(imm) % 8;
                    break;
                case 3:
                    // jnz - if A not zero, jump to lit
                    if (IsPart2)
                    {
                        return false;
                    }
                    if (A != 0)
                    {
                        PC = imm - 2; // Increased to imm below
                    }

                    break;
                case 4:
                    // bxc - bitwise XOR of B and C, store in B (ignore op)
                    B = B ^ C;
                    break;
                case 5:
                    // out - Print value of combo op modulo 8
                    if (IsPart2)
                    {
                        Retval = (int)(Combo(imm) % 8);
                    }
                    else
                    {
                        Console.Write($"{Combo(imm) % 8},");
                    }
                    break;
                case 6:
                    // bdv - like adv, but store in B
                    B = A / ((long)1 << (int)Combo(imm));
                    break;
                case 7:
                    // cdv - like adv, but store in C
                    C = A / ((long)1 << (int)Combo(imm));
                    break;
            }

            PC += 2;

            return PC < Program.Length;
        }

        string ComboString(int imm)
        {
            if (imm < 4)
            {
                return imm.ToString();
            }

            switch (imm)
            {
                case 4:
                    return "A";
                case 5:
                    return "B";
                case 6:
                    return "C";
            }

            return "";
        }

        void DumpProgram()
        {
            for (int pc = 0; pc < Program.Length; pc += 2)
            {
                var imm = Program[pc + 1];
                Console.Write($"{pc}: ");
                switch (Program[pc])
                {
                    case 0:
                        // adv - divide A by 2^combo
                        Console.WriteLine($"adv {ComboString(imm)}");
                        break;
                    case 1:
                        // bxl - bitwise XOR of B and lit, store in B
                        Console.WriteLine($"bxl {imm}");
                        break;
                    case 2:
                        // bst - b <- combo op % 8
                        Console.WriteLine($"bst {ComboString(imm)}");
                        break;
                    case 3:
                        // jnz - if A not zero, jump to lit
                        Console.WriteLine($"jnz {imm}");
                        break;
                    case 4:
                        // bxc - bitwise XOR of B and C, store in B (ignore op)
                        Console.WriteLine($"bxc");
                        break;
                    case 5:
                        // out - Print value of combo op modulo 8
                        Console.WriteLine($"out {ComboString(imm)}");
                        break;
                    case 6:
                        // bdv - like adv, but store in B
                        Console.WriteLine($"bdv {ComboString(imm)}");
                        break;
                    case 7:
                        // cdv - like adv, but store in C
                        Console.WriteLine($"cdv {ComboString(imm)}");
                        break;
                }
            }
        }
        
        public void Part1()
        {
            var data = File.ReadAllLines(@"data\day17.txt");

            /*
            data = new string[]
            {
                "Register A: 729",
                "Register B: 0",
                "Register C: 0",
                "",
                "Program: 0,1,5,4,3,0",
            };
            */

            A = Int32.Parse(data[0].Split(':')[1]);
            B = Int32.Parse(data[1].Split(':')[1]);
            C = Int32.Parse(data[2].Split(':')[1]);

            Program = data[4].Split(new[] { ' ', ',' }).Skip(1).Select(Int32.Parse).ToArray();

            while (Step())
            {
                // Continue;
            }

            Console.WriteLine();
            Console.WriteLine($"Answer is {A}");
        }

        void RunProg()
        {
            PC = 0;
            while (Step())
            {
                // continue;
            }
        }

        private Int64 TestA = 0;

        bool FindA(int[] input)
        {
            if (input.Length == 0)
            {
                Console.WriteLine($"Found A, answer is {TestA}");
                return true;
            }

            TestA <<= 3;
            for (int i = 0; i < 8; i++)
            {
                TestA = (TestA & ~7) | i;
                A = TestA;
                RunProg();
                if (Retval == input[0])
                {
                    if (FindA(input.Skip(1).ToArray()))
                    {
                        TestA >>= 3;
                        return true;
                    };
                }
            }

            TestA >>= 3;
            return false;
        }

        public void Part2()
        {
            var data = File.ReadAllLines(@"data\day17.txt");

            /*
            data = new string[]
            {
                "Register A: 729",
                "Register B: 0",
                "Register C: 0",
                "",
                "Program: 0,1,5,4,3,0",
            };
            */

            IsPart2 = true;

            A = Int32.Parse(data[0].Split(':')[1]);
            B = Int32.Parse(data[1].Split(':')[1]);
            C = Int32.Parse(data[2].Split(':')[1]);

            Program = data[4].Split(new[] { ' ', ',' }).Skip(1).Select(Int32.Parse).ToArray();

            DumpProgram();

            A = 0;

            var input = Program.Reverse().ToArray(); // Make a copy;

            FindA(input);


            Console.WriteLine();
            Console.WriteLine($"Answer is {A}");
        }

    }
}
