using System;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    static void Main()
    {
        Console.WriteLine("Optimizing Task Execution with TaskCreationOptions");
        Console.WriteLine("---------------------------------------------------");

        Task parent = Task.Factory.StartNew(() => // Parent Task
        {
            Console.WriteLine($"Parent task started running on thread: {Thread.CurrentThread.ManagedThreadId}");

            // None: Default behavior (task continuation runs inline on the same thread)
            Task.Factory.StartNew(() => // Detached task
            {
                Console.WriteLine($"Detached task started running on thread: {Thread.CurrentThread.ManagedThreadId}");
            });

            Task.Factory.StartNew(() => // task
            {
                Console.WriteLine($"With PreferFairness, task started running on thread: {Thread.CurrentThread.ManagedThreadId}");
            }, TaskCreationOptions.PreferFairness);

            Task.Factory.StartNew(() => // Child task
            {
                Console.WriteLine($"With Attached To Parent, Child task started running on thread: {Thread.CurrentThread.ManagedThreadId}");
            }, TaskCreationOptions.AttachedToParent);

            Task.Factory.StartNew(() => // With DenyChildAttach, Task as Detached task
            {
                Console.WriteLine($"With DenyChildAttach, task started as a Detached Task running on thread: {Thread.CurrentThread.ManagedThreadId}");
            }, TaskCreationOptions.DenyChildAttach);

            Task.Factory.StartNew(() => // With HideScheduler
            {
                Console.WriteLine($"With HideScheduler, task started running on thread: {Thread.CurrentThread.ManagedThreadId}");
            }, TaskCreationOptions.HideScheduler);

            Task.Factory.StartNew(() => // With RunContinuationsAsynchronously
            {
                Console.WriteLine($"With RunContinuationsAsynchronously, task started running on thread: {Thread.CurrentThread.ManagedThreadId}");
            }, TaskCreationOptions.RunContinuationsAsynchronously);

            Task.Factory.StartNew(() => // Long running child task
            {
                Console.WriteLine($"Long running child task started. running on thread: {Thread.CurrentThread.ManagedThreadId}");
            }, TaskCreationOptions.LongRunning);
        });

        parent.Wait(); //wait for the parent task to complete
        Console.WriteLine("All Task Complete");

        Console.ReadLine();
    }
}
