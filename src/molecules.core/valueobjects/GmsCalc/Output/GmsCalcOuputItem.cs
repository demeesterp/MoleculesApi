namespace molecules.core.valueobjects.GmsCalc.Output
{
    public class GmsCalcOuputItem
    {
        public string OrderName { get; }
        public int OrderItemId { get; }        
        public string MoleculeName { get; }
        public GmsCalculationKind Kind { get; }
        public string[] ContentLines { get; }

        public GmsCalcOuputItem(string displayName, string[] contentLines)
        {
            var entries = displayName.Split("_");
            
            if (entries.Length != 4 || !Enum.IsDefined(typeof(GmsCalculationKind), entries[3])) { 
                throw new ArgumentException($"Invalid display name {displayName}"); 
            }
            
            OrderName = entries[0];
            OrderItemId = int.Parse(entries[1]);
            MoleculeName = entries[2];
            Kind = (GmsCalculationKind)Enum.Parse(typeof(GmsCalculationKind), entries[3]);
            ContentLines = contentLines;
        }


    }
}
