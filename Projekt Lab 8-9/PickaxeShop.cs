using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_Lab_8_9
{
    public class PickaxeShop
    {
        private static PickaxeShop _instance;
        public Dictionary<Pickaxe, Dictionary<ResourceType, double>> PickaxeList { get; private set; }

        private PickaxeShop()
        {
            PickaxeList = new Dictionary<Pickaxe, Dictionary<ResourceType, double>>();
        }

        public static PickaxeShop GetInstance()
        {
            if (_instance == null)
            {
                _instance = new PickaxeShop();
            }

            return _instance;
        }

        public void AddPickaxe(Pickaxe pickaxe, Dictionary<ResourceType, double> cost) 
        {
            if(PickaxeList.Keys.Any(x => x.Id == pickaxe.Id))
            {
                throw new PickaxeAlreadyExistsException(pickaxe.Id);
            }

            PickaxeList.Add(pickaxe, cost);
        }

        public Dictionary<Pickaxe, Dictionary<ResourceType, double>> FindPickaxeByName(String name)
        {
            return PickaxeList
                .Where(x => x.Key.Name.Equals(name))
                .ToDictionary(x => x.Key, x => x.Value);
        }

        public Dictionary<Pickaxe, Dictionary<ResourceType, double>> FindPickaxeById(int id)
        {
            return PickaxeList
                .Where(x => x.Key.Id == id)
                .ToDictionary(x => x.Key, x => x.Value);
        }
    }
}
