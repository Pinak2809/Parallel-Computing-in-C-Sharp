using System;
using System.Threading.Tasks;
using System.Diagnostics;

class Program
{
    static void Main()
    {
        Console.WriteLine("Harnessing Local Values in Parallel Processing");
        Console.WriteLine("-----------------------------------------------");
        Console.WriteLine("");
        Console.WriteLine("Performance Bottleneck Example: ");
        Console.WriteLine("------------------------------");

        Stopwatch myStopWatch1 = new Stopwatch();

        object lockObject1 = new object();
        double totalSum1 = 0.0;

        myStopWatch1.Start();
        // Using Parallel.For with local values to sum square roots
        Parallel.For(1, 10000000,
            i => { lock (lockObject1) totalSum1 += Math.Sqrt(i); });
        //Stop StopWatch...
        myStopWatch1.Stop();

        Console.WriteLine($"Total sum of square roots: {totalSum1} calculated in {myStopWatch1.Elapsed.TotalMilliseconds} ms (Performance Bottleneck)");
        Console.WriteLine("");
        Console.WriteLine("Performance Improvement Example: ");
        Console.WriteLine("----------------------------------");

        Stopwatch myStopWatch2 = new Stopwatch();

        object lockObject2 = new object();
        double totalSum2 = 0.0;

        myStopWatch2.Start();
        // Using Parallel.For with local values to sum square roots efficiently
        Parallel.For(
            1, 
            10000000,
            () => 0.0, // Initialize the local value
            (i, state, localSum) => localSum + Math.Sqrt(i),
            localSum => { lock (lockObject2) totalSum2 += localSum; } // Combine local value with grand total
        );

        //Stop StopWatch...
        myStopWatch2.Stop();

        Console.WriteLine($"Total sum of square roots: {totalSum2} calculated in {myStopWatch2.Elapsed.TotalMilliseconds} ms (Performance Imporvement)");

        Console.ReadLine();
    }
}
