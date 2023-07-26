namespace molecules.core.valueobjects.GmsCalc.Input
{
    public class GmsCalcInputItem
    {
        public GmsCalcItem Details { get; }

        public GmsCalcInputItem(string moleculeName, string content, GmsCalculationKind kind)
        {
            Details = new GmsCalcItem(moleculeName, kind, content);
        }

        public string DisplayName => $"{Details.MoleculeName}_{Details.Kind}";

        public string Content => Details.Content;

    }
}
