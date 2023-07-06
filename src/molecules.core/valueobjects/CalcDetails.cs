
namespace molecules.core.valueobjects
{
    public class CalcDetails
    {
        public int Charge { get; set; }

        public CalcType CalcType { get; set; }

        public string XYZ { get; set; }

        public CalcDetails()
        {
            Charge = 0;
            CalcType = CalcType.GeoOpt;
            XYZ = string.Empty;
        }
    }
}
