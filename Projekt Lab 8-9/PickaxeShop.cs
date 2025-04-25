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
        public Dictionary<int, (Pickaxe pickaxe, Dictionary<ResourceType, double> cost)> PickaxeList { get; private set; }

        private PickaxeShop()
        {
            PickaxeList = new Dictionary<int, (Pickaxe, Dictionary<ResourceType, double>)>();
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
            if (PickaxeList.ContainsKey(pickaxe.Id))
            {
                throw new PickaxeAlreadyExistsException(pickaxe.Id);
            }

            PickaxeList.Add(pickaxe.Id, (pickaxe, cost));
        }

        public Dictionary<int, (Pickaxe, Dictionary<ResourceType, double>)> FindPickaxeByName(string name)
        {
            return PickaxeList
                .Where(x => x.Value.pickaxe.Name.Equals(name))
                .ToDictionary(x => x.Key, x => x.Value);
        }

        public (Pickaxe, Dictionary<ResourceType, double>)? FindPickaxeById(int id)
        {
            if (PickaxeList.TryGetValue(id, out var result))
            {
                return result;
            }
            return null;
        }
    }
}
