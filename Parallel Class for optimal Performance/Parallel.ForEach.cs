using System;
using System.Threading.Tasks;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Parallel.ForEach(): ");
        Console.WriteLine("---------------------------");
        //Sequential foreach loop
        Console.WriteLine("Using Sequencial foreach loop: ");
        Console.WriteLine("--------------------------");
        foreach (char c in "Mastering Parallel Programming")
        {
            MyFunction(c);
        }

        //Parallelised using Parallel. ForEach
        Console.WriteLine();
        Console.WriteLine("Parallelised using Parallel. ForEach");
        Console.WriteLine("------------------------------------");
        Parallel.ForEach("Mastering Parallel Programming", MyFunction);

        Console.ReadLine();
    }

    static void MyFunction(char c)
    {
        Console.WriteLine($"Character - {c}");
    }
}