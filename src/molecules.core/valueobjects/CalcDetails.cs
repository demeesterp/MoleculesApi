using molecules.core.valueobjects.BasisSet;
using static System.Net.Mime.MediaTypeNames;

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
                    retval.Add((lineItems[0], decimal.Parse(lineItems[1]), decimal.Parse(lineItems[2]), decimal.Parse(lineItems[3])));
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
