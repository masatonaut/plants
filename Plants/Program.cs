//Author:   Masato Ito
//Date:     2021.05.13.
//Title:    Program to solve the task

using System;
using System.Collections.Generic;
using System.Linq;
using TextFile;

namespace Plants
{
    public class Program
    {
        static void Main()
        {
            TextFileReader reader = new TextFileReader("inp.txt");
            List<Plant> plants = new List<Plant>();

            // Read input file
            if (reader.ReadLine(out string line))
            {
                int n = int.Parse(line);

                for (int i = 0; i < n; i++)
                {
                    if (reader.ReadLine(out line))
                    {
                        string[] tokens = line.Split(' ');

                        string name = tokens[0];
                        string species = tokens[1];
                        int nutrientLevel = int.Parse(tokens[2]);

                        switch (species)
                        {
                            case "wom":
                                plants.Add(new Wombleroot(name, nutrientLevel));
                                break;
                            case "wit":
                                plants.Add(new Wittentoot(name, nutrientLevel));
                                break;
                            case "wor":
                                plants.Add(new Woreroot(name, nutrientLevel));
                                break;
                        }
                    }
                }

                //Simulation
                RadiationSimulation(plants);
            }
        }

        public static void RadiationSimulation(List<Plant> plants)
        {
            int days = 0;
            int consecutiveNoRadiationDays = 0;
            Radiation radiation = Radiation.None;

            while (consecutiveNoRadiationDays < 2)
            {
                // Compute radiation for the day
                int alphaDemand = plants.Where(p => p.RadiationDemand == Radiation.Alpha).Sum(p => p.RadiationDemandStrength);
                int deltaDemand = plants.Where(p => p.RadiationDemand == Radiation.Delta).Sum(p => p.RadiationDemandStrength);

                if (alphaDemand - deltaDemand >= 3)
                {
                    radiation = Radiation.Alpha;
                }
                else if (deltaDemand - alphaDemand >= 3)
                {
                    radiation = Radiation.Delta;
                }
                else
                {
                    radiation = Radiation.None;
                }

                // Apply radiation and update plants
                foreach (Plant plant in plants)
                {
                    plant.ApplyRadiation(radiation);

                    if (plant.NutrientLevel <= 0 || plant.NutrientLevel > 10)
                    {
                        plant.IsAlive = false;
                    }

                    if (plant.IsAlive)
                    {
                        plant.ComputeRadiationDemand();
                    }
                }

                // Print state of plants and radiation
                Console.WriteLine($"Day {days + 1}");
                Console.WriteLine($"Radiation: {radiation}");
                foreach (Plant plant in plants)
                {
                    Console.WriteLine(plant);
                }
                Console.WriteLine();

                if (radiation == Radiation.None)
                {
                    consecutiveNoRadiationDays++;
                }
                else
                {
                    consecutiveNoRadiationDays = 0;
                }
                days++;
            }
        }
    }
}
