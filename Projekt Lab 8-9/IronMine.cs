namespace Projekt_Lab_8_9
{
    public class IronMine : Mine
    {
        private static IronMine _instance;
        public override ResourceType ResourceType => ResourceType.Iron;

        private IronMine() : base("Kopalnia Żelaza")
        {
        }

        public static IronMine GetInstance()
        {
            if (_instance == null)
            {
                _instance = new IronMine();
            }

            return _instance;
        }
    }
}
