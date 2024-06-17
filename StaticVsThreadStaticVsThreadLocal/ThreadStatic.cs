/*using System;
using System.Threading;

public class ThreadStatic
{
    [ThreadStatic]
    static int num = 5;

    public static void Main()
    {
        Console.WriteLine("[ThreadStatic] Attribute Multithreading Context:");
        Console.WriteLine("--------------------------------------------");

        Console.WriteLine($"Main Thread - Initial Value: {num}");

        // Start a new thread and modify the num value
        Thread thread = new Thread(() =>
        {
            num = 3;
            Console.WriteLine($"New Thread - Updated value: {num}");
        });
        thread.Start();
        thread.Join(); // Wait for the new thread to finish

        Console.WriteLine($"Main Thread - Final Value: {num}");
        Console.ReadLine();
    }
} */
