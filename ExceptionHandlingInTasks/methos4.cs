using System;
using System.Linq;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Efficient Exception-Handling in Tasks and detached tasks with timeouts.");
        Console.WriteLine("-----------------------------------------------------------------------");

        // Detach autonomous task
        _ = Task.Run(() => 
        {
            try
            {
                // Simulating some operation
                Console.WriteLine("Detached task running...");
                // Simulating an exception
                throw new InvalidOperationException("Detached task encountered an error.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Detached task caught an exception: " + ex.Message);
            }
        });

        // Task waited upon with a timeout
        Task<int> calc = Task.Run(() =>
        {
            int divisor = 0;
            // Long running calc
            Task.Delay(2000).Wait();
            return 7 / divisor; // Simulating an exception
        });

        try 
        {
            // Waiting for the task with a timeout
            if (await Task.WhenAny(calc, Task.Delay(1000)) == calc)
            {
                // If task completed within timeout
                Console.WriteLine("Result of calculation: " + calc.Result);
            }
            else
            {
                // If task did not complete within timeout
                Console.WriteLine("Calculation timed out.");
            }
        }
        catch (AggregateException ex)
        {
            if (calc.Exception != null && calc.Exception.InnerExceptions.Any())
            {
                foreach (var innerEx in calc.Exception.InnerExceptions)
                {
                    Console.WriteLine(innerEx.Message);
                }
            }
            else
            {
                Console.WriteLine("Task with timeout caught an exception: " + ex.Message);
            }
        }

        Console.WriteLine("Program completed.");
        Console.ReadLine(); // Placed here to wait for user input at the end
    }
}
