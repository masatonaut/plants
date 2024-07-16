//Author:   Masato Ito
//Date:     2021.05.13.
//Title:    abstract and concrete classes of plants

namespace Plants
{
    public abstract class Plant
    {
        public string Name { get; set; }
        public int NutrientLevel { get; set; }
        public bool IsAlive { get; set; }
        public Radiation RadiationDemand { get; set; }
        public int RadiationDemandStrength { get; set; }

        public Plant(string name, int nutrientLevel)
        {
            Name = name;
            NutrientLevel = nutrientLevel;
            IsAlive = true;
            RadiationDemand = Radiation.Alpha;
            RadiationDemandStrength = 0;
        }

        public abstract void ApplyRadiation(Radiation radiation);
        public abstract void ComputeRadiationDemand();

        public override string ToString()
        {
            return $"{Name} {NutrientLevel} (Radiation demand: {RadiationDemand} strength: {RadiationDemandStrength})";
        }
    }

    public class Wombleroot : Plant
    {
        public Wombleroot(string name, int nutrientLevel) : base(name, nutrientLevel)
        {
        }

        public override void ApplyRadiation(Radiation radiation)
        {
            switch (radiation)
            {
                case Radiation.Alpha:
                    NutrientLevel += 2;
                    break;
                case Radiation.Delta:
                    NutrientLevel -= 2;
                    break;
                case Radiation.None:
                    NutrientLevel -= 1;
                    break;
            }

            if (NutrientLevel <= 0 || NutrientLevel > 10)
            {
                IsAlive = false;
            }
        }

        public override void ComputeRadiationDemand()
        {
            RadiationDemand = Radiation.Alpha;
            RadiationDemandStrength = 10;
        }
    }

    public class Wittentoot : Plant
    {
        public Wittentoot(string name, int nutrientLevel) : base(name, nutrientLevel)
        {
        }

        public override void ApplyRadiation(Radiation radiation)
        {
            switch (radiation)
            {
                case Radiation.Alpha:
                    NutrientLevel -= 3;
                    break;
                case Radiation.Delta:
                    NutrientLevel += 4;
                    break;
                case Radiation.None:
                    NutrientLevel -= 1;
                    break;
            }

            if (NutrientLevel <= 0 || NutrientLevel > 10)
            {
                IsAlive = false;
            }
        }

        public override void ComputeRadiationDemand()
        {
            if (NutrientLevel < 5)
            {
                RadiationDemand = Radiation.Delta;
                RadiationDemandStrength = 4;
            }
            else if (NutrientLevel >= 5 && NutrientLevel <= 10)
            {
                RadiationDemand = Radiation.Delta;
                RadiationDemandStrength = 1;
            }
            else
            {
                RadiationDemand = Radiation.None;
                RadiationDemandStrength = 0;
            }
        }
    }

    public class Woreroot : Plant
    {
        public Woreroot(string name, int nutrientLevel) : base(name, nutrientLevel)
        {
        }

        public override void ApplyRadiation(Radiation radiation)
        {
            switch (radiation)
            {
                case Radiation.Alpha:
                case Radiation.Delta:
                    NutrientLevel += 1;
                    break;
                case Radiation.None:
                    NutrientLevel -= 1;
                    break;
            }

            if (NutrientLevel <= 0 || NutrientLevel > 10)
            {
                IsAlive = false;
            }
        }

        public override void ComputeRadiationDemand()
        {
            RadiationDemand = Radiation.None;
            RadiationDemandStrength = 0;
        }
    }

    public enum Radiation
    {
        None,
        Alpha,
        Delta
    }
}
