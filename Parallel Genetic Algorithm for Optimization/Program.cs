using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

class ParallelGeneticAlgorithmTSP
{
    static double crossoverRate = 0.8;
    static double mutationRate = 0.01;

    static long? randomSeed = null; // Set this to a specific value for reproducibility
    static ThreadLocal<Random> random = new ThreadLocal<Random>(() =>
        randomSeed.HasValue ? new Random((int)randomSeed.Value ^ Thread.CurrentThread.ManagedThreadId)
        : new Random(Guid.NewGuid().GetHashCode())
    );

    static void Main(string[] args)
    {
        int populationSize = 1000; // Increased total population size
        int generations = 1000;
        int numCities = 20;
        int numIslands = Environment.ProcessorCount; // Use one island per CPU core
        int migrationInterval = 100; // Migrate every 100 generations
        int migrationSize = populationSize / (numIslands * 10); // Migrate 10% of each island's population

        if (populationSize <= 0 || generations <= 0 || numCities <= 0)
        {
            throw new ArgumentException("Population size, generations, and number of cities must be positive.");
        }

        double[][] distances = GenerateDistanceMatrix(numCities);

        // Create islands
        List<List<int[]>> islands = Enumerable.Range(0, numIslands)
            .Select(_ => InitializePopulation(populationSize / numIslands, numCities))
            .ToList();

        double previousBestFitness = double.MaxValue;
        int stagnantGenerations = 0;
        int maxStagnantGenerations = 100; // Adjust as needed

        for (int generation = 0; generation < generations; generation++)
        {
            // Evolve each island in parallel
            islands = islands.AsParallel().Select(island => EvolvePopulation(island, distances)).ToList();

            if (generation % migrationInterval == 0)
            {
                // Perform migration
                MigrateBetweenIslands(islands, migrationSize);

                // Calculate best fitness across all islands
                double bestFitness = islands.SelectMany(i => i)
                    .Min(individual => CalculateFitness(individual, distances));

                Console.WriteLine($"Generation {generation}: Best fitness = {bestFitness}");

                // Check for termination condition
                if (Math.Abs(bestFitness - previousBestFitness) < 0.0001) // Adjust threshold as needed
                {
                    stagnantGenerations++;
                    if (stagnantGenerations >= maxStagnantGenerations)
                    {
                        Console.WriteLine($"Terminating: No improvement for {maxStagnantGenerations} migrations");
                        break;
                    }
                }
                else
                {
                    stagnantGenerations = 0;
                }

                previousBestFitness = bestFitness;
            }
        }

        // Find and print the best solution across all islands
        var bestSolution = islands.SelectMany(i => i)
            .OrderBy(individual => CalculateFitness(individual, distances))
            .First();
        Console.WriteLine($"Best solution: {string.Join(" -> ", bestSolution)}");
        Console.WriteLine($"Best fitness: {CalculateFitness(bestSolution, distances)}");
    }

    static void MigrateBetweenIslands(List<List<int[]>> islands, int migrationSize)
    {
        for (int i = 0; i < islands.Count; i++)
        {
            var sourceIsland = islands[i];
            var destinationIsland = islands[(i + 1) % islands.Count]; // Circular migration

            // Select individuals to migrate
            var migrants = sourceIsland
                .OrderBy(_ => random.Value.Next())
                .Take(migrationSize)
                .ToList();

            // Remove migrants from source island
            foreach (var migrant in migrants)
            {
                sourceIsland.Remove(migrant);
            }

            // Add migrants to destination island
            destinationIsland.AddRange(migrants);
        }
    }

    static double[][] GenerateDistanceMatrix(int numCities)
    {
        var rand = random.Value;
        double[][] distances = new double[numCities][];

        for (int i = 0; i < numCities; i++)
        {
            distances[i] = new double[numCities];
            for (int j = 0; j < numCities; j++)
            {
                if (i == j)
                    distances[i][j] = 0;
                else if (i > j)
                    distances[i][j] = distances[j][i];
                else
                    distances[i][j] = rand.Next(1, 100);
            }
        }

        return distances;
    }

    static List<int[]> InitializePopulation(int populationSize, int numCities)
    {
        return Enumerable.Range(0, populationSize)
            .Select(_ => Enumerable.Range(0, numCities).OrderBy(x => random.Value.Next()).ToArray())
            .ToList();
    }

    static List<int[]> EvolvePopulation(List<int[]> population, double[][] distances)
    {
        var parallelOptions = new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount };

        int eliteSize = population.Count / 20; // Keep top 5% of population
        var elites = population.OrderBy(ind => CalculateFitness(ind, distances)).Take(eliteSize).ToList();

        var newPopulation = new List<int[]>(population.Count);
        newPopulation.AddRange(elites);

        Parallel.For(0, population.Count - eliteSize, parallelOptions, _ =>
        {
            var parent1 = SelectParent(population, distances);
            var parent2 = SelectParent(population, distances);
            int[] offspring;

            if (random.Value.NextDouble() < crossoverRate)
            {
                offspring = Crossover(parent1, parent2);
            }
            else
            {
                offspring = parent1.ToArray(); // Clone parent1
            }

            if (random.Value.NextDouble() < mutationRate)
            {
                Mutate(offspring);
            }

            lock (newPopulation)
            {
                newPopulation.Add(offspring);
            }
        });

        return newPopulation;
    }

    static int[] SelectParent(List<int[]> population, double[][] distances)
    {
        int tournamentSize = 5;
        var tournament = new List<int[]>();

        for (int i = 0; i < tournamentSize; i++)
        {
            tournament.Add(population[random.Value.Next(population.Count)]);
        }

        return tournament.OrderBy(individual => CalculateFitness(individual, distances)).First();
    }

    static int[] Crossover(int[] parent1, int[] parent2)
    {
        int length = parent1.Length;
        int start = random.Value.Next(length);
        int end = random.Value.Next(start, length);

        var child = new int[length];
        Array.Fill(child, -1);

        for (int i = start; i <= end; i++)
        {
            child[i] = parent1[i];
        }

        int currentIndex = 0;
        for (int i = 0; i < length; i++)
        {
            if (!child.Contains(parent2[i]))
            {
                while (child[currentIndex] != -1)
                {
                    currentIndex++;
                }
                child[currentIndex] = parent2[i];
            }
        }

        return child;
    }

    static void Mutate(int[] individual)
    {
        int index1 = random.Value.Next(individual.Length);
        int index2 = random.Value.Next(individual.Length);

        (individual[index1], individual[index2]) = (individual[index2], individual[index1]);
    }

    static double CalculateFitness(int[] individual, double[][] distances)
    {
        if (individual.Length < 100) // Adjust this threshold based on your specific case
        {
            double fitness = 0;
            for (int i = 0; i < individual.Length; i++)
            {
                fitness += distances[individual[i]][individual[(i + 1) % individual.Length]];
            }
            return fitness;
        }
        else
        {
            return Enumerable.Range(0, individual.Length)
                .Sum(i => distances[individual[i]][individual[(i + 1) % individual.Length]]);
        }
    }
}
