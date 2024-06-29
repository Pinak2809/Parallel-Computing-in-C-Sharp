// using System;
// using System.Linq;
// using System.Threading.Tasks;

// class Program
// {
//     static async Task Main(string[] args)
//     {
//         Console.WriteLine("Efficient Exception-Handling in Tasks");
//         Console.WriteLine("-------------------------------------");

//         await CallMainAsyncMethod();

//         Console.ReadLine();
//     }

//     static async Task CallMainAsyncMethod()
//     {
//         Task aggregationTask = null;
//         try
//         {
//             var task1 = MyAsyncMethod1();
//             var task2 = MyAsyncMethod2();
//             aggregationTask = Task.WhenAll(task1, task2);
//             await aggregationTask;
//         }
//         catch (Exception ex)
//         {
//             if (aggregationTask?.Exception?.InnerExceptions != null && aggregationTask.Exception.InnerExceptions.Any())
//             {
//                 foreach (var innerEx in aggregationTask.Exception.InnerExceptions)
//                 {
//                     Console.WriteLine(innerEx.Message);
//                 }
//             }
//             else
//             {
//                 Console.WriteLine(ex.Message);
//             }
//         }
//     }

//     static async Task MyAsyncMethod1()
//     {
//         int divisor = 0;
//         var i = 5 / divisor;  // This will throw a divide by zero exception
//         Console.WriteLine(i);

//         //Console.WriteLine($"divisor Value : {divisor}");
//         await Task.CompletedTask;
//     }

//     static async Task MyAsyncMethod2()
//     {
//         int divisor = 0;  // This will also throw a divide by zero exception
//         var i = 10 / divisor;
//         Console.WriteLine(i);

//         await Task.CompletedTask;
//     }
// }
