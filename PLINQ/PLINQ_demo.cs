using System;
using System.Collections.Generic;
using System.Linq;

namespace PLINQDemo
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Demo of PLINQ in C#");
            Console.WriteLine("--------------------");
            var numbers = Enumerable.Range(1, 1000000).ToList(); // Corrected line

            // Use PLINQ to filter odd numbers in parallel
            var oddNumbers = numbers.AsParallel()
                                    .Where(number => number % 2 != 0)
                                    .ToList();

            Console.WriteLine($"Number of odd numbers: {oddNumbers.Count}");

            Console.WriteLine();

            Console.WriteLine("Input character set: uvwxyz");
            Console.WriteLine("Unordered output: ");
            Console.WriteLine(new string("uvwxyz".AsParallel().Select(c => char.ToUpper(c)).ToArray())); // Added new string to correctly format the output
            Console.WriteLine("Ordered output: ");
            Console.WriteLine(new string("uvwxyz".AsParallel().AsOrdered().Select(c => char.ToUpper(c)).ToArray())); // Added new string to correctly format the output
            Console.ReadLine();
        }
    }
}
