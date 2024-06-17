using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

public class CancellationToken
{
    public static void Main()
    {
        Console.WriteLine("Technique 1: PLINQ Cancellation Demo using cancellation token");
        Console.WriteLine("-------------------------------------------------------------");
        
        // Create a cancellation token source
        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        
        // Get the cancellation token from the token source
        CancellationToken cancellationToken = cancellationTokenSource.Token;

        IEnumerable<int> numbers = Enumerable.Range(1, 1000);
        // PLINQ query to double each number
        var query = from num in numbers.AsParallel().WithCancellation(cancellationToken)
                    select num * 2;

        // Start a task to cancel the operation after a certain time or condition
        Task.Run(() =>
        {
            // Cancel the operation after 100 milliseconds
            // You can also cancel based on a condition (e.g., result > 100)
            Thread.Sleep(100);
            cancellationTokenSource.Cancel();
        });

        try
        {
            foreach (var result in query)
            {
                Console.WriteLine(result);
            }
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("Operation was canceled.");
        }
    }
}
