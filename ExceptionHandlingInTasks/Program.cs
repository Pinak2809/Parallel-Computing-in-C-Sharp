// using System;
// using System.Threading.Tasks;

// class Program
// {
//     //static void Main(string[] args)
//     static async Task Main(string[] args)
//     {
//         // Console.WriteLine("Efficient Exception-Handling in Tasks");
//         // Console.WriteLine("-------------------------------------");

//         // int divisor = 0;
//         // Task<int> calc = Task.Factory.StartNew(() => 5 / divisor);
//         // try
//         // {
//         //     Console.WriteLine(calc.Result);
//         // }
//         // catch (AggregateException aex)
//         // {
//         //     Console.WriteLine($"Exception: {aex.InnerException.Message}"); // Exception: Attempted to divide by 0
//         // }

//         Console.WriteLine("Efficient Exception-Handling in Tasks");
//         Console.WriteLine("-------------------------------------");

//         //CallMainAsyncMethod();

//         await CallMainAsyncMethod();

//         Console.ReadLine();
//     }

//      static async Task CallMainAsyncMethod()
//     {
//         try
//         {
//             var task1 = MyAsyncMethod1();
//             var task2 = MyAsyncMethod2();
//             await Task.WhenAll(task1, task2);
//         }
//         catch (Exception ex)
//         {
//             Console.WriteLine(ex);
//         }
//     }

//     static async Task MyAsyncMethod1()
//     {
//         int divisor = 0;
//         // var i = 5 / divisor;
//         // Console.WriteLine(i);

//         Console.WriteLine($"divisor Value : {divisor}");
//         await Task.CompletedTask;
//     }

//     static async Task MyAsyncMethod2()
//     {
//         int divisor = 0; 
//         var i = 10 / divisor;
//         Console.WriteLine(i);
//         await Task.CompletedTask;
//     }
// }
