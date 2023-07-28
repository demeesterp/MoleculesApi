using System.Text;

namespace molecules.core.valueobjects.GmsCalc
{
    public class GmsCalcInfo
    {
        public GmsCalcInfo(string moleculeName, GmsCalculationKind kind,  StringBuilder fileContent)
        {
            MoleculeName = moleculeName;
            Kind = kind;
            Content = fileContent;
        }

        public string MoleculeName { get; }

        public GmsCalculationKind Kind { get; }

        public StringBuilder Content { get; }

    }
}
