using Newtonsoft.Json;
using System.ComponentModel;


namespace Projekt_Lab_8_9
{
    public class MineGame : INotifyPropertyChanged
    {
        private static MineGame _instance;
        public event PropertyChangedEventHandler? PropertyChanged;

        private double _iron;
        public double Iron
        {
            get => _iron;
            set
            {
                if (_iron != value)
                {
                    _iron = value;
                    OnPropertyChanged(nameof(Iron));
                }
            }
        }

        private double _gold;
        public double Gold
        {
            get => _gold;
            set
            {
                if (_gold != value)
                {
                    _gold = value;
                    OnPropertyChanged(nameof(Gold));
                }
            }
        }

        private double _diamond;
        public double Diamond
        {
            get => _diamond;
            set
            {
                if (_diamond != value)
                {
                    _diamond = value;
                    OnPropertyChanged(nameof(Diamond));
                }
            }
        }

        public Pickaxe UsedPickaxe { get; private set; }
        public IronMine IronMine { get; private set; }
        public GoldMine GoldMine { get; private set; }
        public DiamondMine DiamondMine { get; private set; }
        public Equipment Equipment { get; private set; }
        public PickaxeShop PickaxeShop { get; private set; }

        private MineGame()
        {
            Iron = default;
            Gold = default;
            Diamond = default;
            UsedPickaxe = new Pickaxe();
            IronMine = IronMine.GetInstance();
            GoldMine = GoldMine.GetInstance();
            DiamondMine = DiamondMine.GetInstance();
            Equipment = Equipment.GetInstance();
            PickaxeShop = PickaxeShop.GetInstance();
        }

        public static MineGame GetInstance()
        {
            if (_instance == null)
            {
                _instance = new MineGame();
            }

            return _instance;
        }

        public Dictionary<int, (Pickaxe, Dictionary<ResourceType, double>)> GetPickaxesYouCanBuy(string name)
        {
            if (name == null)
            {
                return PickaxeShop.PickaxeList
                    .Where(x => !Equipment.PickaxeList.Any(p => p.Id == x.Value.Item1.Id))
                    .ToDictionary(x => x.Key, x => x.Value);
            }

            return PickaxeShop.FindPickaxeByName(name)
                .Where(x => !Equipment.PickaxeList.Any(p => p.Id == x.Value.Item1.Id))
                .ToDictionary(x => x.Key, x => x.Value);
        }

        public void BuyPickaxe(int idPickaxe)
        {
            if (!PickaxeShop.PickaxeList.ContainsKey(idPickaxe))
            {
                return;
            }

            var pickaxeData = PickaxeShop.PickaxeList[idPickaxe];
            Pickaxe pickaxe = pickaxeData.Item1;
            Dictionary<ResourceType, double> cost = pickaxeData.Item2;

            double ironCost = cost.GetValueOrDefault(ResourceType.Iron, 0);
            double goldCost = cost.GetValueOrDefault(ResourceType.Gold, 0);
            double diamondCost = cost.GetValueOrDefault(ResourceType.Diamond, 0);

            if (Iron < ironCost || Gold < goldCost || Diamond < diamondCost)
            {
                return;
            }

            Iron -= ironCost;
            Gold -= goldCost;
            Diamond -= diamondCost;

            Equipment.AddPickaxe(pickaxe);
        }

        public void EquipPickaxe(int idPickaxe)
        {
            Pickaxe pickaxe = Equipment.PickaxeList.Find(x => x.Id == idPickaxe);

            if (pickaxe == null)
            {
                return;
            }

            UsedPickaxe = pickaxe;
        }
        public void UpgradeMine(Mine mine)
        {
            Dictionary<ResourceType, double> cost = mine.RequirmentsForNextLevel();

            if (cost == null)
            {
                return;
            }

            double ironCost = cost.GetValueOrDefault(ResourceType.Iron, 0);
            double goldCost = cost.GetValueOrDefault(ResourceType.Gold, 0);
            double diamondCost = cost.GetValueOrDefault(ResourceType.Diamond, 0);

            if (Iron < ironCost || Gold < goldCost || Diamond < diamondCost)
            {
                return;
            }

            Iron -= ironCost;
            Gold -= goldCost;
            Diamond -= diamondCost;

            mine.Levelup();
        }


        public String PointForClick(ResourceType type)
        {
            double value = 0;
            try
            {
                if (type == ResourceType.Iron)
                {
                    value = IronMine.GetPointForClick(UsedPickaxe);
                    Iron += value;
                }
                else if (type == ResourceType.Gold)
                {
                    value = GoldMine.GetPointForClick(UsedPickaxe);
                    Gold += value;
                }
                else if (type == ResourceType.Diamond)
                {
                    value = DiamondMine.GetPointForClick(UsedPickaxe);
                    Diamond += value;
                }
            }
            catch (PickaxeLevelRequirementException e)
            {
                return $"{e.Message}";
            }

            return $"+ {value}";
        }

        public String PointsPerInterval(ResourceType type)
        {
            double value = 0;
            if (type == ResourceType.Iron)
            {
                value = IronMine.GetPointsPerInterval();
                Iron += value;
            }
            else if (type == ResourceType.Gold)
            {
                value = GoldMine.GetPointsPerInterval();
                Gold += value;
            }
            else if (type == ResourceType.Diamond)
            {
                value = DiamondMine.GetPointsPerInterval();
                Diamond += value;
            }

            return $"+ {value}";
        }

        public String SaveDataToJson()
        {
            return JsonConvert.SerializeObject(_instance, Formatting.Indented);
        }

        public void LoadDataFromJson(String json)
        {
            var loadedData = JsonConvert.DeserializeObject<MineGame>(json);

            if (loadedData != null)
            {
                double iron = loadedData.Iron;
                double gold = loadedData.Gold;
                double diamond = loadedData.Diamond;

                _instance.Iron = loadedData.Iron;
                _instance.Gold = loadedData.Gold;
                _instance.Diamond = loadedData.Diamond;
                _instance.UsedPickaxe = loadedData.UsedPickaxe;

                _instance.IronMine = loadedData.IronMine;
                _instance.GoldMine = loadedData.GoldMine;
                _instance.DiamondMine = loadedData.DiamondMine;
                _instance.Equipment = loadedData.Equipment;
                _instance.PickaxeShop = loadedData.PickaxeShop;
            }


        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
