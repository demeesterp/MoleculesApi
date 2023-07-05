using molecules.core.valueobjects;

namespace molecules.core.aggregates
{
    public class CalcOrderItem
    {
        public int Id { get; set; }

        public int CalcOrderId { get; set; }

        public string MoleculeName { get; set; } = string.Empty;

        public int Charge { get; set; } = 0;

        public CalcType CalcType { get; set; } = CalcType.GeoOpt;        

        public string XYZ { get; set; } = string.Empty;
        
    }
}
