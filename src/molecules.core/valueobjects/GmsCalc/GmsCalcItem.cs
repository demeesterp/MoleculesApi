namespace molecules.core.valueobjects.GmsCalc
{
    public class GmsCalcItem
    {
        public GmsCalcItem(string moleculeName, GmsCalculationKind kind,  string content)
        {
            MoleculeName = moleculeName;
            Content = content;
            Kind = kind;
        }

        public string MoleculeName { get; }

        public string Content { get; }

        public GmsCalculationKind Kind { get; }

    }
}
