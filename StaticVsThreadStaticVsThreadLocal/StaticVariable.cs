﻿/*using System;
using System.Threading;

public class Staticvariable
{
    //static variable
    static int num = 5;

    public static void Main()
    {
        Console.WriteLine("Static Variable Demo Multithreading Context:");
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
        Console.ReadLine(); // Static variables should be used judiciously and with proper synchronization mechanisms to prevent race conditions and ensure data integrity in a parallel environment.
    }
}*/