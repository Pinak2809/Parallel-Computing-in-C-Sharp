using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class CancellationToken
{
    public static void Main()
    {
        Console.WriteLine("Technique 1: PLINQ Cancellation Demo using break statement");
        Console.WriteLine("----------------------------------------------------------");

        bool cancellationFlag = false; // Flag to indicate cancellation condition

        IEnumerable<int> numbers = Enumerable.Range(1, 1000);
        // PLINQ query to double each number
        Parallel.ForEach(numbers, (num, loopState) =>
        {
            int result = num * 2;
            if (result > 100) // Check cancellation condition
            {
                cancellationFlag = true;
                loopState.Break(); // Break out of the loop
            }
            else
            {
                Console.WriteLine(result);
            }
        });

        if (cancellationFlag)
        {
            Console.WriteLine("Operation was canceled.");
        }
    }
}
