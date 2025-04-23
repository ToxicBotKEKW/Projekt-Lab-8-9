using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_Lab_8_9
{
    public class Pickaxe
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public String ImageName { get; set; }
        public Dictionary<ResourceType, int> Multiplier { get; set; }
        public int RequirmentLevel { get; set; }

        public Pickaxe(int id, string name, string imageName, Dictionary<ResourceType, int> multiplier, int requirmentLevel)
        {
            Id = id;
            Name = name;
            ImageName = imageName;
            Multiplier = multiplier;
            RequirmentLevel = requirmentLevel;
        }

        public Pickaxe()
        {
        }
    }
}
