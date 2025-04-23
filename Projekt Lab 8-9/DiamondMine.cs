using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_Lab_8_9
{
    public class DiamondMine : Mine
    {
        public override ResourceType ResourceType => ResourceType.Diamond;
        private static DiamondMine _instance;

        private DiamondMine() 
            : base("Kopalnia Diamentów")
        {
        }

        public static DiamondMine GetInstance()
        {
            if (_instance == null)
            {
                _instance = new DiamondMine();
            }

            return _instance;
        }

    }
}
