using System;
using System.Linq;
using System.Threading.Tasks;

class FunctionalPurityInPLNIQ
{
    static int position = 0;
    static void Main() 
    {
        Console.WriteLine("Demo of Functional Purity in C# PLINQ:");
        Console.WriteLine("--------------------------------------");

         ShowTheResultWithoutFunctionalPurity();
         ShowTheResultWithFunctionalPurity();

        Console.ReadLine(); // Keeps console window open until Enter is pressed
    }
    static void ShowTheResultWithoutFunctionalPurity()
    {
        // The following query multiplies each element by its position.
        // GIven an input of Enumerable.Range(0, 5), it should output Squares
        var query = from n in Enumerable.Range(0, 5).AsParallel()
                    select n * position++;
        Console.WriteLine("Result Without functional purity:");
        // Execute the query and print the results

        foreach (var result in query)
        {
            Console.WriteLine(result);
        }
        Console.WriteLine($"position:{position}");

        position = 0;
    }
    static void ShowTheResultWithFunctionalPurity()
    {
        // The following query multiplies each element by its position.
        // Making the code functionally pure
        var query = Enumerable.Range(0, 5).AsParallel().Select((n, position) => n * position);

        Console.WriteLine("Reult with functional purity:");
        //Execute the query and print the results
        foreach (var result in query)
        {
            Console.WriteLine(result);
        }

        Console.WriteLine($"position:{position}");
    }
}    