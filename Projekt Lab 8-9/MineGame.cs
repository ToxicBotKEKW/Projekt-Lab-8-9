using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using System.ComponentModel;
using static System.Net.Mime.MediaTypeNames;


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
            private set
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
            private set
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
            private set
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
            Iron = 0;
            Gold = 0;
            Diamond = 0;

            IronMine = IronMine.GetInstance();
            GoldMine = GoldMine.GetInstance();
            DiamondMine = DiamondMine.GetInstance();
            Equipment = Equipment.GetInstance();
            PickaxeShop = PickaxeShop.GetInstance();

            InitializeGameData();
        }

        private void InitializeGameData()
        {
            var defaultPickaxe = new Pickaxe
            {
                Id = 1,
                Name = "Podstawowy kilof",
                ImageName = "pickaxe-0.png",
                Multiplier = new Dictionary<ResourceType, int>
                {
                    { ResourceType.Iron, 0 },
                    { ResourceType.Gold, 0 },
                    { ResourceType.Diamond, 0 }
                },
                RequirmentLevel = 1
            };

            var pickaxe1 = new Pickaxe
            {
                Id = 2,
                Name = "Kilof - 1",
                ImageName = "pickaxe-1.png",
                Multiplier = new Dictionary<ResourceType, int>
                {
                    { ResourceType.Iron, 2 },
                    { ResourceType.Gold, 1 },
                    { ResourceType.Diamond, 0 }
                },
                RequirmentLevel = 2
            };

            var pickaxe2 = new Pickaxe
            {
                Id = 3,
                Name = "Kilof - 2",
                ImageName = "pickaxe-2.png",
                Multiplier = new Dictionary<ResourceType, int>
                {
                    { ResourceType.Iron, 2 },
                    { ResourceType.Gold, 1 },
                    { ResourceType.Diamond, 0 }
                },
                RequirmentLevel = 2
            };

            var pickaxe3 = new Pickaxe
            {
                Id = 4,
                Name = "Kilof - 3",
                ImageName = "pickaxe-3.png",
                Multiplier = new Dictionary<ResourceType, int>
                {
                    { ResourceType.Iron, 2 },
                    { ResourceType.Gold, 1 },
                    { ResourceType.Diamond, 0 }
                },
                RequirmentLevel = 2
            };

            var pickaxe4 = new Pickaxe
            {
                Id = 5,
                Name = "Kilof - 4",
                ImageName = "pickaxe-4.png",
                Multiplier = new Dictionary<ResourceType, int>
                {
                    { ResourceType.Iron, 2 },
                    { ResourceType.Gold, 1 },
                    { ResourceType.Diamond, 0 }
                },
                RequirmentLevel = 2
            };

            var adminPickaxe = new Pickaxe
            {
                Id = 0,
                Name = "Kilof - admin",
                ImageName = "pickaxe-admin.png",
                Multiplier = new Dictionary<ResourceType, int>
                {
                    { ResourceType.Iron, 1000 },
                    { ResourceType.Gold, 1000 },
                    { ResourceType.Diamond, 1000 }
                },
                RequirmentLevel = 0
            };

            UsedPickaxe = defaultPickaxe;
            Equipment.AddPickaxe(defaultPickaxe);
            Equipment.AddPickaxe(adminPickaxe);

            PickaxeShop.AddPickaxe(
                pickaxe1,
                new Dictionary<ResourceType, double>
                {
                    { ResourceType.Iron, 20 },
                    { ResourceType.Gold, 1 },
                    { ResourceType.Diamond, 0.5 }
                }
            );

            PickaxeShop.AddPickaxe(
                pickaxe2,
                new Dictionary<ResourceType, double>
                {
                    { ResourceType.Iron, 40 },
                    { ResourceType.Gold, 1 },
                    { ResourceType.Diamond, 0.5 }
                }
            );

            PickaxeShop.AddPickaxe(
                pickaxe3,
                new Dictionary<ResourceType, double>
                {
                    { ResourceType.Iron, 10 },
                    { ResourceType.Gold, 3 },
                    { ResourceType.Diamond, 0.5 }
                }
            );

            PickaxeShop.AddPickaxe(
                pickaxe4,
                new Dictionary<ResourceType, double>
                {
                    { ResourceType.Iron, 50 },
                    { ResourceType.Gold, 6 },
                    { ResourceType.Diamond, 0.5 }
                }
            );


            IronMine.LevelRequirments.Add(2, new Dictionary<ResourceType, double> { { ResourceType.Iron, 100 } });
            IronMine.LevelRequirments.Add(3, new Dictionary<ResourceType, double> { { ResourceType.Iron, 300 } });
            IronMine.LevelRequirments.Add(4, new Dictionary<ResourceType, double> { { ResourceType.Iron, 700 }, { ResourceType.Gold, 50 } });

            IronMine.PointForClick.Add(1, 1);
            IronMine.PointForClick.Add(2, 2);
            IronMine.PointForClick.Add(3, 3.5);
            IronMine.PointForClick.Add(4, 6);

            IronMine.PointsPerInterval.Add(1, 10);
            IronMine.PointsPerInterval.Add(2, 25);
            IronMine.PointsPerInterval.Add(3, 50);
            IronMine.PointsPerInterval.Add(4, 80);


            GoldMine.LevelRequirments.Add(2, new Dictionary<ResourceType, double> { { ResourceType.Gold, 100 } });
            GoldMine.LevelRequirments.Add(3, new Dictionary<ResourceType, double> { { ResourceType.Gold, 300 } });
            GoldMine.LevelRequirments.Add(4, new Dictionary<ResourceType, double> { { ResourceType.Gold, 700 }, { ResourceType.Iron, 200 } });

            GoldMine.PointForClick.Add(1, 0.1);
            GoldMine.PointForClick.Add(2, 0.25);
            GoldMine.PointForClick.Add(3, 0.5);
            GoldMine.PointForClick.Add(4, 1);

            GoldMine.PointsPerInterval.Add(1, 0.25);
            GoldMine.PointsPerInterval.Add(2, 0.6);
            GoldMine.PointsPerInterval.Add(3, 1.5);
            GoldMine.PointsPerInterval.Add(4, 2.5);


            DiamondMine.LevelRequirments.Add(2, new Dictionary<ResourceType, double> { { ResourceType.Diamond, 100 } });
            DiamondMine.LevelRequirments.Add(3, new Dictionary<ResourceType, double> { { ResourceType.Diamond, 250 } });
            DiamondMine.LevelRequirments.Add(4, new Dictionary<ResourceType, double> { { ResourceType.Diamond, 600 }, { ResourceType.Gold, 300 } });

            DiamondMine.PointForClick.Add(1, 0.01);
            DiamondMine.PointForClick.Add(2, 0.03);
            DiamondMine.PointForClick.Add(3, 0.08);
            DiamondMine.PointForClick.Add(4, 0.13);

            DiamondMine.PointsPerInterval.Add(1, 0.05);
            DiamondMine.PointsPerInterval.Add(2, 0.12);
            DiamondMine.PointsPerInterval.Add(3, 0.3);
            DiamondMine.PointsPerInterval.Add(4, 0.55);
        }


        public static MineGame GetInstance()
        {
            if (_instance == null)
            {
                _instance = new MineGame();
            }

            return _instance;
        }

        public Dictionary<Pickaxe, Dictionary<ResourceType, double>> GetPickaxesYouCanBuy()
        {
            return PickaxeShop.PickaxeList
                .Where(x => !Equipment.PickaxeList.Contains(x.Key))
                .ToDictionary(x => x.Key, x => x.Value);
        }

        public void BuyPickaxe(int idPickaxe)
        {
            Pickaxe pickaxe = PickaxeShop.PickaxeList.Keys.FirstOrDefault(x => x.Id == idPickaxe);

            if (pickaxe == null)
            {
                return;
            }

            if (Equipment.PickaxeList.Any(x => x.Id == pickaxe.Id))
            {
                return;
            }

            if (!PickaxeShop.PickaxeList.TryGetValue(pickaxe, out Dictionary<ResourceType, double> cost))
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

            Equipment.AddPickaxe(pickaxe);
        }

        public void EquipPickaxe(int idPickaxe) {
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

            if(cost == null)
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
            catch(PickaxeLevelRequirementException e)
            {
                return $"{e.Message}";
            }

            return $"+ {value}";
        }

        public String PointsPerInterval(ResourceType type) {
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

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
