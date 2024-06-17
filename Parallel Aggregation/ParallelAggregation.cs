using System;
using System.Linq;

class ParallelAggregation
{
    static void Main()
    {
        Console.WriteLine("PLINQ Parallel Aggregation");
        Console.WriteLine("--------------------------");
        
        // Example data
        int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        Console.WriteLine("-----------------------------");
        Console.WriteLine("SECTION-1: ");
        Console.WriteLine("-----------------------------");
        Console.WriteLine("PLINQ works well with SUM, Average, Min, and Max: ");
        Console.WriteLine("................................................. ");
        
        // Sum
        int sum = numbers.AsParallel().Sum();
        Console.WriteLine($"Sum: {sum}");

        // Average
        double average = numbers.AsParallel().Average();
        Console.WriteLine($"Average: {average}");

        // Min 
        int min = numbers.AsParallel().Min();
        Console.WriteLine($"Min: {min}");

        // Max
        int max = numbers.AsParallel().Max();
        Console.WriteLine($"Max: {max}");

        Console.WriteLine("-----------------------------");
        Console.WriteLine("SECTION-2: ");
        Console.WriteLine("-----------------------------");
        Console.WriteLine("LINQ Aggregate function with/without seed value: ");
        Console.WriteLine("................................................. ");

        // Aggregate with seed value (for multiplication use seed value 0)
        int aggregateMultiplicationWithSeedValue = numbers.Aggregate(0, (total, n) => total * n);
        Console.WriteLine($"aggregate Multiplication With SeedValue: {aggregateMultiplicationWithSeedValue}");

        // Aggregate without seed value
        int aggregateMultiplicationWithoutSeedValue = numbers.Aggregate((total, n) => total * n);
        Console.WriteLine($"aggregate Multiplication Without SeedValue: {aggregateMultiplicationWithoutSeedValue}");

        Console.WriteLine("-----------------------------");
        Console.WriteLine("SECTION-3(a): ");
        Console.WriteLine("-----------------------------");
        Console.WriteLine("Parallel Aggregation with PLINQ's Aggregate function: ");
        Console.WriteLine("................................................. ");

        int result = numbers.AsParallel().Aggregate(
            0,                                   // Seed value
            (localTotal, n) => localTotal + n,   // Local aggregation
            (mainTotal, localTotal) => mainTotal + localTotal, // Global aggregation
            finalResult => finalResult           // Result selector
        );

        Console.WriteLine($"Sum Result using Aggregate function: {result}");

        Console.WriteLine("-----------------------------");
        Console.WriteLine("SECTION-3(b): ");
        Console.WriteLine("-----------------------------");
        
        string strText = "Parallel Computing is the MOST Horrifying subject I had to study till NOW";
        Console.WriteLine($"Text: {strText}");
        int[] letterFrequencies = strText.AsParallel().Aggregate(
            () => new int[26],                        // Seed factory
            (localFrequencies, c) =>                  // Update accumulator function
            {
                char upperC = char.ToUpper(c);
                if (char.IsLetter(upperC))             // Check if the character is a letter
                {
                    int index = upperC - 'A';          // Calculate the index of the letter in the alphabet
                    localFrequencies[index]++;         // Increment the corresponding element in the local accumulator
                }
                return localFrequencies;
            },
            (mainFreq, localFreq) =>                   // Combine accumulator function
                mainFreq.Zip(localFreq, (f1, f2) => f1 + f2).ToArray(), // Combine the local accumulators into the global accumulator
            finalResult => finalResult                 // Result selector
        );
        
        Console.WriteLine("Letter Frequencies: ");
        for (int i = 0; i < 26; i++)
        {
            char letter = (char)('A' + i);             // Convert the index back to the corresponding uppercase letter
            Console.WriteLine($"{letter}: {letterFrequencies[i]}"); // Display the letter and its frequency
        }

        Console.ReadLine();
    }    
}
