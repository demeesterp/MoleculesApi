using molecules.core.valueobjects.BasisSet;
using molecules.shared;

namespace molecules.core.valueobjects
{
    public class CalcDetails
    {
        public int Charge { get; set; }

        public CalcBasisSetCode BasisSetCode { get; set; }

        public string XYZ { get; set; }

        public CalcDetails()
        {
            Charge = 0;
            XYZ = string.Empty;
            BasisSetCode = CalcBasisSetCode.BSTO3G;
        }


        public List<(string symbol, decimal x, decimal y, decimal z)> ParseXyz()
        {
            List<(string symbol, decimal x, decimal y, decimal z)> retval
                                = new List<(string symbol, decimal x, decimal y, decimal z)>();

            string[] lines = XYZ.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
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
