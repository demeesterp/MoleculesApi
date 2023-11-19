using molecules.core.valueobjects.BasisSet;
using molecules.shared;

namespace molecules.core.valueobjects
{
    public class CalcDetails(int charge, string xyz, CalcBasisSetCode calcBasisSetCode)
    {
        private static readonly string[] _returns = ["\r\n", "\r", "\n"];

        public int Charge { get; } = charge;
        public string XYZ { get; } = xyz;
        public CalcBasisSetCode BasisSetCode { get; } = calcBasisSetCode;

        public List<(string symbol, decimal x, decimal y, decimal z)> ParseXyz()
        {
            List<(string symbol, decimal x, decimal y, decimal z)> retval = [];

            string[] lines = XYZ.Split(_returns, StringSplitOptions.None);
            foreach(var line in lines)
            {
                string[] lineItems = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                if ( lineItems.Length > 2)
                {
                    retval.Add((lineItems[0], StringConversion.ToDecimal(lineItems[1]), StringConversion.ToDecimal(lineItems[2]), StringConversion.ToDecimal(lineItems[3])));
                }
            }
            return retval;
        }

        public override string ToString()
        {
            return $"Charge {Charge}, XYZ {XYZ}";
        }
    }
}
