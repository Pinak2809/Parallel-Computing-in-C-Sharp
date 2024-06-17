// using System;
// using System.Threading.Tasks;

// class Parallel_class
// {
//     static void Main(string[]args)
//     {
//         Console.WriteLine("Parallel .Invoke()");
//         Console.WriteLine("-------------------");

//         Console.WriteLine("Scenario: Preparing for the big dinner party...");
//         Console.WriteLine("---------------------------------------------- ");
//         // Imagine each task as a different aspect of party preparation
//         // Parallel.Invoke(
//         //     () => CookMainCourse(),     // Task 1: Cooking the main course
//         //     () => SetTable(),           // Task 2: Setting the table
//         //     () => Decorate(),           // Task 3: Decorating the dining area
//         //     () => PrepareAppetizers()   // Task 4: Preparing appetizers
//         // );
//         // Creating an array of Action delegates

//         // Creating an array of Action delegates
//         Action [] actions = new Action []
//         {
//         CookMainCourse,      // Task 1: Cooking the main course
//         SetTable,            // Task 2: Setting the table
//         Decorate,            // Task 3: Decorating the dining area
//         PrepareAppetizers    // Task 4: Preparing appetizers
//         };
//         // Invoking the tasks in parallel using Parallel. Invoke
//         Parallel.Invoke(actions);

//         Console.WriteLine("----------------------------------");
//         Console.WriteLine("All Tasks Complete");

//         Console.ReadLine();
//     }

//     static void CookMainCourse()
//     {
//         Console.WriteLine("In-Progress: Food Being Cooked");
//         Task.Delay(3000).Wait(); // Simulate Cooking
//         Console.WriteLine("Completed: Food ready");
//     }

//     static void SetTable()
//     {
//         Console.WriteLine("In-Progress: Setting the table");
//         Task.Delay(2000).Wait(); // Simulate table setting
//         Console.WriteLine("Completed: Table set");
//     }

//     static void Decorate()
//     {
//         Console.WriteLine("In-Progress: Decorating..");
//         Task.Delay(4000).Wait(); // Simulate Decorating
//         Console.WriteLine("Completed: Decoration Ready");
//     }

//     static void PrepareAppetizers()
//     {
//         Console.WriteLine("In-Progress: Preparing Appetizers");
//         Task.Delay(2500).Wait(); // Simulate preparing Appetizers
//         Console.WriteLine("Completed: Appretizers Ready");
//     }

// }