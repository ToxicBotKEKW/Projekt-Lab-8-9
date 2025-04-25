namespace Projekt_Lab_8_9
{
    public class Equipment
    {
        public List<Pickaxe> PickaxeList { get; private set; }
        private static Equipment _instance;

        private Equipment()
        {
            PickaxeList = new List<Pickaxe>();
        }

        public static Equipment GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Equipment();
            }

            return _instance;
        }

        public void AddPickaxe(Pickaxe pickaxe)
        {
            if (PickaxeList.Any(x => x.Id == pickaxe.Id))
            {
                throw new PickaxeAlreadyExistsException(pickaxe.Id);
            }

            PickaxeList.Add(pickaxe);
        }

        public Pickaxe EquipPickaxe(int id)
        {
            return PickaxeList.Find(x => x.Id == id);
        }
    }
}
