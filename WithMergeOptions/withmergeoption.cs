using System;
using System.Threading.Tasks;
using System.Linq;

class Program
{
    static void Main()
    {
        Console.WriteLine("PLINQ's WithmergeOptions Demo in C#");
        Console.WriteLine("-----------------------------------");

        int[] numbers = Enumerable.Range(1, 10000).ToArray();

        // Using WithMergeOptions to control buffering behavior
        var result = numbers.AsParallel()
                            .WithMergeOptions(ParallelMergeOptions.AutoBuffered) // Default one (ParallelMergeOptions.Default)
                            .Where(x => x % 2 == 0)
                            .Select(x => x)
                            .ToArray();

        Console.WriteLine("Even Numbers between 1 and 10000:");
        foreach (var item in result)
        {
            Console.Write(item + "\n");
        }
    }
}