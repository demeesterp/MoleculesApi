namespace molecules.core.valueobjects.GmsCalc.Output
{
    public class GmsCalcOuputItem
    {
        public GmsCalcItem Details { get; }

        public GmsCalcOuputItem(string displayName, string content)
        {
            var (moleculeName, kind) = ParseDisplayName(displayName);
            Details = new GmsCalcItem(moleculeName, kind, content);
        }

        private (string moleculeName, GmsCalculationKind kind) ParseDisplayName(string displayName)
        {
            var result = displayName.Split("_", StringSplitOptions.RemoveEmptyEntries);
            if (result.Length != 2) { throw new ArgumentException($"Invalid display name {displayName}"); }
            if (!Enum.TryParse(result[1], true, out GmsCalculationKind kind)) { throw new ArgumentException($"Invalid calculation kind {result[1]}"); }
            return (result[0], kind);
        }
    }
}
