using System;
using System.Threading;
using System.Threading.Tasks;

namespace TaskCompletionSourceDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("TaskCompletionSource ");
            Console.WriteLine("--------------------");

            // Step 1: Create an Instance of TaskCompletionSource Class
            var source = new TaskCompletionSource<string>();

            // Step 2: Get the Task
            Task<string> task = source.Task;

            // Start a new thread to simulate some work
            new Thread(() =>
            {
                // Simulate a delay to represent work being done
                Thread.Sleep(5000); // Wait for 5 seconds

                // Step 3: Control the Task using SetResult(), SetException(), or SetCanceled()
                source.SetResult("Why do French people eat snails? They don’t like fast food.");
                //source.SetException(new Exception("Error came from SetException method"));
                //source.SetCanceled();
                //source.SetResult("My Result");
                bool isSuccess = source.TrySetResult("My Result");
                if (isSuccess)
                    Console.WriteLine("Success..");
            }).Start();

            // Step 4: Use the Task
            try
            {
                // Wait for the task to complete and get the result
                Console.WriteLine("Waiting for the task to complete...");
                string result = task.Result;
                Console.WriteLine($"Task completed with result: {result}");
            }
            catch (AggregateException ex)
            {
                // Handle any exceptions that were thrown during the task execution
                foreach (var innerException in ex.InnerExceptions)
                {
                    Console.WriteLine($"Task threw an exception: {innerException.Message}");
                }
            }
        }
    }
}
