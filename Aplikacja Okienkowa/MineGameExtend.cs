using Projekt_Lab_8_9;

namespace Aplikacja_Okienkowa
{
    public static class MineGameExtend
    {
        public static bool CanUpgradeMine(this MineGame mineGame, Mine mine)
        {
            Dictionary<ResourceType, double> cost = mine.RequirmentsForNextLevel();

            if (cost == null)
            {
                return false;
            }


            double ironCost = cost.GetValueOrDefault(ResourceType.Iron, 0);
            double goldCost = cost.GetValueOrDefault(ResourceType.Gold, 0);
            double diamondCost = cost.GetValueOrDefault(ResourceType.Diamond, 0);

            if (mineGame.Iron < ironCost || mineGame.Gold < goldCost || mineGame.Diamond < diamondCost)
            {
                return false;
            }

            return true;
        }

        public static bool CanBuyPickaxe(this MineGame mineGame, int idPickaxe)
        {
            Pickaxe pickaxe = mineGame.PickaxeShop.PickaxeList.Values.FirstOrDefault(x => x.pickaxe.Id == idPickaxe).pickaxe;

            if (pickaxe == null)
            {
                return false;
            }

            if (mineGame.Equipment.PickaxeList.Any(x => x.Id == pickaxe.Id))
            {
                return false;
            }

            if (!mineGame.PickaxeShop.PickaxeList.TryGetValue(pickaxe.Id, out (Pickaxe, Dictionary<ResourceType, double>) cost))
            {
                return false;
            }

            double ironCost = cost.Item2.GetValueOrDefault(ResourceType.Iron, 0);
            double goldCost = cost.Item2.GetValueOrDefault(ResourceType.Gold, 0);
            double diamondCost = cost.Item2.GetValueOrDefault(ResourceType.Diamond, 0);

            if (mineGame.Iron < ironCost || mineGame.Gold < goldCost || mineGame.Diamond < diamondCost)
            {
                return false;
            }

            return true;
        }
    }
}
