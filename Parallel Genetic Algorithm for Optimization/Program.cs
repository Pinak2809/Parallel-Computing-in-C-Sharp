using System;
using System.Collections.Generic;
using System.Linq;

class ParallelGeneticAlgorithmTSP
{
    static void Main(string[] args)
    {
        int populationSize = 100;
        int generations = 1000;
        int numCities = 20;
        
        double[][] distances = GenerateDistanceMatrix(numCities);
        List<int[]> population = InitializePopulation(populationSize, numCities);

        for (int generation = 0; generation < generations; generation++)
        {
            population = EvolvePopulation(population, distances);

            if (generation % 100 == 0)
            {
                double bestFitness = population.Min(individual => CalculateFitness(individual, distances));
                Console.WriteLine($"Generation {generation}: Best fitness = {bestFitness}");
            }
        }
    }

    static double[][] GenerateDistanceMatrix(int numCities)
    {
        var random = new Random();
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
                    distances[i][j] = random.Next(1, 100);
            }
        }

        return distances;
    }

    static List<int[]> InitializePopulation(int populationSize, int numCities)
    {
        return Enumerable.Range(0, populationSize)
        .AsParallel()
        .Select(_ => Enumerable.Range(0, numCities).OrderBy(x => Guid.NewGuid()).ToArray())
        .ToList();
    }

    static List<int[]> EvolvePopulation(List<int[]> population, double[][] distances)
    {
        var parallelOptions = new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount };
        
        var newPopulation = new List<int[]>(population.Count);

        Parallel.For(0, population.Count, parallelOptions, _ =>
        {
            var parent1 = SelectParent(population, distances);
            var parent2 = SelectParent(population, distances);
            var offspring = Crossover(parent1, parent2);
            Mutate(offspring);
            lock (newPopulation)
            {
                newPopulation.Add(offspring);
            }
        });

    return newPopulation;
    }

    static int[] SelectParent(List<int[]> population, double[][] distances)
    {
        var random = new Random();
        int tournamentSize = 5;
        var tournament = new List<int[]>();

        for (int i = 0; i < tournamentSize; i++)
        {
            tournament.Add(population[random.Next(population.Count)]);
        }

        return tournament.OrderBy(individual => CalculateFitness(individual, distances)).First();
    }

    static int[] Crossover(int[] parent1, int[] parent2)
    {
        var random = new Random();
        int length = parent1.Length;
        int start = random.Next(length);
        int end = random.Next(start, length);

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
        var random = new Random();
        int index1 = random.Next(individual.Length);
        int index2 = random.Next(individual.Length);

        (individual[index1], individual[index2]) = (individual[index2], individual[index1]);
    }

    static double CalculateFitness(int[] individual, double[][] distances)
    {
        return Enumerable.Range(0, individual.Length)
            .AsParallel()
            .Sum(i => distances[individual[i]][individual[(i + 1) % individual.Length]]);
    }
}
