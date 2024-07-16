using Microsoft.VisualStudio.TestPlatform.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Plants;

namespace Plants.Tests
{
    [TestClass]
    public class PlantsTests
    {
        [TestMethod]
        public void EmptyTest()
        {
            List<Plant> plants = new List<Plant>();
            string filePath = "empty.txt";
            File.WriteAllText(filePath, "");
            Program.RadiationSimulation(plants);
            Assert.AreEqual(0, plants.Count, "The program should not have added any plants when the input file is empty");
        }

        [TestMethod]
        public void SinglePlantTest()
        {
            List<Plant> plants = new List<Plant>();
            plants.Add(new Wittentoot("Plant1", 7));
            Program.RadiationSimulation(plants);

            Assert.AreEqual(1, plants.Count); 
            Plant plant = plants[0];
            Assert.AreEqual(5, plant.NutrientLevel); 
            Assert.IsTrue(plant.IsAlive); 
        }

        [TestMethod]
        public void SinglePlantAbove10Test()
        {
            List<Plant> plants = new List<Plant>();
            plants.Add(new Wittentoot("Plant1", 12)); 
            Program.RadiationSimulation(plants);
            Assert.IsFalse(plants[0].IsAlive);
        }

        [TestMethod]
        public void MultiplePlantsTest()
        {   
            List<Plant> plants = new List<Plant>
            {
                new Wombleroot("Plant1", 7),   
                new Wittentoot("Plant2", 5),   
                new Woreroot("Plant3", 4),     
                new Wittentoot("Plant4", 3)    
            };

            using (StringWriter writer = new StringWriter())
            {
                Console.SetOut(writer);
                Program.RadiationSimulation(plants);
                string output = writer.ToString();
                Assert.IsTrue(output.Contains("Radiation: None")); 
                Assert.IsTrue(output.Contains("Radiation: None")); 
                Assert.IsTrue(output.Contains("Plant1 6"));
                Assert.IsTrue(output.Contains("Plant2 4"));
                Assert.IsTrue(output.Contains("Plant3 3"));
                Assert.IsTrue(output.Contains("Plant4 2"));
            }
        }
    }
}
