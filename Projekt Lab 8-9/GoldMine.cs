using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_Lab_8_9
{
    public class GoldMine : Mine
    {
        private static GoldMine _instance;
        public override ResourceType ResourceType => ResourceType.Gold;

        private GoldMine() 
            : base("Kopalnia Złota")
        {
        }

        public static GoldMine GetInstance()
        {
            if (_instance == null)
            {
                _instance = new GoldMine();
            }

            return _instance;
        }
    }
}
