namespace Projekt_Lab_8_9
{
    public class PickaxeLevelRequirementException : Exception
    {
        public PickaxeLevelRequirementException()
            : base($"Poziom kopalni jest za niski by używać tego kilofa.") { }
    }
}
