using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_Lab_8_9
{
    public abstract class Mine
    {
        public String Name { get; private set; }
        public int Level { get; set; } = 1;
        public Dictionary<int, double> PointForClick { get; private set; }
        public Dictionary<int, double> PointsPerInterval { get; private set; }
        public abstract ResourceType ResourceType { get; }
        public Dictionary<int, Dictionary<ResourceType,double>> LevelRequirments { get; private set; }

        protected Mine(
            String name
            )
        {
            Name = name;
            LevelRequirments = new Dictionary<int, Dictionary<ResourceType, double>>();
            PointForClick = new Dictionary<int, double>();
            PointsPerInterval = new Dictionary<int, double>();
        }

        public virtual double GetPointForClick()
        {
            double value = 0;

            if (!PointForClick.TryGetValue(Level, out value))
            {
                Console.WriteLine($"Brak danych dla poziomu {Level}.");
            }

            return value;
        }

        public virtual double GetPointForClick(Pickaxe pickaxe)
        {
            double value = 0;

            if (!PointForClick.TryGetValue(Level, out value))
            {
                Console.WriteLine($"Brak danych dla poziomu {Level}.");
            }

            if (pickaxe.RequirmentLevel > Level)
            {
                throw new PickaxeLevelRequirementException();
            }

            if (pickaxe.Multiplier.TryGetValue(ResourceType, out int multiplier))
            {
                value = value * (1 + multiplier);
            }

            return value;
        }

        public virtual double GetPointsPerInterval()
        {
            double value = 0;

            if (!PointsPerInterval.TryGetValue(Level, out value))
            {
                Console.WriteLine($"Brak danych dla poziomu {Level}.");
            }

            return value;
        }

        public void Levelup()
        {
            Level++;
        }

        public Dictionary<ResourceType, double> RequirmentsForNextLevel()
        {
            return LevelRequirments.GetValueOrDefault(Level + 1, null);
        }
    }
}
