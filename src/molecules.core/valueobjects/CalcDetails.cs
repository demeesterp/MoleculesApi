using molecules.core.valueobjects.BasisSet;

namespace molecules.core.valueobjects
{
    public class CalcDetails
    {
        public int Charge { get; set; }

        public CalcType CalcType { get; set; }

        public CalcBasisSetCode BasisSetCode { get; set; }

        public string XYZ { get; set; }

        public CalcDetails()
        {
            Charge = 0;
            CalcType = CalcType.GeoOpt;
            XYZ = string.Empty;
            BasisSetCode = CalcBasisSetCode.BSTO3G;
        }


        public List<(string symbol, decimal x, decimal y, decimal z)> ParseXyz()
        {
            List<(string symbol, decimal x, decimal y, decimal z)> retval
                                = new List<(string symbol, decimal x, decimal y, decimal z)>();



            return retval;
        }

        public override string ToString()
        {
            return $"Charge {Charge}, CalcType {CalcType}, XYZ {XYZ}";
        }
    }
}
