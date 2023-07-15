namespace molecule.infrastructure.data.interfaces.DbEntities
{
    public class CalcOrderItemDbEntity
    {
        public int Id { get; set; }

        public string MoleculeName { get; set; } = "";

        public int Charge { get; set; } = 0;

        public string CalcType { get; set; } = "";

        public string BasissetCode { get; set; } = "";

        public string XYZ { get; set; } = "";

        public int CalcOrderId { get; set; }

        public CalcOrderDbEntity CalcOrder { get; set; } = new CalcOrderDbEntity(); 
    }
}
