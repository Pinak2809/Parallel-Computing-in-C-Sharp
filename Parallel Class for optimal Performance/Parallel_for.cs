// using System;
// using System.Threading.Tasks;

// class Program
// {
//     static void Main(string[] args)
//     {
//         Console.WriteLine("Parallel.For()");
//         Console.WriteLine("---------------------");
//         //Sequential for loop
//         Console.WriteLine("Using Sequencial for loop:");
//         Console.WriteLine("--------------------------- ");
//         for (int i = 0; i < 50; i++)
//         {
//         MyFunction(i);
//         }
//         //Parallelised using Parallel.For with a lambda expression
//         Console.WriteLine();
//         Console.WriteLine("Parallelised using Parallel.For with a lambda expression: ");
//         Console.WriteLine("-----------------------------------------------------------");
//         Parallel.For(0, 50, i => MyFunction(i));

//         //Parallelised using Parallel. For with method group Conversion
//         Console.WriteLine();
//         Console.WriteLine("Parallelised using Parallel.For with method group Conversion: ");
//         Console.WriteLine("-- -");
//         Parallel. For(0, 50, MyFunction);

//         Console.ReadLine();
//     }

//     static void MyFunction(int i)
//     {
//         Console.WriteLine($"Square of {i}: {i * i}");
//     }
// }